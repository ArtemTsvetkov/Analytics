using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.MsSqlServersQueryConfigurator
{
    class MsSqlServersQueryConfigurator
    {
        //Получение уникальных имен лицензий
        public static string getUnicLicensesName()
        {
            return "SELECT Code FROM Software";
        }

        //Получение данных об использовании лицензий пользователями
        public static string getDataOfUsersUseLicenses()
        {
            return "SELECT(SELECT u.Name FROM Users u WHERE u.UserID = h.UserID) AS UserName,(SELECT u.Host FROM Users u WHERE u.UserID = h.UserID) AS UserHost,(SELECT s.Code FROM Software s WHERE s.SoftwareID = h.SoftwareID) AS SoftWare FROM History h";
        }

        //Получение количества закупленых лицензий всех типов
        public static string getNumberOfPurchasedLicenses()
        {
            return "SELECT Code, NumberOfPurchased FROM Software";
        }

        //Получение данных о процентом соотношении частей бюджета на покупку лицензий
        public static string getPartsInPersentOfPurchasedLicenses()
        {
            return "SELECT Code, AmountOfInvestments FROM Software";
        }
    }
}
