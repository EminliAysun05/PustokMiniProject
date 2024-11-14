using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModels.CategoryViewModels;
using Pustokk.BLL.ViewModels.ProductViewModels;
using Pustokk.DAL.DataContext.Entities;
using Pustokk.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.Services
{
    public class CategoryManager : CrudManager<Category, CategoryViewModel, CategoryCreateViewModel, CategoryUpdateViewModel>, ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryManager(IRepository<Category> categoryRepository, IMapper mapper) : base(categoryRepository, mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
       
        public async Task<List<CategoryViewModel>> GetParentCategoriesAsync()
        {
            //parent categoisi olmayanlari getirecem parentcategory==null
            var parentCategories = await _categoryRepository.GetAllAsync(c=>c.ParentCategoryId==null);

            return _mapper.Map<List<CategoryViewModel>>(parentCategories);
        }

        async Task ICategoryService.CreateAsync(CategoryCreateViewModel model)
        {
            var category = _mapper.Map<Category>(model);
            await _categoryRepository.CreateAsync(category);
        }

        public override async Task<CategoryViewModel> UpdateAsync(CategoryUpdateViewModel model)
        {
            var category = await _categoryRepository.GetAsync(model.Id);
            if (category == null) throw new Exception("Category not found");

            //eyni category ust category kimi eleme
            if(model.ParentCategoryId == model.Id)
            {
                throw new Exception("A category cant be set own parent category");
            }

            int? parentId = model.ParentCategoryId;
            while (parentId != null)
            {
                if (parentId == model.Id)
                {
                    throw new InvalidOperationException("A category cannot have itself or its subcategories as a parent category.");
                }

                // Mövcud `parentId`-ə uyğun olaraq parent kateqoriyanı gətiririk
                var parentCategory = await _categoryRepository.GetAsync((int)parentId);
                parentId = parentCategory?.ParentCategoryId;
            }

            category.Name = model.Name;
            category.ParentCategoryId = model.ParentCategoryId;

            await _categoryRepository.UpdateAsync(category);
            return _mapper.Map<CategoryViewModel>(category);
        }

        public override async Task<CategoryViewModel> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(id);
            if (category == null) throw new Exception("category not found");

            var subCategories = await _categoryRepository.GetAllAsync(c=>c.ParentCategoryId==id);

            foreach (var subCategory in subCategories)
            {
                subCategory.ParentCategoryId = null;
                await _categoryRepository.UpdateAsync(subCategory);
            }

            await _categoryRepository.DeleteAsync(category);
            return _mapper.Map<CategoryViewModel>(category);
        }

        public override async Task<CategoryViewModel?> GetAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(c=>c.Id == id, include: q=>q.Include(c=>c.SubCategories!));
            if (category == null) throw new Exception("Category not found"); ;

            return _mapper.Map<CategoryViewModel>(category);
            
        }

        public async Task<List<CategoryViewModel>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<CategoryViewModel>>(categories);
        }
    }
}
