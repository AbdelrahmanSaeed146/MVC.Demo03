using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MVC.Demo03.PL.Helpers
{
    public static class DocumentSetting
    {
        public static async Task< string> UploadFile( IFormFile File, string FolderName)
        {
            //string FolderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{FolderName}";

            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

            if (Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);
            
            string FileName = $"{Guid.NewGuid()}{Path.GetExtension(File.FileName)}";

            string FilePath = Path.Combine(FolderPath, FileName);

            using var FileStream = new FileStream(FilePath,FileMode.Create);

            await File.CopyToAsync(FileStream);

            return FileName;
        }

        public static void DeleteFile(string FileName , string FolderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
