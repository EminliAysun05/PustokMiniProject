using AutoMapper;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.ViewModels.ProductViewModels;
using Pustokk.BLL.ViewModels.ServiceViewModels;
using Pustokk.DAL.DataContext.Entities;
using Pustokk.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.Services
{
    public class ServiceManager : CrudManager<Service, ServiceViewModel, ServiceCreateViewModel, ServiceUpdateViewModel>, IServiceService
    {
        private readonly IMapper _mapper;
        private readonly ICloudService _cloudService;
        private readonly IRepository<Service> _serviceRepository;
        public ServiceManager(IRepository<Service> repository, IMapper mapper, ICloudService cloudService, IRepository<Service> serviceRepository) : base(repository, mapper)
        {
            _cloudService = cloudService;
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public  async Task<List<ServiceViewModel>> GetAllAsync()
        {
            var services = await _serviceRepository.GetAllAsync();

            return _mapper.Map<List<ServiceViewModel>>(services);
        }

        public override async Task<ServiceViewModel> CreateAsync(ServiceCreateViewModel model)
        {
            var service = _mapper.Map<Service>(model);

            if(model.IconFile != null && model.IconFile.Length > 0)
            {
                var iconUrl = await _cloudService.FileCreateAsync(model.IconFile);
                service.IconUrl = iconUrl;
            }

            await _serviceRepository.CreateAsync(service);
            return _mapper.Map<ServiceViewModel>(service);
        }

        public override async Task<ServiceViewModel> UpdateAsync(ServiceUpdateViewModel model)
        {
            var service = await _serviceRepository.GetAsync(model.Id);
            if (service == null) throw new Exception("Service not found");

            _mapper.Map(model, service);

            if(model.NewIconFile != null && model.NewIconFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(service.IconUrl))
                {
                    await _cloudService.FileDeleteAsync(service.IconUrl);
                }

                service.IconUrl = await _cloudService.FileCreateAsync(model.NewIconFile);
            }

            await _serviceRepository.UpdateAsync(service);
            return _mapper.Map<ServiceViewModel>(service);
        }

        public override async Task<ServiceViewModel> DeleteAsync(int id)
        {
            var service = await _serviceRepository.GetAsync(id);
            if (service == null) throw new Exception("Service not found");

            if(!string.IsNullOrEmpty(service.IconUrl))
            {
                await _cloudService.FileDeleteAsync(service.IconUrl);

            }

            await _serviceRepository.DeleteAsync(service);
            return _mapper.Map<ServiceViewModel>(service);
        }

        public override async Task<ServiceViewModel?> GetAsync(int id)
        {
            var service = await _serviceRepository.GetAsync(id);
            if (service == null) throw new Exception(" Service not found");

            return _mapper.Map<ServiceViewModel>(service);
        }

    }
}
