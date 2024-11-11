//using Pustokk.BLL.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pustokk.BLL.Extentions;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModels.ProductViewModels;
using Pustokk.DAL.DataContext.Entities;
using Pustokk.DAL.Repositories.Contracts;
using System.Runtime.CompilerServices;

namespace Pustokk.BLL.Services;

public class ProductManager : CrudManager<Product, ProductViewModel, ProductCreateViewModel, ProductUpdateViewModel>, IProductService
{
    private readonly ICloudService _cloudService;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductManager(ICloudService cloudService, IProductRepository productRepository, IMapper mapper) : base(productRepository, mapper)
    {
        _cloudService = cloudService;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductViewModel> AddProductWithImagesAsync(ProductCreateViewModel createViewModel)
    {

        var product = _mapper.Map<Product>(createViewModel);


        product.ProductImages = new List<ProductImage>();

        foreach (var file in createViewModel.ImageFiles)
        {
            if (!file.IsImage() || !file.AllowedSize(2))
                throw new Exception("Invalid image file");

            string imageUrl = await _cloudService.FileCreateAsync(file);
            var productImage = new ProductImage { ImageUrl = imageUrl, Product = product };
            product.ProductImages.Add(productImage);

        }

        await _productRepository.CreateAsync(product);
        var productViewModel = _mapper.Map<ProductViewModel>(product);

        return productViewModel;
    }

    //movcud producti tap
    //melumatlari yenile
    //kohne sekilleri sil
    //yenisini elave ele
    //sekli yukle
    //yeni productImage
    //maple qaytar

    public override async Task<ProductViewModel> UpdateAsync(ProductUpdateViewModel updateViewModel)
    {

        if (updateViewModel.Price <= 0)
        {
            throw new Exception("Price must be greater than zero");
        }

        if (updateViewModel.DisCountPrice < 0 || updateViewModel.DisCountPrice > updateViewModel.Price)
        {
            throw new Exception("Discount price mustn be egative or zero and cannot be higher than the original price.");
        }

        var existingProduct = await _productRepository.GetAsync(updateViewModel.Id);//bura 

        if (existingProduct is null)
            throw new Exception("Product not found");

        if (updateViewModel.NewImageFiles != null)
        {
            var imageName = await _cloudService.FileCreateAsync(updateViewModel.NewImageFiles);
            await _cloudService.FileDeleteAsync(existingProduct.NewImageFiles);
            updateViewModel.NewImageFiles = imageName;
        }


    }





}


// tutaq ki burda sekil yaradacaqsan


//public async Task CreateImage(IFormFile image)
//{
//    string imageUrl=await _cloudService.FileCreateAsync(image);

//    //bu imageUr burda url gəldi sənə düzdü mü və product in var
//    //product.ImageUrl=imageUrl
//    //_context.Products.AddAsync(product)
//    // qaranliqaa q okay men fiziki olaraq gelib harasa copy elemirem burda duzdu?yeapp cloudinary ozu eliyir anladimm heiqetenn cox coxx saolllll xosduuu bb
//}
