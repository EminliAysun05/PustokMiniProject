using AutoMapper;
using Pustokk.BLL.ViewModels.AppUserViewModels;
using Pustokk.BLL.ViewModels.CategoryViewModels;
using Pustokk.BLL.ViewModels.ProductImageViewModels;
using Pustokk.BLL.ViewModels.ProductViewModels;
using Pustokk.BLL.ViewModels.ServiceViewModels;
using Pustokk.BLL.ViewModels.SetttingViewModels;
using Pustokk.BLL.ViewModels.SliderViewModels;
using Pustokk.BLL.ViewModels.SubscribeViewModels;
using Pustokk.BLL.ViewModels.TagViewModels;
using Pustokk.DAL.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>()
      .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : ""))
      .ForMember(dest => dest.TagNames, opt => opt.MapFrom(src => src.ProductTags.Select(pt => pt.Tag.Name).ToList()))  
      .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.ProductImages.Select(pi => pi.ImageUrl).ToList()));


            CreateMap<Product, ProductCreateViewModel>()
                .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.ProductTags.Select(pt => pt.TagId).ToList()))
                .ReverseMap()
                .ForPath(src => src.ProductTags, opt => opt.Ignore());

            CreateMap<Product, ProductUpdateViewModel>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.ProductImages.Select(pi => pi.ImageUrl).ToList()))
                .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.ProductTags.Select(pt => pt.TagId).ToList()))
                .ReverseMap()
                .ForPath(src => src.ProductTags, opt => opt.Ignore());

           
            CreateMap<ProductTag, TagViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TagId))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Tag != null ? src.Tag.Name : string.Empty))
                .ReverseMap()
                .ForPath(src => src.TagId, opt => opt.MapFrom(dest => dest.Id));

            CreateMap<ProductViewModel, ProductUpdateViewModel>().ReverseMap();

            //product
            //CreateMap<Product, ProductViewModel>().ReverseMap();
            //CreateMap<Product, ProductCreateViewModel>().ReverseMap();
            //CreateMap<Product, ProductUpdateViewModel>().ReverseMap();


            //category
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Category, CategoryCreateViewModel>().ReverseMap();
            CreateMap<Category, CategoryUpdateViewModel>().ReverseMap();
            CreateMap<CategoryViewModel, CategoryUpdateViewModel>().ReverseMap();



            //appUser
            CreateMap<AppUser, RegisterViewModel>().ReverseMap();
            CreateMap<AppUser, LoginViewModel>().ReverseMap();
            CreateMap<AppUser, AppUserViewModel>().ReverseMap();

            //ProductImage
            CreateMap<ProductImage, ProductImageViewModel>().ReverseMap();
            CreateMap<ProductImage, ProductImageCreateViewModel>().ReverseMap();
            CreateMap<ProductImage, ProductImageUpdateViewModel>().ReverseMap();

            //service
            CreateMap<Service, ServiceViewModel>().ReverseMap();
            CreateMap<Service, ServiceCreateViewModel>().ReverseMap();
            CreateMap<Service, ServiceUpdateViewModel>().ReverseMap();

            // Slider 
            CreateMap<Slider, SliderViewModel>().ReverseMap();
            CreateMap<Slider, SliderCreateViewModel>().ReverseMap();
            CreateMap<Slider, SliderUpdateViewModel>().ReverseMap();
            CreateMap<SliderViewModel, SliderUpdateViewModel>().ReverseMap();

            // Tag 
            CreateMap<Tag, TagViewModel>().ReverseMap();
            CreateMap<Tag, TagCreateViewModel>().ReverseMap();
            CreateMap<Tag, TagUpdateViewModel>().ReverseMap();

            //setting, subscribe
            CreateMap<Setting, SettingViewModel>().ReverseMap();
            CreateMap<Subscribe, SubscribeViewModel>().ReverseMap();
        }
    }
}
