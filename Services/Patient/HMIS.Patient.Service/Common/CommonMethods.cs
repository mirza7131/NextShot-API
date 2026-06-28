using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Service.Common
{
    public class CommonMethods
    {
        public static string IncrementStringEnd(string name, int minNumericalCharacters = 1)
        {
            var prefix = System.Text.RegularExpressions.Regex.Match(name, @"\d+$");
            if (prefix.Success)
            {
                var capture = prefix.Captures[0];
                long number = long.Parse(capture.Value) + 1;
                name = name.Remove(capture.Index, capture.Length) + number.ToString("D" + minNumericalCharacters);
            }

            return name;
        }

        public static int GetGenderMRNId(string Gender)
        {
            switch (Gender)
            {
                case "Male":
                    return 1;
                case "Female":
                    return 2;
                case "TransGender":
                    return 3;
                default:
                    return 1;
            }
        }

        public static string GetRelationShipMRNId(string RelationShip)
        {
            switch (RelationShip)
            {
                case "Self":
                    return 0.ToString().PadLeft(2, '0');
                case "Father":
                    return 1.ToString().PadLeft(2, '0');
                case "Mother":
                    return 2.ToString().PadLeft(2, '0');
                case "Brother":
                    return 3.ToString().PadLeft(2, '0');
                case "Sister":
                    return 4.ToString().PadLeft(2, '0');
                case "Wife":
                    return 5.ToString().PadLeft(2, '0');
                case "Son":
                    return 6.ToString().PadLeft(2, '0');
                case "Daughter":
                    return 7.ToString().PadLeft(2, '0');
                case "Cousion":
                    return 8.ToString().PadLeft(2, '0');
                case "Neighbour":
                    return 9.ToString().PadLeft(2, '0');
                case "Friend":
                    return 10.ToString().PadLeft(2, '0');
                default:
                    return 0.ToString().PadLeft(2, '0');
            }
        }


        public static string DifferenceBetweenTwoDates(DateTime? startDate, DateTime endDate)
        {
            //DateTime startTime = DateTime.Now; //Current Date
            //Console.WriteLine(startTime);

            //string dateString = "8/6/2021 6:19:02 AM";  //If Date is in String Format..

            //DateTime dateFromString = DateTime.Parse(dateString); //Parse the String to the DateTime

            //DateTime endTime = new DateTime(2021, 6, 8, 6, 01, 20);
            //Date in the(yyyy,dd,mm,hh,mm,ss) format


            DateTime startTime = (DateTime)startDate;

            TimeSpan span = endDate.Subtract(startTime);

            int Secondsdiff = span.Seconds;
            int Minutesdiff = span.Minutes;
            int Hoursdiff = span.Hours;
            int Daysdiff = span.Days;

            return Daysdiff+" Days "+ Hoursdiff + " Hours " + Minutesdiff + " Min " + Secondsdiff + " Seconds";
        }



    }
}
