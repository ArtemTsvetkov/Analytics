using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.GroupByTypes
{
    class BasicType
    {
        public SecondType second = new SecondType();
        public MinuteType minute = new MinuteType();
        public HourType hour = new HourType();
        public DayType day = new DayType();
        public MonthType month = new MonthType();
        public YearType year = new YearType();
    }
}
