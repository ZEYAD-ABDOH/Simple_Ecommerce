using Microsoft.AspNetCore.Http;


namespace Ecom.Core.Services
{
    public interface IImageManagementServies
    {

        Task<List<string>> AddImageAsync(IFormFileCollection file, string src);
        void DeleteImageAsync(string src);
    }
}
