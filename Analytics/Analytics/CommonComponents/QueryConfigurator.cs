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
    }
}
