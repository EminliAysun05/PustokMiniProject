using Microsoft.AspNetCore.Http;

namespace Pustokk.BLL.Services.Contracts;

public interface ICloudService
{
    Task<string> FileCreateAsync(IFormFile file);
    Task<bool> FileDeleteAsync(string filePath);

    Task<List<string>> FileCreateAsync(List<IFormFile> files);
    Task FileDeleteAsync(List<string> filePaths);

}
