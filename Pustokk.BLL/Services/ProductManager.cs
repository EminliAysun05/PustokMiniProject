//using Pustokk.BLL.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Pustokk.BLL.Extentions;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModels.CategoryViewModels;
using Pustokk.BLL.ViewModels.ProductViewModels;
using Pustokk.BLL.ViewModels.TagViewModels;
using Pustokk.DAL.DataContext;
using Pustokk.DAL.DataContext.Entities;
using Pustokk.DAL.Repositories;
using Pustokk.DAL.Repositories.Contracts;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Pustokk.BLL.Services;

public class ProductManager : CrudManager<Product, ProductViewModel, ProductCreateViewModel, ProductUpdateViewModel>, IProductService
{
    private readonly ICloudService _cloudService;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITagRepository _tagRepository;
    private readonly AppDbContext _context;

    public ProductManager(ICloudService cloudService, IProductRepository productRepository, IMapper mapper, ICategoryRepository categoryRepository, ITagRepository tagRepository, AppDbContext context) : base(productRepository, mapper)
    {
        _cloudService = cloudService;
        _productRepository = productRepository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _tagRepository = tagRepository;
        _context = context;
    }



    public async Task<ProductViewModel> AddProductWithImagesAsync(ProductCreateViewModel createViewModel)//create
    {

        var product = _mapper.Map<Product>(createViewModel);


        product.ProductImages = new List<ProductImage>();

        foreach (var file in createViewModel.ImageFiles ?? [])
        {
            if (!file.IsImage() || !file.AllowedSize(2))
                throw new Exception("Invalid image file");

            string imageUrl = await _cloudService.FileCreateAsync(file);
            var productImage = new ProductImage { ImageUrl = imageUrl, Product = product };
            product.ProductImages.Add(productImage);

        }
            product.ProductTags = [];

        foreach (var tagId in createViewModel.TagIds)
        {
            
            //isExistTag

            ProductTag productTag = new()
            {
                Product = product,
                TagId = tagId
            };

            product.ProductTags.Add(productTag);

        }

        await _productRepository.CreateAsync(product);
        var productViewModel = _mapper.Map<ProductViewModel>(product);

        return productViewModel;
    }


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

    public override async Task<List<ProductViewModel>> GetAllAsync(
    Expression<Func<Product, bool>>? predicate = null,
    Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null,
    Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null)
    {
        //var products = await _productRepository.GetAllAsync
        //    (predicate, include: q => q.Include(p => p.ProductTags).
        //    Include(p => p.ProductImages), orderBy);

        var products = await _productRepository.GetAllAsync(
     predicate,
     include: q => q.Include(p => p.Category)
                    .Include(p => p.ProductTags)
                    .ThenInclude(pt => pt.Tag)
                    .Include(p => p.ProductImages),
     orderBy);

        var vms = _mapper.Map<List<ProductViewModel>>(products);

        return vms;
    }


    public async Task<List<CategoryViewModel>> GetCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return _mapper.Map<List<CategoryViewModel>>(categories);
    }

    public async Task<List<TagViewModel>> GetTagsAsync()
    {
        var tags = await _tagRepository.GetAllAsync();
        return _mapper.Map<List<TagViewModel>>(tags);
    }

    public override async Task<ProductViewModel?> GetAsync(int id)
    {
        var product = await _productRepository.GetAsync(
            p => p.Id == id,
            include: query => query
            .Include(p => p.Category)
            .Include(p => p.ProductTags).ThenInclude(pt => pt.Tag!)
            .Include(p => p.ProductImages));

        if (product == null) throw new Exception("Not found");

        return _mapper.Map<ProductViewModel?>(product);
    }

    public override async Task<ProductViewModel> DeleteAsync(int id)
    {
        var product = await _productRepository.GetAsync(
            p => p.Id == id,
            include: query => query
                .Include(p => p.ProductImages)
                .Include(p => p.ProductTags)
        );

        if (product == null)
        {
            throw new Exception("Product not found");
        }

        foreach (var image in product.ProductImages)
        {
            await _cloudService.FileDeleteAsync(image.ImageUrl);
        }

        var deletedProduct = await _productRepository.DeleteAsync(product);

        return _mapper.Map<ProductViewModel>(deletedProduct);
    }


    public async Task<List<ProductViewModel>> GetByCategoryIdAsync(int categoryId)
    {
        var productCategory = await _context.Products
                              .Where(p => p.CategoryId == categoryId)
                              .Include(p => p.ProductImages)
                              .ToListAsync();

         var dtos= _mapper.Map<List<ProductViewModel>>(productCategory);

        return dtos;
    }
}




