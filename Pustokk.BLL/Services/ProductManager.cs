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

    public async Task<ProductViewModel> AddProductWithImagesAsync(ProductCreateViewModel createViewModel)//create
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

        var existingProduct = await _productRepository.GetAsync(x => x.Id == updateViewModel.Id, x => x.Include(x => x.ProductTags).Include(x => x.ProductImages)); //noldu men yazanda islmirdi aqmma nebilim valla cxo saol xosduu bbbb

        if (existingProduct is null)
            throw new Exception("Product not found");

        _mapper.Map(updateViewModel, existingProduct);

        if (updateViewModel.RemoveOldImages && existingProduct.ProductImages != null)
        {
            foreach (var oldImage in existingProduct.ProductImages)
            {
                await _cloudService.FileDeleteAsync(oldImage.ImageUrl);
            }
            existingProduct.ProductImages.Clear();
        }

        if (updateViewModel.NewImageFiles != null && updateViewModel.NewImageFiles.Count > 0)
        {
            var newImageUrls = new List<string>();
            foreach (var file in updateViewModel.NewImageFiles)
            {
                var imageUrl = await _cloudService.FileCreateAsync(file);
                newImageUrls.Add(imageUrl);
            }

            if (existingProduct.ProductImages == null)
            {
                existingProduct.ProductImages = new List<ProductImage>();
            }

            foreach (var url in newImageUrls)
            {
                existingProduct.ProductImages.Add(new ProductImage
                {
                    ImageUrl = url
                });

            }

        }

        var updatedProduct = await _productRepository.UpdateAsync(existingProduct);
        return _mapper.Map<ProductViewModel>(updatedProduct);
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
