using Blog_CoreLayer.Utilities;
using Microsoft.AspNetCore.Http;

namespace Blog_CoreLayer.Services.FileManager
{
    public class FileManager : IFileManager
    {
        public void DeleteFile(string FileName, string path)
        {
           var filePath = Path.Combine(Directory.GetCurrentDirectory(),path,FileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public string SaveFileAndReturnName(IFormFile file, string savePath)
        {
            if (file == null)
                throw new Exception("File Is Null");

            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(),savePath.Replace("/","\\"));
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var fullPath = Path.Combine(folderPath , fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stream);

            return fileName;
        }

        public string SaveImageAndReturnImageName(IFormFile file, string savePath)
        {
            var isNotImage = !ImageValidation.Validate(file);
            if (isNotImage)
                throw new Exception();

            return SaveFileAndReturnName(file, savePath);
        }
    }
}
