using System;
using System.Drawing;
using System.IO;
using System.Reflection;


namespace AppCommonMethods
{
    public static class AppCommonMethod
    {

        #region Null Or Empty Check Methods
        public static bool IsNullOrEmptyGuid(Guid? Id)
        {
            if (Id == null || Id == Guid.Empty)
                return true;
            else
                return false;

        }


        public static bool IsNullIntId(int? Id)
        {
            if (Id > 0)
                return false;
            else
                return true;

        }
        public static bool IsNullObject(object? obj)
        {
            if (obj == null)
                return true;
            else
                return false;

        }

        public static bool IsNullorZerolong(long? id)
        {
            if (id.Equals(0) || id == 0 || id == null)
                return true;
            else
                return false;
        }
        public static bool IsNullorZeroInt(int? id)
        {
            if (id.Equals(0) || id == 0 || id == null)
                return true;
            else
                return false;
        }
        public static bool IsNullorZeroDecimal(Decimal? id)
        {
            if (id == null || Math.Abs(id.Value) < 0.000001M)
                return true;
            else
                return false;
        }
        public static bool IsNull<T>(T id)
        {
            if (id == null)
                return true;
            else
                return false;
        }
        public static bool IsNullorEmptyDate(DateTime? date)
        {
            if (date == DateTime.MinValue || !date.HasValue)
                return true;
            else
                return false;
        }

        public static bool IsNullOrEmptyList<T>(List<T> list)
        {
            if (list == null || !list.Any())
                return true;
            else
                return false;
        }

        public static bool IsNullBool(bool? value)
        {
            if (value == null)
                return true;
            else
                return false;
        }

        public static string ConvertPngToJpegBase64(string pngBase64)
        {
            byte[] pngBytes = Convert.FromBase64String(pngBase64);


            using (MemoryStream pngStream = new MemoryStream(pngBytes))
            {
                using (Image image = Image.FromStream(pngStream))
                {
                    using (MemoryStream jpegStream = new MemoryStream())
                    {
                        image.Save(jpegStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] jpegBytes = jpegStream.ToArray();
                        return "data:image/jpg;base64," + Convert.ToBase64String(jpegBytes);
                    }
                }
            }
        }

        #endregion


        #region Replace String Methods

        public static string RemoveDashes(string input)
        {
            return input.Replace("-", string.Empty);
        }

        // ADD DASHES IN CNIC

        public static string AddDashesInCnic(string input)
        {
            input.Replace("-", string.Empty);
            if (!input.Contains("-") && input.Length == 13)
            {
                input = input.Substring(0, 5) + "-" + input.Substring(5);
                input = input.Substring(0, 13) + "-" + input.Substring(13);
            }
            return input;
        }



        #endregion


        #region Count No. of months b/w two dates
        public static int CountMonths(DateTime date1, DateTime date2)
        {
            // Calculate the difference in years and months

            int yearsDiff = date2.Year - date1.Year;
            int monthsDiff = date2.Month - date1.Month;

            // Adjust the difference if the current day is before the given day
            if (date2.Day < date1.Day)
            {
                monthsDiff--;
            }

            // Calculate the total month count
            int totalMonths = yearsDiff * 12 + monthsDiff;

            return totalMonths + 1;
        }
        #endregion


        #region Check if any property of an object is null
        public static bool IsAnyObjectPropertyNull(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (var property in properties)
            {
                object value = property.GetValue(obj);

                if (value == null)
                {
                    return true; // At least one property is null
                }
            }

            return false; // No property is null
        }
        #endregion


        #region GenerateRandom4DigitNumber
        public static int GenerateRandom4DigitNumber()
        {
            Random random = new Random();
            return random.Next(1000, 10000);
        }
        #endregion


        #region GenerateSequenceNo

        public static string GenerateSequenceNumber(int currentMonth, int sequenceNumber)
        {

            string formattedMonth = currentMonth.ToString("D2"); // Ensures two digits, e.g., 01 for January
            string formattedSequence = sequenceNumber.ToString("D4"); // Ensures four digits, e.g., 0001

            if (sequenceNumber >= 9999)
            {
                sequenceNumber = 1; // Reset sequence number to 1
            }
            else
            {
                sequenceNumber++; // Increment sequence number
            }

            return $"{formattedMonth}-{formattedSequence}";
        }
        #endregion

    }
}
