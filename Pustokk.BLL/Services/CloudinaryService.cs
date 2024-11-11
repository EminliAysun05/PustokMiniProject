using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Pustokk.BLL.Services.Contracts;
using System.Net;
using Pustokk.BLL.ViewModels;

namespace Pustokk.BLL.Services;

internal class CloudinaryService : ICloudService
{
    private readonly IConfiguration _configuration;
    private readonly CloudinaryOptionsViewModel _optionsVM;
    private readonly Cloudinary _cloudinary = null!;

    public CloudinaryService(IConfiguration configuration)
    {
        _configuration = configuration;
        _optionsVM = _configuration.GetSection("CloudinarySettings").Get<CloudinaryOptionsViewModel>() ?? new();

        var myAccount = new Account { ApiKey = _optionsVM.APIKey, ApiSecret = _optionsVM.APISecret, Cloud = _optionsVM.CloudName };

        _cloudinary = new Cloudinary(myAccount);
        _cloudinary.Api.Secure = true;
    }

    public async Task<string> FileCreateAsync(IFormFile file)
    {
        string fileName = string.Concat(Guid.NewGuid(), file.FileName.Substring(file.FileName.LastIndexOf('.')));

        var uploadResult = new ImageUploadResult();
        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, stream),
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }
        string url = uploadResult.SecureUrl.ToString();

        return url;
    }

    public async Task<List<string>> FileCreateAsync(List<IFormFile> files)
    {
        var imageUrls = new List<string>();
        foreach (var file in files)
        {
            var imageUrl = await FileCreateAsync(file);
            if (!string.IsNullOrEmpty(imageUrl))
            {
                imageUrls.Add(imageUrl);
            }
        }
        return imageUrls;
    }

    public async Task<bool> FileDeleteAsync(string filePath)
    {
        try
        {
            string publicIdWithExtension = filePath.Substring(filePath.LastIndexOf("{folderName}"));
            string publicId = publicIdWithExtension.Substring(0, publicIdWithExtension.LastIndexOf('.'));

            var deleteParams = new DelResParams()
            {
                PublicIds = new List<string> { publicId },
                Type = "upload",
                ResourceType = ResourceType.Image
            };
            var result = await _cloudinary.DeleteResourcesAsync(deleteParams);

            return result.StatusCode == HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

   

    public async Task FileDeleteAsync(List<string> filePaths)
    {
        foreach (var path in filePaths)
        {
            await FileDeleteAsync(path);
        }
    }

   
}
