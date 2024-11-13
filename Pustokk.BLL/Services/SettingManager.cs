using AutoMapper;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModels.ProductViewModels;
using Pustokk.BLL.ViewModels.SetttingViewModels;
using Pustokk.DAL.DataContext.Entities;
using Pustokk.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.Services
{
    public class SettingManager : CrudManager<Setting, SettingViewModel, SettingCreateViewModel, SettingUpdateViewModel>, ISettingService
    {
        private readonly IRepository<Setting> _settingRepository;
        private readonly IMapper _mapper;
        public SettingManager(IRepository<Setting> repository, IMapper mapper, IRepository<Setting> settingRepository) : base(repository, mapper)
        {
            _settingRepository = settingRepository;
            _mapper = mapper;
        }

        public async Task<List<SettingViewModel>> GetAllAsync()
        {
            var settings = await _settingRepository.GetAllAsync();
            return _mapper.Map<List<SettingViewModel>>(settings);
        }
        public override async Task<SettingViewModel?> GetAsync(int id)
        {
            var setting = await _settingRepository.GetAsync(id);
            return setting != null ? _mapper.Map<SettingViewModel>(setting) : null;
        }
        public override async Task<SettingViewModel> CreateAsync(SettingCreateViewModel model)
        {
            var setting = _mapper.Map<Setting>(model);
            var createdSetting = await _settingRepository.CreateAsync(setting);
            return _mapper.Map<SettingViewModel>(createdSetting);
        }

        public override async Task<SettingViewModel> UpdateAsync(SettingUpdateViewModel model)
        {
            var setting = await _settingRepository.GetAsync(model.Id);
            if (setting == null) throw new Exception("Setting not found");

            setting.Key = model.Key;
            setting.Value = model.Value;

            var updatedSetting = await _settingRepository.UpdateAsync(setting);
            return _mapper.Map<SettingViewModel>(updatedSetting);
        }

        public override async Task<SettingViewModel> DeleteAsync(int id)
        {
            var setting = await _settingRepository.GetAsync(id);
            if (setting == null) throw new Exception("Setting not found");

            var deletedSetting = await _settingRepository.DeleteAsync(setting);
            return _mapper.Map<SettingViewModel>(deletedSetting);
        }
    }
}
