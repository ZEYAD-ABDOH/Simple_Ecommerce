using Ecom.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Ecom.Infrastructure.Repositres
{
    public class ImageManagementServies : IImageManagementServies
    {
        private readonly IFileProvider fileProvider;

        public ImageManagementServies(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }
        public async Task<List<string>> AddImageAsync(IFormFileCollection file, string src)
        {
            var SaveImageSrc = new List<string>();
            var ImageDirectory = Path.Combine("wwwroot", "Images", src);
            if (Directory.Exists(ImageDirectory) is not true)
            {

                Directory.CreateDirectory(ImageDirectory);
            }
            foreach (var item in file)
            {
                if (item.Length > 0)
                {
                    // get images name
                    var ImageName = item.FileName;
                    //Path images
                    // ALL Photo hava src 
                    var ImageSrc = $"Images/{src}/{ImageName}";
                    var root = Path.Combine(ImageDirectory, ImageName);
                    // Sava photo

                    using (FileStream stream = new FileStream(root, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }
                    SaveImageSrc.Add(ImageSrc);


                }

            }
            return SaveImageSrc;
        }

        public void DeleteImageAsync(string src)
        {
            //get info about file
            var info = fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;

            File.Delete(root);





        }
    }
}
