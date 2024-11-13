using AutoMapper;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModels.SliderViewModels;
using Pustokk.BLL.ViewModels.TagViewModels;
using Pustokk.DAL.DataContext.Entities;
using Pustokk.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.Services
{

    public class TagManager : CrudManager<Tag, TagViewModel, TagCreateViewModel, TagUpdateViewModel>, ITagService
    {
        private readonly IRepository<Tag> _tagRepository;
        private readonly IMapper _mapper;
        public TagManager(IRepository<Tag> repository, IMapper mapper, IRepository<Tag> tagRepository) : base(repository, mapper)
        {
            _tagRepository = tagRepository;
            _mapper=mapper;
        }
        public  async Task<List<TagViewModel>> GetAllAsync()
        {
            var tags = await _tagRepository.GetAllAsync();
            return _mapper.Map<List<TagViewModel>>(tags);
        }

        public override async Task<TagViewModel?> GetAsync(int id)
        {
            var tag = await _tagRepository.GetAsync(t => t.Id == id);
            if (tag == null)
            {
                return null;
            }

            return new TagViewModel
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public override async Task<TagViewModel> CreateAsync(TagCreateViewModel model)
        {
            var tag = _mapper.Map<Tag>(model);
            await _tagRepository.CreateAsync(tag);

            return _mapper.Map<TagViewModel>(tag);
        }

        public override async Task<TagViewModel> UpdateAsync(TagUpdateViewModel model)
        {
            var existingTag = await _tagRepository.GetAsync(t => t.Id == model.Id);
            if (existingTag == null)
            {
                throw new Exception("Tag not found");
            }

            existingTag.Name = model.Name;
            await _tagRepository.UpdateAsync(existingTag);

            return _mapper.Map<TagViewModel>(existingTag);
        }

        public override async Task<TagViewModel> DeleteAsync(int id)
        {
            var tag = await _tagRepository.GetAsync(t => t.Id == id);
            if (tag == null) throw new Exception("Tag not found");

            var deletedTag = await _tagRepository.DeleteAsync(tag);
            return _mapper.Map<TagViewModel>(deletedTag);
        }
    }

}

