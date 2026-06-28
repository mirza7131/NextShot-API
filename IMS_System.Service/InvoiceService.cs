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

namespace IMS_System.Service
{
    public class InvoiceService
    {
        #region Class Fields & Propertities
        //  private readonly JWTAuthentication.TokenService _tokenService;
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
        #endregion

        #region Constructor

        public InvoiceService(
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
          UnitOfWork<ViewInvoiceStatusCount> invoiceStatus)
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
        }
        #endregion
        #region CUD Opertations

        public async Task<SpInvoiceDTO> CreateorEditInvoice(SpInvoiceDTO invoice)
        {
            if (AppCommonMethod.IsNullIntId(invoice.InvoiceId))
                return await CreateInvoice(invoice);
            else
                return await EditInvoice(invoice);
        }

        public async Task<SpInvoiceDTO> CreateInvoice(SpInvoiceDTO invoice)
        {
            // invoice.SpId = null;
            if (invoice.DueDate.HasValue)
            {
                DateTime date = invoice.DueDate.Value.Date; // Extracts the DateTime value from nullable
                invoice.DueDate = date.AddDays(1);
            }
            var userEntity = _mapper.Map<SpInvoice>(invoice);
           
            _mapper.Map(invoice, userEntity);
            FillEntity(userEntity);

            await _spInvoice.Repository.Insert(userEntity);
            await _spInvoice.Save();

            return _mapper.Map<SpInvoiceDTO>(userEntity);

        }
        public async Task<SpInvoiceDTO> EditInvoice(SpInvoiceDTO invoice)
        {

            var userEntity = _spInvoice.Repository.GetALL(x => x.InvoiceId == invoice.InvoiceId)
                .FirstOrDefault();

            if (userEntity == null)
            {
                throw new UserFriendlyException($"{CommonMessageConstant.RecordNotFound}: InvoiceId {invoice.InvoiceId}");
            }
            if (invoice.DueDate.HasValue)
            {
                DateTime date = invoice.DueDate.Value.Date; // Extracts the DateTime value from nullable
                userEntity.DueDate = date.AddDays(1);
            }

            FillEntity(userEntity);
            _spInvoice.Repository.Update(userEntity);

            await _spInvoice.CommitAsync();


            return _mapper.Map<SpInvoiceDTO>(userEntity);
        }
        public async Task<InsertComments> AddNotes(InsertComments comments)
        {
            var userEntity = _spInvoice.Repository.GetALL(x => x.InvoiceNumber == comments.InvoiceNumber)
               .FirstOrDefault();

            if (userEntity == null)
            {
                throw new UserFriendlyException($"{CommonMessageConstant.RecordNotFound}: InvoiceNumber {comments.InvoiceNumber}");
            }
            userEntity.Comments = comments.Comments;
            _spInvoice.Repository.Update(userEntity);

            await _spInvoice.CommitAsync();


            return _mapper.Map<InsertComments>(userEntity);
        }
        public async Task<SaveEmployeeDTO> SavePartimeEmployee(SaveEmployeeDTO employee)
        {
            Guid Id = Guid.NewGuid();
            employee.HfSpEmployeeId = Id;
            employee.EmploymentTypeId = 2;

            var userEntity = _mapper.Map<HfSpEmployee>(employee);
            _mapper.Map(employee, userEntity);
            FillEntityEmployee(userEntity);

            await _hfSpEmployee.Repository.Insert(userEntity);
            await _hfSpEmployee.Save();

            return _mapper.Map<SaveEmployeeDTO>(userEntity);
        }

        #endregion
        #region Read Operations
        public async Task<List<DivisionDTO>> GetAllDivisions()
        {
            var divisions = await _Division.Repository
                .GetALL(x => x.ProvinceId == 1)
                .ToListAsync();

            return divisions.Select(d => new DivisionDTO
            {
                Code = d.Code,
                Name = d.Name
            }).ToList();
        }
        public async Task<List<DistrictDTO>> GetAllDistricts(string val)
        {
            var districts = await _District.Repository
                .GetALL(x => x.Code.Contains(val))
                .ToListAsync();

            return districts.Select(d => new DistrictDTO
            {
                Code = d.Code,
                Name = d.Name
            }).ToList();
        }
        public async Task<List<HealthFacilityTypeDTO>> GetHfTypes()
        {
            var hfType = await _hfType.Repository
                .GetALL(x => x.Code == "011" || x.Code == "012")
                .ToListAsync();

            return hfType.Select(d => new HealthFacilityTypeDTO
            {
                Code = d.Code,
                Name = d.Name,
                ShortName = d.Code == "011" ? "DHQ" : d.Code == "012" ? "THQ" : null
            }).ToList();
        }
        public async Task<List<HealthFacilityDTO>> GetHealthFacilities(string hftCode,string code)
        {
            var hf = await _hf.Repository
                .GetALL(x => x.HealthFacilityTypeCode == hftCode && (x.DivisionCode == code || x.DistrictCode == code))
                .ToListAsync();

            return hf.Select(d => new HealthFacilityDTO
            {
                HealthFacilityId = d.HealthFacilityId,
                Name = d.Name,
                
            }).ToList();
        }
        public async Task<List<ServiceTypeDTO>> GetServiceTypes()
        {
            var serviceType = await _serviceType.Repository
                .GetALL()
                .ToListAsync();

            return serviceType.Select(d => new ServiceTypeDTO
            {
                Id = d.Id,
                DisplayName = d.DisplayName
            }).ToList();
        }
        public async Task<List<ValidationDTO>> GetInvoices()
        {
            var invoices = await _spInvoice.Repository
                .GetALL()
                .ToListAsync();

            return invoices.Select(d => new ValidationDTO
            {
                InvoiceId = d.InvoiceId,
                InvoiceNumber = d.InvoiceNumber,
                SpId = d.SpId,
                HfId = d.HfId,
                ServiceTypeId = d.ServiceTypeId,
                Month = d.Month,
                DueDate = d.DueDate,
                IsActive = d.IsActive,
                IssuedBy = d.IssuedBy,
                IssuedOn = d.IssuedOn,
                IsReIssued = d.IsReIssued,
                ReIssuedBy = d.ReIssuedBy,
                ReIssuedOn = d.ReIssuedOn,
                IsApproved = d.IsApproved,
                IsRejected = d.IsRejected,
            }).ToList();
        }
        public async Task<List<SpEmployee>> GetAllEmployeList()
        {
            return await _spEmployee.Repository.GetALL().ToListAsync();
        }
        public async Task<List<ViewSpEmployeeListForInvoiceDTO>> GetEmployeeListForInvoice(FilterEmployeeDTO filter)
        {
            var query = _viewEmployee.Repository.GetALL();
            //if (filter.SpId.HasValue)
            //{
            //    query = query.Where(x => x.SpId == filter.SpId);
            //}
            if (filter.HfId.HasValue)
            {
                query = query.Where(x => x.HfId == filter.HfId);
            }
            if (filter.ServiceTypeId.HasValue)
            {
                query = query.Where(x => x.ServiceTypeId == filter.ServiceTypeId);
            }
            return query.Select(d => new ViewSpEmployeeListForInvoiceDTO
            {
                HfSpEmployeeId = d.HfSpEmployeeId,
                SpId = d.SpId,
                ServiceProvider = d.ServiceProvider,
                HfId = d.HfId,
                HealthFacility = d.HealthFacility,
                ServiceTypeId = d.ServiceTypeId,
                Service =   d.Service,
                DesignationId = d.DesignationId,
                Designation = d.Designation,
                ShiftId = d.ShiftId,
                Shifts = d.Shifts,
                EmploymentTypeId = d.EmploymentTypeId,
                EmploymentTypes = d.EmploymentTypes,
                EmployeeId = d.EmployeeId,
                Name = d.Name,
                ReplacementOf =d.ReplacementOf,
            }).ToList();
        }
        public async Task<List<ViewSpEmployeesListForInvoice>> GetEmployeeToEdit(Guid? HfSpEmployeeId)
        {
            return await _viewEmployee.Repository.GetALL(x=> x.HfSpEmployeeId == HfSpEmployeeId).ToListAsync();
        }
        public async Task<List<ViewAllInvoiceStatus>> GetAllInvoiceStatus()
        {
            return await _viewInvoice.Repository.GetALL().ToListAsync();
        }
        public async Task<List<ViewInvoiceStatusCount>> GetInvoiceStatusCount()
        {
            return await _invoiceStatus.Repository.GetALL().ToListAsync();
        }
        public async Task<List<InvoiceWithEmployeeDetailsDTO>> GetInvoiceById(string? invoiceId)
        {
            // Return an empty list if the invoiceId is null or whitespace
            if (string.IsNullOrWhiteSpace(invoiceId))
            {
                return new List<InvoiceWithEmployeeDetailsDTO>();
            }

            var results = new List<InvoiceWithEmployeeDetailsDTO>();
            using (var connection = _context.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    // Configure the stored procedure command
                    command.CommandText = "GetInvoiceWithEmployeeDetails";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add the parameter
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@InvoiceNumber";
                    parameter.Value = invoiceId;
                    command.Parameters.Add(parameter);

                    // Execute the command
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // Read and map results to DTO
                        while (await reader.ReadAsync())
                        {
                            var dto = new InvoiceWithEmployeeDetailsDTO
                            {
                                // Example mappings, adjust based on actual column names and types returned
                                InvoiceNumber = reader["InvoiceNumber"] == DBNull.Value ? null : reader["InvoiceNumber"].ToString(),
                                SpId = reader["SpId"] == DBNull.Value ? (Guid?)null : (Guid)reader["SpId"],
                                Logo = reader["Logo"] == DBNull.Value ? null : reader["Logo"].ToString(),
                                Banner = reader["Banner"] == DBNull.Value ? null : reader["Banner"].ToString(),
                                SpName = reader["SpName"] == DBNull.Value ? null : reader["SpName"].ToString(),
                                ServiceTypeId = reader["ServiceTypeId"] == DBNull.Value ? (Guid?)null : (Guid)reader["ServiceTypeId"],
                                Service = reader["Service"] == DBNull.Value ? null : reader["Service"].ToString(),
                                HfId = reader["HfId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["HfId"]),
                                HealthFacilityName = reader["HealthFacilityName"] == DBNull.Value ? null : reader["HealthFacilityName"].ToString(),
                                HealthFacilityTypeCode = reader["HealthFacilityTypeCode"] == DBNull.Value ? null : reader["HealthFacilityTypeCode"].ToString(),
                                HFType = reader["HFType"] == DBNull.Value ? null : reader["HFType"].ToString(),
                                Month = reader["Month"] == DBNull.Value ? null : reader["Month"].ToString(),
                                DueDate = reader["DueDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DueDate"]),
                                Comments = reader["Comments"] == DBNull.Value ? null : reader["Comments"].ToString(),
                                IssuedOn = reader["IssuedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["IssuedOn"]),
                                EmployeeName = reader["EmployeeName"] == DBNull.Value ? null : reader["EmployeeName"].ToString(),
                                EmploymentType = reader["EmploymentType"] == DBNull.Value ? null : reader["EmploymentType"].ToString()
                            };
                            results.Add(dto);
                        }
                    }
                }
            }
            return results;
        }

        #endregion
        #region Helper

        private void FillEntity(SpInvoice obj)
        {
            if (obj.InvoiceId == 0)
            {
                //obj.IssuedBy = _tokenService.GetUserIdForInt(); 
                obj.IssuedOn = DateTime.Now;
                obj.IsActive = true;
            }
            else
            {
                obj.IsReIssued = true;
                //obj.UpdatedBy = _tokenService.GetUserIdForInt(); 
                obj.ReIssuedOn = DateTime.Now;
            }
        }
        private void FillEntityEmployee(HfSpEmployee obj)
        {
            if (obj.IsUpdated == null)
            {
              
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
