using Analytics.Modeling.GroupByTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents
{
    static class QueryConfigurator
    {
        //Получение уникальных имен лицензий
        public static string getUnicLicensesName(string dataBaseTable)
        {
            return "SELECT DISTINCT software FROM " + dataBaseTable;
        }

        //Получение данных об использовании для всех видов дицензий
        public static string getDataOfUseAllLicenses(string[] unicLicenseNames)
        {
            string query = "SELECT  i.year_in, i.month_in, i.day_in, i.hours_in";
            for (int i = 0; i < unicLicenseNames.Length; i++)
            {
                query += ", (SELECT COUNT(*) FROM Information ii WHERE ii.software='" +
                    unicLicenseNames[i] + "' AND ii.year_in = i.year_in  AND ii.month_in =  " +
                    "i.month_in AND ii.day_in =  i.day_in AND ii.hours_in = i.hours_in)";
            }
            query += "FROM Information i WHERE hours_in IS NOT NULL GROUP BY hours_in, day_in, " +
                "month_in, year_in ORDER BY year_in, month_in, day_in, hours_in";

            return query;
        }

        //Получение количества закупленых лицензий всех типов
        public static string getNumberOfPurchasedLicenses()
        {
            return "SELECT type, count FROM PurchasedLicenses";
        }

        //Получение данных о процентом соотношении частей бюджета на покупку лицензий
        public static string getPartsInPersentOfPurchasedLicenses()
        {
            return "SELECT type, percent FROM PercentageOfLicense";
        }

        //Получение времени отдачи сервером лицензии
        public static string getTimesGiveLicense(string licenseName, GropByType type)
        {
            if (type.getType().Equals("year"))
            {
                return "SELECT i.year_in FROM Information i WHERE  i.year_in <> "+
                    "null AND i.software='" + licenseName + "' AND i.user_name<>'" +
                    "RevitSystem' ORDER BY i.year_in";
            }
            if (type.getType().Equals("month"))
            {
                return "SELECT CStr(i.year_in*12+i.month_in) FROM Information i WHERE " +
                    "i.year_in <> null AND i.software='" + licenseName + "' " + 
                    "AND i.user_name<>'RevitSystem' ORDER BY i.year_in, i.month_in";
            }
            if (type.getType().Equals("day"))
            {
                return "SELECT CStr(i.year_in*360+i.month_in*30+i.day_in) FROM " +
                    "Information i WHERE  i.year_in <> null AND i.software=" +
                    "'" + licenseName + "' AND i.user_name<>'RevitSystem' " + 
                    "ORDER BY i.year_in, i.month_in, i.day_in";
            }
            if (type.getType().Equals("hour"))
            {
                return "SELECT CStr(i.year_in*8640+i.month_in*720+i.day_in*24+" +
                    "i.hours_in) FROM Information i WHERE i.year_in <> null " +
                    "AND i.software='" + licenseName + "' AND i.user_name<>" +
                    "'RevitSystem' ORDER BY i.year_in, i.month_in, " + 
                    "i.day_in, i.hours_in";
            }
            if (type.getType().Equals("minute"))
            {
                return "SELECT CStr(i.year_in*518400+i.month_in*43200+i.day_in*" +
                    "1440+i.hours_in*60+i.minute_in) FROM Information i  WHERE  " +
                    "i.year_in <> null AND i.software='" + licenseName + "' AND " +
                    "i.user_name<>'RevitSystem' ORDER BY i.year_in, i.month_in, " + 
                    "i.day_in, i.hours_in, i.minute_in";
            }
            if (type.getType().Equals("second"))
            {
                return "SELECT CStr(i.year_in*3110400+i.month_in*2592000+i.day_in*" +
                    "86400+i.hours_in*3600+i.minute_in*60+i.second_in) FROM Information " +
                    "i  WHERE i.year_in <> null AND i.software='" + licenseName + "' " +
                    "AND i.user_name<>'RevitSystem' ORDER BY i.year_in, i.month_in, " + 
                    "i.day_in, i.hours_in, i.minute_in, i.second_in";
            }
            //ДОБАВИТЬ ИСКЛЛЮЧЕНИЕ-НЕИЗВЕСТНЫЙ ТИП
            throw new Exception();
        }

        //Получение разницы во времени между получением и возвращением лицензии
        public static string getInBetweenOutLicenses(string licenseName, GropByType type)
        {
            if (type.getType().Equals("year"))
            {
                return "SELECT i.year_out-i.year_in FROM Information i WHERE " +
                    "i.year_in <> null AND i.year_out <> null AND i.software=" +
                    "'" + licenseName + "' AND i.user_name<>'RevitSystem'  " +
                    "AND i.year_out-i.year_in <> 0 ORDER BY i.year_in, " +
                    "i.month_in, i.day_in, i.hours_in, i.minute_in, " + 
                    "i.second_in";
            }
            if (type.getType().Equals("month"))
            {
                return "SELECT (i.year_out-i.year_in)*12+i.month_out-i.month_in FROM " +
                    "Information i WHERE  i.year_in <> null AND i.year_out <> null AND " +
                    "i.software='" + licenseName + "' AND i.user_name<>'RevitSystem' " +
                    "AND (i.year_out-i.year_in)*12+i.month_out-i.month_in <> 0 ORDER " +
                    "BY i.year_in, i.month_in, i.day_in, i.hours_in, i.minute_in, " + 
                    "i.second_in";
            }
            if (type.getType().Equals("day"))
            {
                return "SELECT (i.year_out-i.year_in)*360+(i.month_out-i.month_in)*30+" +
                    "i.day_out-i.day_in FROM Information i WHERE  i.year_in <> null AND " +
                    "i.year_out <> null AND i.software='" + licenseName + "' AND " +
                    "i.user_name<>'RevitSystem'  AND (i.year_out-i.year_in)*360+(" +
                    "i.month_out-i.month_in)*30+i.day_out-i.day_in <> 0 ORDER BY " +
                    "i.year_in, i.month_in, i.day_in, i.hours_in, i.minute_in, " + 
                    "i.second_in";
            }
            if (type.getType().Equals("hour"))
            {
                return "SELECT (i.year_out-i.year_in)*8640+(i.month_out-i.month_in)*" +
                    "720+(i.day_out-i.day_in)*24+i.hours_out-i.hours_in FROM Information " +
                    "i WHERE  i.year_in <> null AND i.year_out <> null AND i.software=" +
                    "'" + licenseName + "' AND i.user_name<>'RevitSystem'  AND " +
                    "(i.year_out-i.year_in)*8640+(i.month_out-i.month_in)*720+(i.day_out" +
                    "-i.day_in)*24+i.hours_out-i.hours_in <> 0 ORDER BY i.year_in, " + 
                    "i.month_in, i.day_in, i.hours_in, i.minute_in, i.second_in";
            }
            if (type.getType().Equals("minute"))
            {
                return "SELECT (i.year_out-i.year_in)*518400+(i.month_out-i.month_in)*43200+" +
                    "(i.day_out-i.day_in)*1440+(i.hours_out-i.hours_in)*60+i.minute_out-" +
                    "i.minute_in FROM Information i WHERE  i.year_in <> null AND i.year_out" +
                    " <> null AND i.software='" + licenseName + "' AND i.user_name<>" +
                    "'RevitSystem'  AND (i.year_out-i.year_in)*518400+(i.month_out-" +
                    "i.month_in)*43200+(i.day_out-i.day_in)*1440+(i.hours_out-i.hours_in)" +
                    "*60+i.minute_out-i.minute_in <> 0 ORDER BY i.year_in, i.month_in, " + 
                    "i.day_in, i.hours_in, i.minute_in, i.second_in";
            }
            if (type.getType().Equals("second"))
            {
                return "SELECT (i.year_out-i.year_in)*31104000+(i.month_out-i.month_in)*" +
                "2592000+(i.day_out-i.day_in)*86400+(i.hours_out-i.hours_in)*3600+" +
                "(i.minute_out-i.minute_in)*60+i.second_out-i.second_in FROM Information " +
                "i WHERE  i.year_in <> null AND i.year_out <> null AND i.software=" +
                "'" + licenseName + "' AND i.user_name<>'RevitSystem'  AND (i.year_out-" +
                "i.year_in)*31104000+(i.month_out-i.month_in)*2592000+(i.day_out-i.day_in)" +
                "*86400+(i.hours_out-i.hours_in)*3600+(i.minute_out-i.minute_in)*60+" +
                "i.second_out-i.second_in <> 0 ORDER BY i.year_in, i.month_in, " +
                "i.day_in, i.hours_in, i.minute_in, i.second_in";
            }
            //ДОБАВИТЬ ИСКЛЛЮЧЕНИЕ-НЕИЗВЕСТНЫЙ ТИП
            throw new Exception();
        }

        //Получение данных о количестве полученных лицензий за определенный промежуток времени
        public static string getNumberOfLicenesForTime(string licenseName, GropByType type)
        {
            if (type.getType().Equals("year"))
            {
                return "SELECT COUNT(*) FROM INFORMATION i WHERE i.year_in <> 0 AND " +
                    "i.year_out AND i.software='" + licenseName + "' GROUP BY i.year_in";
            }
            if (type.getType().Equals("month"))
            {
                return "SELECT COUNT(*) FROM INFORMATION i WHERE i.year_in <> 0 AND " +
                    "i.year_out AND i.software='" + licenseName + "' GROUP BY i.year_in, " +
                    "i.month_in";
            }
            if (type.getType().Equals("day"))
            {
                return "SELECT COUNT(*) FROM INFORMATION i WHERE i.year_in <> 0 AND " +
                    "i.year_out AND i.software='" + licenseName + "' GROUP BY i.year_in, " +
                    "i.month_in, i.day_in";
            }
            if (type.getType().Equals("hour"))
            {
                return "SELECT COUNT(*) FROM INFORMATION i WHERE i.year_in <> 0 AND " +
                    "i.year_out AND i.software='" + licenseName + "' GROUP BY i.year_in, " +
                    "i.month_in, i.day_in, i.hours_in";
            }
            if (type.getType().Equals("minute"))
            {
                return "SELECT COUNT(*) FROM INFORMATION i WHERE i.year_in <> 0 AND " +
                    "i.year_out AND i.software='" + licenseName + "' GROUP BY i.year_in, " +
                    "i.month_in, i.day_in, i.hours_in, i.minute_in";
            }
            if (type.getType().Equals("second"))
            {
                return "SELECT COUNT(*) FROM INFORMATION i WHERE i.year_in <> 0 AND " +
                    "i.year_out AND i.software='" + licenseName + "' GROUP BY i.year_in, " + 
                    "i.month_in, i.day_in, i.hours_in, i.minute_in, i.second_in";
            }
            //ДОБАВИТЬ ИСКЛЛЮЧЕНИЕ-НЕИЗВЕСТНЫЙ ТИП
            throw new Exception();
        }
    }
}
