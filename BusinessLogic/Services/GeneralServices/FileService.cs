using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.GeneralServices
{
    public interface IFileService
    {
        Task<string> UploadImage(IFormFile image);
    }
    public class FileService : IFileService
    {
        private readonly Cloudinary _cloudinary;

        public FileService(Cloudinary cloudinary) => _cloudinary = cloudinary;

        public async Task<string> UploadImage(IFormFile image)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(image.FileName, image.OpenReadStream()),
                Folder = "HMSImages"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult.SecureUrl.ToString();
        }
    }
}
