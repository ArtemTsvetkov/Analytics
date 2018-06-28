using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.GroupByTypes
{
    static class BasicType
    {
        public static SecondType second = new SecondType();
        public static MinuteType minute = new MinuteType();
        public static HourType hour = new HourType();
        public static DayType day = new DayType();
        public static MonthType month = new MonthType();
        public static YearType year = new YearType();
    }
}
