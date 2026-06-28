using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Dashboard
{
    public class DashboardCountsDTO
    {
        public int TodayVisits { get; set; }
        public int TodayBHUVisits { get; set; }
        public int TodayBHU247Visits { get; set; }
        public int TodayRHCVisits { get; set; }
 
        public int TodayOpenVisits { get; set; }
        public int TodayCloseVisits { get; set; }
        public int TodayOpenBHUVisits { get; set; }
        public int TodayCloseBHUVisits { get; set; }
        public int TodayCloseBHU247Visits { get; set; }
        public int TodayopenBHU247Visits { get; set; }
        public int TodayopenRHCVisits { get; set; }
        public int TodayCloseRHCVisits { get; set; }

        public int weekTotal { get; set; }
        public int weekBHUVisits { get; set; }
        public int weekBHU247Visits { get; set; }
        public int weekRHCVisits { get; set; }

        public int weekOpenTotal { get; set; }
        public int weekCloseTotal { get; set; }
        public int weekOpenBHUVisits { get; set; }
        public int weekCloseBHUVisits { get; set; }
        public int weekOpenBHU247Visits { get; set; }
        public int weekcloseBHU247Visits { get; set; }
        public int weekOpenRHCVisits { get; set; }
        public int weekCloseRHCVisits { get; set; }

        public int monthVisits { get; set; }
        public int monthBHUVisits { get; set; }
        public int monthBHU247Visits { get; set; }
        public int monthRHCVisits { get; set; }


        public int monthOpenVisits { get; set; }
        public int monthCloseVisits { get; set; }
        public int monthOpenBHUVisits { get; set; }
        public int monthCloseBHUVisits { get; set; }
        public int monthOpenBHU247Visits { get; set; }
        public int monthCloseBHU247Visits { get; set; }
        public int monthOpenRHCVisits { get; set; }
        public int monthCloseRHCVisits { get; set; }


        public int TotalVisits { get; set; }
        public int TotalBHUVisits { get; set; }
        public int TotalBHU247Visits { get; set; }
        public int TotalRHCVisits { get; set; }

        public int TotalOpenVisits { get; set; }
        public int TotalCloseVisits { get; set; }
        public int TotalOpenBHUVisits { get; set; }
        public int TotalCloseBHUVisits { get; set; }
        public int TotalOpenBHU247Visits { get; set; }
        public int TotalCloseBHU247Visits { get; set; }
        public int TotalOpenRHCVisits { get; set; }
        public int TotalCloseRHCVisits { get; set; }
    }
}
