using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCommonMethods;
using AutoMapper;
using CommonExceptionHandler;
using CommonMessages;
using IMSSystem.Domain.Models.DbModels;
using IMSSystem.Domain.Models.DTO;
using IMSSystem.Domain.Models.DTO.Invoice;
using IMSSystem.Domain.Repositories._UOW;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMSSystem.Domain.Models.DTO.ServiceProvider;

namespace IMS_System.Service
{
    public class ServiceProviderService
    {
        #region Class Fields & Propertities
         private readonly JWTAuthentication.TokenService _tokenService;
        private readonly ImsSystemContext _context;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<Division> _Division;
        private readonly UnitOfWork<District> _District;
        private readonly UnitOfWork<HealthFacility> _hf;
        private readonly UnitOfWork<HealthFacilityType> _hfType;
        private readonly UnitOfWork<ServiceType> _serviceType;
        private readonly UnitOfWork<SpInvoice> _spInvoice;
        private readonly UnitOfWork<SpEmployee> _spEmployee;
        private readonly UnitOfWork<Shift> _shift;
        private readonly UnitOfWork<ViewSpEmployeesListForInvoice> _viewEmployee;
        private readonly UnitOfWork<ViewAllInvoiceStatus> _viewInvoice;
        private readonly UnitOfWork<HfSpEmployee> _hfSpEmployee;
        private readonly UnitOfWork<ViewInvoiceStatusCount> _invoiceStatus;
        private readonly UnitOfWork<ServiceProvider> _serviceProvider;
        private readonly UnitOfWork<SpService> _spServices;

        #endregion

        #region Constructor

       public ServiceProviderService(
          //JWTAuthentication.TokenService tokenService,
          UnitOfWork<Division> division,
          UnitOfWork<District> district,
           UnitOfWork<HealthFacility> hf,
          UnitOfWork<HealthFacilityType> hfType,
          UnitOfWork<ServiceType> serviceType,
          UnitOfWork<SpInvoice> spInvoice,
          IMapper mapper,
          UnitOfWork<SpEmployee> spEmployee,
          UnitOfWork<Shift> shift,
          UnitOfWork<ViewSpEmployeesListForInvoice> viewEmployee,
          UnitOfWork<HfSpEmployee> hfSpEmployee,
          ImsSystemContext context,
          UnitOfWork<ViewAllInvoiceStatus> viewInvoice,
          UnitOfWork<ViewInvoiceStatusCount> invoiceStatus,
          UnitOfWork<ServiceProvider> serviceProvider,
          UnitOfWork<SpService> spServices
          )
        {
            _Division = division;
            _District = district;
            _hf = hf;
            _hfType = hfType;
            _serviceType = serviceType;
            _spInvoice = spInvoice;
            _mapper = mapper;
            _spEmployee = spEmployee;
            _shift = shift;
            _viewEmployee = viewEmployee;
            _hfSpEmployee = hfSpEmployee;
            _context = context;
            _viewInvoice = viewInvoice;
            _invoiceStatus = invoiceStatus;
            _serviceProvider = serviceProvider;
            _spServices = spServices;
        }
        #endregion

        #region CUD Opertations
        public async Task<RegisterSPDTO> RegisterCompany(RegisterSPDTO register)
        {
           
            var serviceProviderEntity = _mapper.Map<ServiceProvider>(register);

            if (register.SpServices != null && register.SpServices.Any())
            {
              
                serviceProviderEntity.SpServices = register.SpServices
                    .Select(serviceDto => new SpService
                    {
                        Id = Guid.NewGuid(), 
                        ServiceTypeId = serviceDto.ServiceTypeId,
                        IsActive = true,
                        SpId = serviceProviderEntity.Id 
                    })
                    .ToList();
            }
            FillEntity(serviceProviderEntity);

            await _serviceProvider.Repository.Insert(serviceProviderEntity);

            await _serviceProvider.Save();

            return _mapper.Map<RegisterSPDTO>(serviceProviderEntity);
        }




        #endregion
        #region Helper
        private void FillEntity(ServiceProvider obj)
        {
            if (obj.Id == Guid.Empty)
            {
                obj.CreatedBy = new Guid("B7E43E17-9DF4-4BB1-9D05-7F9C4268061B"); 
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
            }
            else
            {
              
                //obj.UpdatedBy = _tokenService.GetUserIdForInt(); 
                obj.UpdatedOn = DateTime.Now;
            }
        }
        #endregion
    }
}
