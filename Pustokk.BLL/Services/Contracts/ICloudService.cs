using Microsoft.AspNetCore.Http;

namespace Pustokk.BLL.Services.Contracts;

public interface ICloudService
{
    Task<string> FileCreateAsync(IFormFile file);
    Task<string> FileDeleteAsync(IFormFile file);
}
