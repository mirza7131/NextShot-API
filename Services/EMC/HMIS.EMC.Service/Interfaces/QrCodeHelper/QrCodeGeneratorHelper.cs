using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.QRCodeGenerator;

namespace HMIS.EMC.Service.Interfaces.QrCodeHelper
{
    public class QrCodeGeneratorHelper : IQrCodeGeneratorHelper
    {
        public byte[] GenerateQrCode(string text)
        {
            byte[] QrCode = new byte[0];
            if (!string.IsNullOrEmpty(text)){
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        QrCode = byteImage;
                    }
                }
            }

            return QrCode;
        }
    }
}
