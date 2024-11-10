using Microsoft.AspNetCore.Http;

namespace Pustokk.BLL.ViewModels.ServiceViewModels;

public class ServiceViewModel : IViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? IconUrl { get; set; }
}

public class ServiceCreateViewModel : IViewModel
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required IFormFile IconFile { get; set; }
}

public class ServiceUpdateViewModel : IViewModel
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public IFormFile? NewIconFile { get; set; }
    public string? ExistingIconUrl { get; set; }
}
