//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using ChampService.DataAccess;

//namespace ChampService.Utils
//{
//    public class DateUtils
//    {
//        public static bool IsItMaintenanceTime(int fyStart, Association.Frequency frq)
//        {
//            DateTime curr = DateTime.Now;

//            switch (frq)
//            {
//                case Association.Frequency.Quarterly:

//                    // CurrentMonth - FYStartMonth is a multiple of 3, then we need add maintenance entries
//                    if (Math.Abs(curr.Month - fyStart) % 3 == 0)
//                        return true;
//                    break;
//                case Association.Frequency.HalfYearly:
//                    // CurrentMonth - FYStartMonth is a multiple of 6, then we need add maintenance entries
//                    if (Math.Abs(curr.Month - fyStart) % 6 == 0)
//                        return true;
//                    break;

//                case Association.Frequency.Yearly:
//                    // CurrentMonth - FYStartMonth is a multiple of 3, then we need add maintenance entries
//                    if (curr.Month == fyStart)
//                        return true;
//                    break;
//                default:
//                    break;
//            }
//            return false;
//        }

//        Dictionary<int, List<int>> DatesHalfYearly =  new Dictionary<int, List<int>>() {
//            { 1, new List<int>() {1, 7 } },
//            { 4, new List<int>() {4, 10 } },
//            { 7, new List<int>() {7, 1 } },
//            { 10, new List<int>() {10, 4 } }
//        };


//        public static DateTime GetCurrentMaintenanceStart(int fyStart, DateTime today, Association.Frequency frq)
//        {
//            DateTime newdate;
//            int subtractMonths = 0;
//            switch (frq)
//            {
//                case Association.Frequency.Quarterly:
//                    subtractMonths = today.Month % 3 - 1;
//                    break;
//                case Association.Frequency.HalfYearly:
//                    if (fyStart == 1 || fyStart == 7)
//                        subtractMonths = today.Month % 6 - 1;
//                    else
//                        subtractMonths = today.Month % 6 - 1;
//                    break;

//                case Association.Frequency.Yearly:
//                    subtractMonths = today.Month % 12;
//                    break;
//                default:
//                    break;
//            }
//            DateTime temp = today.AddMonths(-subtractMonths);
//            newdate = new DateTime(temp.Year, temp.Month, 1);

//            return newdate;
//        }
//        public static DateTime GetNextMaintenanceStart(int fyStart, DateTime today, Association.Frequency frq)
//        {
//            DateTime newdate;
//            int addMonths = 0;
//            switch (frq)
//            {
//                case Association.Frequency.Quarterly:
//                    addMonths = 4 - today.Month % 3;
//                    break;
//                case Association.Frequency.HalfYearly:
//                    addMonths = 7 - today.Month % 6;
//                    break;

//                case Association.Frequency.Yearly:
//                    addMonths = 13 - today.Month;
//                    break;
//                default:
//                    break;
//            }
//            DateTime temp = today.AddMonths(addMonths);
//            newdate = new DateTime(temp.Year, temp.Month, 1);

//            return newdate;
//        }
//    }
//}