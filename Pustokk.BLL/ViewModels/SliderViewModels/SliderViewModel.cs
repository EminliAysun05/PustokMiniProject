﻿using Microsoft.AspNetCore.Http;

namespace Pustokk.BLL.ViewModels.SliderViewModels;

public class SliderViewModel : IViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Link { get; set; }
    public string? ImageUrl { get; set; }
}

public class SliderCreateViewModel : IViewModel
{
    public required string Title { get; set; }
    public required string Subtitle { get; set; }
    public required string Link { get; set; }
    public required IFormFile ImageFile { get; set; }
}

public class SliderUpdateViewModel : IViewModel
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Subtitle { get; set; }
    public required string Link { get; set; }
    public IFormFile? NewImageFile { get; set; }
    public string? ExistingImageUrl { get; set; }
}
