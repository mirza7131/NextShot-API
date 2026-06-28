using System.Text;
using System.Threading.Tasks;
using autoMapper = AutoMapper;
using IMSSystem.Domain.Models.DbModels;
using DbModel = IMSSystem.Domain.Models.DbModels;
using IMSSystem.Domain.Models.DTO.Invoice;
using IMSSystem.Domain.Models.DTO.ServiceProvider;

namespace HMIS.MEAs.Domain
{
    public class AutoMapperProfiles
    {
        public class InvoiceProfile : autoMapper.Profile
        {
            public InvoiceProfile()
            {
                //CreateMap<DbModel.User, RegisterDTO>().ReverseMap();
                CreateMap<DbModel.SpInvoice, SpInvoiceDTO>()
                    .ReverseMap();

                CreateMap<DbModel.HfSpEmployee, SaveEmployeeDTO>()
                    .ReverseMap();
                CreateMap<DbModel.SpInvoice, InsertComments>()
                   .ReverseMap();
                CreateMap<DbModel.ServiceProvider, RegisterSPDTO>()
                  .ReverseMap();
                CreateMap<DbModel.SpService, SpServiceDto>()
                 .ReverseMap();
            }
        }
    }
}
