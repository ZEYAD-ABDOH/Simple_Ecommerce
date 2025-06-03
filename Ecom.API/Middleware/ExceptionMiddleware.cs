using Ecom.API.Halper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace Ecom.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);


        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment environment, IMemoryCache memoryCache)
        {
            _next = next;
            _environment = environment;
            _memoryCache = memoryCache;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                ApplySecurity(context);


                if (IsRequsetAllowed(context) == false)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";
                    var response = new ExceptionApi((int)HttpStatusCode.TooManyRequests,
                      "To many request .Plases try again later");

                    await context.Response.WriteAsJsonAsync(response);
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = _environment.IsDevelopment() ?

                    new ExceptionApi((int)HttpStatusCode.InternalServerError,
                    ex.Message, ex.StackTrace) :
                    new ExceptionApi((int)HttpStatusCode.InternalServerError,
                    ex.Message);


                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);

            }
        }

        private bool IsRequsetAllowed(HttpContext content)
        {
            var ip = content.Connection.RemoteIpAddress.ToString();
            var cachKey = $"Rate :{ip}";
            var dateNow = DateTime.Now;
            var (TimesSpan, count) = _memoryCache.GetOrCreate(cachKey, op =>
            {
                op.AbsoluteExpirationRelativeToNow = _rateLimitWindow;
                return (TimesSpan: dateNow, count: 0);
            });
            if (dateNow - TimesSpan < _rateLimitWindow)
            {
                if (count >= 8)
                {
                    return false;
                }
                _memoryCache.Set(cachKey, (TimesSpan, count += 1), _rateLimitWindow);

            }
            else
            {
                _memoryCache.Set(cachKey, (TimesSpan, count), _rateLimitWindow);

            }
            return true;
        }

        private void ApplySecurity(HttpContext context)
        {
            //  يمنع تخمين المتصفحات
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            //js يمنع ارسال ملفات ضاره مرسلة او مكتوبة ب 
            context.Response.Headers["X-XSS-Protection"] = "1;mode=block";
            //يمنع   موقعي في موقغ اخرى  frame
            context.Response.Headers["X-Frame-Options"] = "DENY";
        }
    }
}
