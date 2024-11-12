using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Pustokk.BLL.Extentions;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModels.SliderViewModels;
using Pustokk.DAL.DataContext.Entities;
using Pustokk.DAL.Repositories.Contracts;
using System.Linq.Expressions;

namespace Pustokk.BLL.Services;

public class SliderManager : CrudManager<Slider, SliderViewModel, SliderCreateViewModel, SliderUpdateViewModel>, ISliderService
{
    private readonly IRepository<Slider> _sliderRepository;
    private readonly ICloudService _cloudService;
    private readonly IMapper _mapper;

    public SliderManager(IRepository<Slider> sliderRepository, ICloudService cloudService, IMapper mapper) : base(sliderRepository, mapper)
    {
        _sliderRepository = sliderRepository;
        _cloudService = cloudService;
        _mapper = mapper;
    }

    public override async Task<SliderViewModel> CreateAsync(SliderCreateViewModel createViewModel)
    {
        if (!createViewModel.ImageFile.IsImage() || !createViewModel.ImageFile.AllowedSize(2))
            throw new Exception("Invalid image file");

        var imageUrl = await _cloudService.FileCreateAsync(createViewModel.ImageFile);
        var slider = _mapper.Map<Slider>(createViewModel);
        slider.ImageUrl = imageUrl;

        var createdSlider = await _sliderRepository.CreateAsync(slider);
        return _mapper.Map<SliderViewModel>(createdSlider);
    }

    public override async Task<SliderViewModel> UpdateAsync(SliderUpdateViewModel updateViewModel)
    {
        var slider = await _sliderRepository.GetAsync(updateViewModel.Id);
        if (slider == null) throw new Exception("Slider not found");

        if (updateViewModel.NewImageFile != null)
        {
            if (!updateViewModel.NewImageFile.IsImage() || !updateViewModel.NewImageFile.AllowedSize(2))
                throw new Exception("Invalid image file");

            var newImageUrl = await _cloudService.FileCreateAsync(updateViewModel.NewImageFile);
            await _cloudService.FileDeleteAsync(slider.ImageUrl!);
            slider.ImageUrl = newImageUrl;
        }

        slider.Title = updateViewModel.Title;
        slider.Description = updateViewModel.Description!;

        var updatedSlider = await _sliderRepository.UpdateAsync(slider);
        return _mapper.Map<SliderViewModel>(updatedSlider);
    }

    public override async Task<SliderViewModel> DeleteAsync(int id)
    {
        var slider = await _sliderRepository.GetAsync(id);
        if (slider == null) throw new Exception("Slider not found");

        await _cloudService.FileDeleteAsync(slider.ImageUrl!);
        var deletedSlider = await _sliderRepository.DeleteAsync(slider);
        return _mapper.Map<SliderViewModel>(deletedSlider);
    }

    public override async Task<SliderViewModel> GetAsync(int id)
    {
        var slider = await _sliderRepository.GetAsync(id);
        if (slider == null) throw new Exception("Slider not found");

        return _mapper.Map<SliderViewModel>(slider);
    }

    public override async Task<List<SliderViewModel>> GetAllAsync(
    Expression<Func<Slider, bool>>? predicate = null,
    Func<IQueryable<Slider>, IIncludableQueryable<Slider, object>>? include = null,
    Func<IQueryable<Slider>, IOrderedQueryable<Slider>>? orderBy = null)
    {
        // Customize predicate, include, or orderBy if needed, then call the base method
        var sliderList = await base.GetAllAsync(predicate, include, orderBy);

        // Additional filtering or operations on the sliders if required, before returning
        return sliderList;
    }
}
