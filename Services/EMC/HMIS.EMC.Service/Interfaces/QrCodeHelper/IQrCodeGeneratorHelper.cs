using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Service.Interfaces.QrCodeHelper
{
    public interface IQrCodeGeneratorHelper
    {
        byte[] GenerateQrCode(string text);
    }
}
