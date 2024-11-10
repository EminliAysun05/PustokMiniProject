using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.Extentions
{
    public static class FileExtention
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image", StringComparison.OrdinalIgnoreCase);
        }
        public static bool AllowedSize(this IFormFile file, int mb)
        {
            return file.Length <= mb * 1024 * 1024;
        }
        public static async Task<string> GenerateFile(this IFormFile file, string path)
        {
            var name = Guid.NewGuid().ToString() + file.FileName;
            path = Path.Combine(path, name);

            using (var fs = new FileStream(path, FileMode.CreateNew))
                await file.CopyToAsync(fs);

            return name;
        }
    }
}
