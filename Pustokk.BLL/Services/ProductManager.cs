//using Pustokk.BLL.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Pustokk.BLL.Extentions;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModels.CategoryViewModels;
using Pustokk.BLL.ViewModels.PaginateViewModels;
using Pustokk.BLL.ViewModels.ProductViewModels;
using Pustokk.BLL.ViewModels.TagViewModels;
using Pustokk.DAL.DataContext;
using Pustokk.DAL.DataContext.Entities;
using Pustokk.DAL.Paginate;
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

        var existingProduct = await _productRepository.GetAsync(x => x.Id == updateViewModel.Id, x => x.Include(x => x.ProductTags).Include(x => x.ProductImages));

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

        var dtos = _mapper.Map<List<ProductViewModel>>(productCategory);

        return dtos;
    }

    public async Task<ProductDetailsViewModel> GetProductDetailsAsync(int productId)
    {
        var product = await _productRepository.GetAsync(
               predicate: x => x.Id == productId,
               include: query => query
                   .Include(p => p.Category)
                   .Include(p => p.ProductImages)
                   .Include(p => p.ProductTags)
           );

        if (product == null)
            throw new Exception("Product not found");

        var relatedProducts = await _productRepository.Query()
       .Where(p => p.Id != productId &&
                   p.ProductTags.Any(pt => product.ProductTags.Select(t => t.TagId).Contains(pt.TagId)))
       .Take(4) // Maksimum 4 məhsul
       .Select(p => new ProductViewModel
       {
           Id = p.Id,
           Name = p.Name,
           Price = p.Price,
           DisCountPrice = p.DisCountPrice,
           ImageUrl = p.ProductImages.FirstOrDefault() != null ? p.ProductImages.FirstOrDefault().ImageUrl : "/default.jpg",
           CategoryName = p.Category.Name
       })
       .ToListAsync();

        // var relatedProducts = await GetRelatedProductsAsync(product.CategoryId, productId);

        return new ProductDetailsViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            DisCountPrice = product.DisCountPrice,
            ProductCode = product.ProductCode,
            Brand = product.Brand,
            Availability = product.Availability,
            RewardPoints = product.RewardPoints,
            CategoryName = product.Category.Name,
            ImageUrls = product.ProductImages.Select(pi => pi.ImageUrl).ToList(),
            RelatedProducts = relatedProducts
        };
    }

    public async Task<List<ProductViewModel>> GetRelatedProductAsync(int productId)
    {
        var product = await _productRepository.GetAsync(productId);
        if (product == null)
        {
            throw new Exception("Product not found.");
        }

        var relatedProductsQuery = await _productRepository
    .Query()
    .Include(p => p.ProductImages)
    .Include(p => p.Category)
    .Where(p => p.Id != productId &&
                p.ProductTags.Any(pt => product.ProductTags.Select(t => t.TagId).Contains(pt.TagId)))
    .Take(4)
    .Select(p => new
    {
        p.Id,
        p.Name,
        p.Price,
        p.DisCountPrice,
        CategoryName = p.Category.Name,
        ProductImages = p.ProductImages
    })
    .ToListAsync();

        // Şəkil URL-lərini burda idarə edin
        var relatedProducts = relatedProductsQuery.Select(p => new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            DisCountPrice = p.DisCountPrice,
            ImageUrl = p.ProductImages.FirstOrDefault()?.ImageUrl ?? "",
            CategoryName = p.CategoryName
        }).ToList();


        return relatedProducts;
    }

    public async Task<List<ProductViewModel>> GetBestSellingProductsAsync()
    {
        var products = await _productRepository.Query()
            .OrderByDescending(p => p.SalesCount)
            .Take(8)
            .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                DisCountPrice = p.DisCountPrice,
                ImageUrl = p.ProductImages.FirstOrDefault().ImageUrl ?? "/default.jpg",
                CategoryName = p.Category.Name
            })
            .ToListAsync();

        return products;
    }

    public async Task<ProductPaginateViewModel> GetPaginatedProductAsync(int pageIndex, int pageSize, string sortBy)
    {
        var query = _productRepository.Query();

        query = sortBy switch
        {
            "name-asc" => query.OrderBy(p => p.Name),
            "name-desc" => query.OrderByDescending(p => p.Name),
            "price-asc" => query.OrderBy(p => p.Price),
            "price-desc" => query.OrderByDescending(p => p.Price),
            _ => query
        };

        var totalCount = await query.CountAsync();

        var items = await query
            .Include(p => p.ProductImages)
            .Include(p => p.Category)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var products = items.Select(p => new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            DisCountPrice = p.DisCountPrice,
            ImageUrl = p.ProductImages.FirstOrDefault()?.ImageUrl ?? "/images/default.jpg",
            CategoryName = p.Category?.Name ?? "Unknown"
        }).ToList();

        return new ProductPaginateViewModel
        {
            Index = pageIndex,
            Size = pageSize,
            Count = totalCount,
            Pages = (int)Math.Ceiling(totalCount / (double)pageSize),
            HasPrevious = pageIndex > 0,
            HasNext = pageIndex < (int)Math.Ceiling(totalCount / (double)pageSize) - 1,
            Products = products
        };
    }

}

//    public async Task<ProductPaginateViewModel> GetPaginatedProductAsync(int pageIndex, int pageSize, string sortBy)
//    {
//        try
//        {
//            // Səhifələmə üçün dəyərləri yoxla
//            if (pageIndex <= 0 || pageSize <= 0)
//            {
//                throw new ArgumentException("Page index and page size must be greater than zero.");
//            }

//            // Məhsulları sorğula
//            var query = _productRepository.Query();

//            // Sort əməliyyatını tətbiq et
//            query = sortBy switch
//            {
//                "name-asc" => query.OrderBy(p => p.Name),
//                "name-desc" => query.OrderByDescending(p => p.Name),
//                "price-asc" => query.OrderBy(p => p.Price),
//                "price-desc" => query.OrderByDescending(p => p.Price),
//                _ => query
//            };

//            // Məhsul sayı
//            var totalCount = await query.CountAsync();

//            // Əgər heç bir nəticə yoxdursa
//            if (totalCount == 0)
//            {
//                return new ProductPaginateViewModel
//                {
//                    Index = pageIndex,
//                    Size = pageSize,
//                    Count = totalCount,
//                    Pages = 0,
//                    Products = new List<ProductViewModel>(),
//                    HasNext = false,
//                    HasPrevious = false
//                };
//            }

//            // Skip hesabını yoxla
//            var skipCount = (pageIndex - 1) * pageSize;
//            if (skipCount >= totalCount)
//            {
//                throw new ArgumentException("Page index is out of range.");
//            }

//            // Səhifələmə tətbiq et
//            var items = await query
//                .Include(p => p.ProductImages) // Şəkillər əlaqəsi
//                .Include(p => p.Category) // Kateqoriya əlaqəsi
//                .Skip(skipCount) // Başlanğıcı keç
//                .Take(pageSize) // Səhifə ölçüsünü götür
//                .ToListAsync();

//            // Məhsulları ViewModel-ə map et
//            var products = items.Select(p => new ProductViewModel
//            {
//                Id = p.Id,
//                Name = p.Name,
//                Price = p.Price,
//                DisCountPrice = p.DisCountPrice,
//                ImageUrl = p.ProductImages.FirstOrDefault()?.ImageUrl ?? "/images/default.jpg",
//                CategoryName = p.Category?.Name ?? "Unknown"
//            }).ToList();

//            // ViewModel-i doldur
//            return new ProductPaginateViewModel
//            {
//                Index = pageIndex,
//                Size = pageSize,
//                Count = totalCount,
//                Pages = (int)Math.Ceiling(totalCount / (double)pageSize),
//                HasPrevious = pageIndex > 1,
//                HasNext = pageIndex < (int)Math.Ceiling(totalCount / (double)pageSize),
//                Products = products,
//                SortBy = sortBy,
//                SortOptions = new Dictionary<string, string>
//            {
//                { "name-asc", "Name: A to Z" },
//                { "name-desc", "Name: Z to A" },
//                { "price-asc", "Price: Low to High" },
//                { "price-desc", "Price: High to Low" }
//            }
//            };
//        }
//        catch (ArgumentException ex)
//        {
//            throw new ArgumentException($"Invalid pagination parameters: {ex.Message}");
//        }
//        catch (InvalidOperationException ex)
//        {
//            throw new InvalidOperationException($"Data processing error: {ex.Message}");
//        }
//        catch (Exception ex)
//        {
//            throw new Exception($"An unexpected error occurred: {ex.Message}");
//        }
//    }

//}





