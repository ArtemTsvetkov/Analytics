using Analytics.CommonComponents.Exceptions;
using Analytics.Modeling.GroupByTypes;
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
            return "SELECT(SELECT u.Name FROM Users u WHERE u.UserID = h.UserID) " +
                "AS UserName,(SELECT u.Host FROM Users u WHERE u.UserID = h.UserID) " +
                "AS UserHost,(SELECT s.Code FROM Software s WHERE s.SoftwareID = " + 
                "h.SoftwareID) AS SoftWare FROM History h";
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

        //Получение среднего количества запросов на получение лицензии за промежуток времени
        public static string getAvgLicesensePerTime(string licenseName, GropByType type)
        {
            if (type.getType().Equals("year"))
            {
                return "SELECT AVG(newTable.licenseCount) FROM (SELECT COUNT(*) AS " +
                    "licenseCount FROM History h WHERE h.SoftwareID=(SELECT s.SoftwareID " +
                    "FROM Software s WHERE s.Code='" + licenseName + "') AND " + 
                    "h.DateIN is not null GROUP BY Year(h.DateIN)) as newTable";
            }
            if (type.getType().Equals("month"))
            {
                return "SELECT AVG(newTable.licenseCount) FROM(SELECT COUNT(*) AS " +
                    "licenseCount FROM History h WHERE h.SoftwareID=(SELECT s.SoftwareID " +
                    "FROM Software s WHERE s.Code='" + licenseName + "') AND " + 
                    "h.DateIN is not null GROUP BY Year(h.DateIN), MONTH(h.DateIN)) as newTable";
            }
            if (type.getType().Equals("day"))
            {
                return "SELECT AVG(newTable.licenseCount) FROM(SELECT COUNT(*) AS " +
                    "licenseCount FROM History h WHERE h.SoftwareID=(SELECT s.SoftwareID " +
                    "FROM Software s WHERE s.Code='" + licenseName + "') AND " +
                    "h.DateIN is not null GROUP BY Year(h.DateIN), MONTH(h.DateIN), " + 
                    "DAY(h.DateIN)) as newTable";
            }
            if (type.getType().Equals("hour"))
            {
                return "SELECT AVG(newTable.licenseCount) FROM(SELECT COUNT(*) AS " +
                    "licenseCount FROM History h WHERE h.SoftwareID=(SELECT s.SoftwareID FROM " +
                    "Software s WHERE s.Code='" + licenseName + "') AND h.DateIN " +
                    "is not null GROUP BY Year(h.DateIN), MONTH(h.DateIN), " + 
                    "DAY(h.DateIN), DATEPART(hour,h.TimeIN)) as newTable";
            }
            if (type.getType().Equals("minute"))
            {
                return "SELECT AVG(newTable.licenseCount) FROM(SELECT COUNT(*) " +
                    "AS licenseCount FROM History h WHERE h.SoftwareID=(SELECT " +
                    "s.SoftwareID FROM Software s WHERE s.Code='" + licenseName +
                    "') AND h.DateIN is not null GROUP BY Year(h.DateIN), " +
                    "MONTH(h.DateIN), DAY(h.DateIN), DATEPART(hour,h.TimeIN), " + 
                    "DATEPART(minute,h.TimeIN)) as newTable";
            }
            if (type.getType().Equals("second"))
            {
                return "SELECT AVG(newTable.licenseCount) FROM(SELECT COUNT(*)" +
                    " AS licenseCount FROM History h WHERE h.SoftwareID=(SELECT " +
                    "s.SoftwareID FROM Software s WHERE s.Code='" + licenseName + "') " +
                    "AND h.DateIN is not null GROUP BY Year(h.DateIN), " +
                    "MONTH(h.DateIN), DAY(h.DateIN), DATEPART(hour,h.TimeIN), " + 
                    "DATEPART(minute,h.TimeIN), DATEPART(second,h.TimeIN)) as newTable";
            }
            throw new UnknownTimeIntervalType("Unknown time interval type");
        }

        //Получение данных об использовании для всех видов лицензий
        public static string getDataOfUseAllLicenses(string[] unicLicenseNames, GropByType type)
        {
            if (type.getType().Equals("year"))
            {
                string query = "SELECT ";
                for (int i = 0; i < unicLicenseNames.Length; i++)
                {
                    if (i == (unicLicenseNames.Length - 1))
                    {
                        query += "(SELECT COUNT(*) FROM History hh WHERE hh." +
                            "SoftwareID=(SELECT SoftwareID FROM Software s WHERE s.Code=" +
                            "'" + unicLicenseNames[i] + "') AND YEAR(hh.DateIN) = " + 
                            "YEAR(h.DateIN))";
                        continue;
                    }
                    query += "(SELECT COUNT(*) FROM History hh WHERE hh.SoftwareID=" +
                        "(SELECT SoftwareID FROM Software s WHERE s.Code=" +
                        "'" + unicLicenseNames[i] + "') AND YEAR(hh.DateIN) = YEAR(h.DateIN)),";
                }
                query += "FROM History h WHERE h.TimeIN IS NOT NULL GROUP BY " + 
                    "YEAR(h.DateIN) ORDER BY YEAR(h.DateIN)";

                return query;
            }
            if (type.getType().Equals("month"))
            {
                string query = "SELECT ";
                for (int i = 0; i < unicLicenseNames.Length; i++)
                {
                    if (i == (unicLicenseNames.Length - 1))
                    {
                        query += "(SELECT COUNT(*) FROM History hh WHERE hh.SoftwareID=" +
                            "(SELECT SoftwareID FROM Software s WHERE s.Code=" +
                            "'" + unicLicenseNames[i] + "') AND YEAR(hh.DateIN) = " + 
                            "YEAR(h.DateIN)  AND MONTH(hh.DateIN) =  MONTH(h.DateIN))";
                        continue;
                    }
                    query += "(SELECT COUNT(*) FROM History hh WHERE hh.SoftwareID=" +
                        "(SELECT SoftwareID FROM Software s WHERE s.Code=" +
                        "'" + unicLicenseNames[i] + "') AND YEAR(hh.DateIN) = " + 
                        "YEAR(h.DateIN)  AND MONTH(hh.DateIN) =  MONTH(h.DateIN)),";
                }
                query += "FROM History h WHERE h.TimeIN IS NOT NULL GROUP BY MONTH(h.DateIN)" + 
                    ", YEAR(h.DateIN) ORDER BY YEAR(h.DateIN), MONTH(h.DateIN)";

                return query;
            }
            if (type.getType().Equals("day"))
            {
                string query = "SELECT ";
                for (int i = 0; i < unicLicenseNames.Length; i++)
                {
                    if (i == (unicLicenseNames.Length - 1))
                    {
                        query += "(SELECT COUNT(*) FROM History hh WHERE hh.SoftwareID=" +
                            "(SELECT SoftwareID FROM Software s WHERE s.Code=" +
                            "'" + unicLicenseNames[i] + "') AND YEAR(hh.DateIN) = " +
                            "YEAR(h.DateIN)  AND MONTH(hh.DateIN) =  MONTH(h.DateIN) " + 
                            "AND DAY(hh.DateIN) =  DAY(h.DateIN))";
                        continue;
                    }
                    query += "(SELECT COUNT(*) FROM History hh WHERE hh.SoftwareID=" +
                        "(SELECT SoftwareID FROM Software s WHERE s.Code=" +
                        "'" + unicLicenseNames[i] + "') AND YEAR(hh.DateIN) = " +
                        "YEAR(h.DateIN)  AND MONTH(hh.DateIN) =  MONTH(h.DateIN) " + 
                        "AND DAY(hh.DateIN) =  DAY(h.DateIN)),";
                }
                query += "FROM History h WHERE h.TimeIN IS NOT NULL GROUP " + 
                    "BY DAY(h.DateIN), MONTH(h.DateIN), YEAR(h.DateIN) ORDER BY " + 
                    "YEAR(h.DateIN), MONTH(h.DateIN), DAY(h.DateIN)";

                return query;
            }
            if (type.getType().Equals("hour"))
            {
                string query = "SELECT ";
                for (int i = 0; i < unicLicenseNames.Length; i++)
                {
                    if (i == (unicLicenseNames.Length - 1))
                    {
                        query += "(SELECT COUNT(*) FROM History hh WHERE hh.SoftwareID=" + 
                            "(SELECT SoftwareID FROM Software s WHERE s.Code='" + 
                            unicLicenseNames[i] + "') AND YEAR(hh.DateIN) = YEAR(h.DateIN)  " +
                            "AND MONTH(hh.DateIN) =  MONTH(h.DateIN) AND DAY(hh.DateIN) = " +
                            " DAY(h.DateIN) AND DATEPART(hour,hh.TimeIN) = " + 
                            "DATEPART(hour,h.TimeIN))";
                        continue;
                    }
                    query += "(SELECT COUNT(*) FROM History hh WHERE hh.SoftwareID=(SELECT " + 
                        "SoftwareID FROM Software s WHERE s.Code='" + 
                        unicLicenseNames[i] + "') AND YEAR(hh.DateIN) = " +
                        "YEAR(h.DateIN)  AND MONTH(hh.DateIN) =  MONTH(h.DateIN) " +
                        "AND DAY(hh.DateIN) =  DAY(h.DateIN) AND DATEPART(hour,hh.TimeIN) " + 
                        "= DATEPART(hour,h.TimeIN)),";
                }
                query += "FROM History h WHERE h.TimeIN IS NOT NULL GROUP BY " +
                    "DATEPART(hour,h.TimeIN), DAY(h.DateIN), MONTH(h.DateIN), " +
                    "YEAR(h.DateIN) ORDER BY YEAR(h.DateIN), MONTH(h.DateIN), " + 
                    "DAY(h.DateIN), DATEPART(hour,h.TimeIN)";

                return query;
            }
            if (type.getType().Equals("minute"))
            {
                string query = "SELECT ";
                for (int i = 0; i < unicLicenseNames.Length; i++)
                {
                    if (i == (unicLicenseNames.Length - 1))
                    {
                        query += "(SELECT COUNT(*) FROM History hh WHERE hh.SoftwareID=" +
                            "(SELECT SoftwareID FROM Software s WHERE s.Code=" +
                            "'" + unicLicenseNames[i] + "') AND YEAR(hh.DateIN) = " +
                            "YEAR(h.DateIN)  AND MONTH(hh.DateIN) =  MONTH(h.DateIN) " +
                            "AND DAY(hh.DateIN) =  DAY(h.DateIN) AND DATEPART(hour," +
                            "hh.TimeIN) = DATEPART(hour,h.TimeIN) AND DATEPART(minute," + 
                            "hh.TimeIN) = DATEPART(minute,h.TimeIN))";
                        continue;
                    }
                    query += "(SELECT COUNT(*) FROM History hh WHERE hh.SoftwareID=(SELECT" +
                        " SoftwareID FROM Software s WHERE s.Code=" +
                        "'" + unicLicenseNames[i] + "') AND YEAR(hh.DateIN) = " +
                        "YEAR(h.DateIN)  AND MONTH(hh.DateIN) =  MONTH(h.DateIN) AND " +
                        "DAY(hh.DateIN) =  DAY(h.DateIN) AND DATEPART(hour,hh.TimeIN) =" +
                        " DATEPART(hour,h.TimeIN) AND DATEPART(minute,hh.TimeIN) = " + 
                        "DATEPART(minute,h.TimeIN)),";
                }
                query += "FROM History h WHERE h.TimeIN IS NOT NULL GROUP BY " +
                    "DATEPART(minute,h.TimeIN), DATEPART(hour,h.TimeIN), DAY(h.DateIN)," +
                    " MONTH(h.DateIN), YEAR(h.DateIN) ORDER BY YEAR(h.DateIN), " +
                    "MONTH(h.DateIN), DAY(h.DateIN), DATEPART(hour,h.TimeIN), " + 
                    "DATEPART(minute,h.TimeIN)";

                return query;
            }
            if (type.getType().Equals("second"))
            {
                string query = "SELECT ";
                for (int i = 0; i < unicLicenseNames.Length; i++)
                {
                    if (i == (unicLicenseNames.Length - 1))
                    {
                        query += "(SELECT COUNT(*) FROM History hh WHERE hh.SoftwareID=" +
                            "(SELECT SoftwareID FROM Software s WHERE s.Code=" +
                            "'" + unicLicenseNames[i] + "') AND YEAR(hh.DateIN) = " +
                            "YEAR(h.DateIN)  AND MONTH(hh.DateIN) =  MONTH(h.DateIN) " +
                            "AND DAY(hh.DateIN) =  DAY(h.DateIN) AND DATEPART(hour," +
                            "hh.TimeIN) = DATEPART(hour,h.TimeIN) AND DATEPART(minute," +
                            "hh.TimeIN) = DATEPART(minute,h.TimeIN) AND DATEPART(" + 
                            "second,hh.TimeIN) = DATEPART(second,h.TimeIN))";
                        continue;
                    }
                    query += "(SELECT COUNT(*) FROM History hh WHERE hh." +
                        "SoftwareID=(SELECT SoftwareID FROM Software s WHERE " +
                        "s.Code='" + unicLicenseNames[i] + "') AND YEAR(hh.DateIN) = " +
                        "YEAR(h.DateIN)  AND MONTH(hh.DateIN) =  MONTH(h.DateIN) " +
                        "AND DAY(hh.DateIN) =  DAY(h.DateIN) AND DATEPART(" +
                        "hour,hh.TimeIN) = DATEPART(hour,h.TimeIN) AND " +
                        "DATEPART(minute,hh.TimeIN) = DATEPART(minute,h.TimeIN) " + 
                        "AND DATEPART(second,hh.TimeIN) = DATEPART(second,h.TimeIN)),";
                }
                query += "FROM History h WHERE h.TimeIN IS NOT NULL GROUP BY " +
                    "DATEPART(second,h.TimeIN), DATEPART(minute,h.TimeIN), " +
                    "DATEPART(hour,h.TimeIN), DAY(h.DateIN), MONTH(h.DateIN), " +
                    "YEAR(h.DateIN) ORDER BY YEAR(h.DateIN), MONTH(h.DateIN), " +
                    "DAY(h.DateIN), DATEPART(hour,h.TimeIN), " + 
                    "DATEPART(minute,h.TimeIN), DATEPART(second,h.TimeIN)";

                return query;
            }
            throw new UnknownTimeIntervalType("Unknown time interval type");
        }
        
        //Получение времени отдачи сервером лицензии
        public static string getTimesGiveLicense(string licenseName, GropByType type)
        {
            if (type.getType().Equals("year"))
            {
                return "SELECT (YEAR(h.DateIN)-1970)FROM History h WHERE h.DateIN is not " +
                    "null AND h.SoftwareID=(SELECT SoftwareID FROM Software s WHERE " +
                    "s.Code='" + licenseName + "') AND h.UserID!=(SELECT UserID " + 
                    "FROM Users u Where u.Name = 'RevitSystem' )ORDER BY YEAR(h.DateIN)";
            }
            if (type.getType().Equals("month"))
            {
                return "SELECT (YEAR(h.DateIN)-1970)*12+MONTH(h.DateIN) FROM " +
                    "History h WHERE h.DateIN is not null AND h.SoftwareID=(" +
                    "SELECT SoftwareID FROM Software s WHERE s.Code=" +
                    "'" + licenseName + "') AND h.UserID!=(SELECT UserID FROM " +
                    "Users u Where u.Name = 'RevitSystem' )ORDER BY YEAR(h.DateIN), " + 
                    "MONTH(h.DateIN)";
            }
            if (type.getType().Equals("day"))
            {
                return "SELECT (YEAR(h.DateIN)-1970)*360+MONTH(h.DateIN)*30+DAY(h.DateIN)" +
                    "FROM History h WHERE h.DateIN is not null AND h.SoftwareID=(" +
                    "SELECT SoftwareID FROM Software s WHERE s.Code=" +
                    "'" + licenseName + "') AND h.UserID!=(SELECT UserID FROM Users" +
                    " u Where u.Name = 'RevitSystem' )ORDER BY YEAR(h.DateIN), " + 
                    "MONTH(h.DateIN), DAY(h.DateIN)";
            }
            if (type.getType().Equals("hour"))
            {
                return "SELECT (YEAR(h.DateIN)-1970)*8640+MONTH(h.DateIN)*720+" +
                    "DAY(h.DateIN)*24+DATEPART(hour,h.TimeIN) FROM History h " +
                    "WHERE h.DateIN is not null AND h.SoftwareID=(SELECT SoftwareID " +
                    "FROM Software s WHERE s.Code=" +
                    "'" + licenseName + "') AND h.UserID!=(SELECT UserID FROM Users " +
                    "u Where u.Name = 'RevitSystem')ORDER BY YEAR(h.DateIN), " + 
                    "MONTH(h.DateIN), DAY(h.DateIN), DATEPART(hour,h.TimeIN)";
            }
            if (type.getType().Equals("minute"))
            {
                return "SELECT (YEAR(h.DateIN)-1970)*518400+MONTH(h.DateIN)*43200+" +
                    "DAY(h.DateIN)*1440+DATEPART(hour,h.TimeIN)*60+DATEPART(minute," +
                    "h.TimeIN)FROM History h WHERE h.DateIN is not null AND " +
                    "h.SoftwareID=(SELECT SoftwareID FROM Software s WHERE " +
                    "s.Code='" + licenseName + "') AND h.UserID!=(SELECT UserID " +
                    "FROM Users u Where u.Name = 'RevitSystem' )ORDER BY " +
                    "YEAR(h.DateIN), MONTH(h.DateIN), DAY(h.DateIN), DATEPART(" + 
                    "hour,h.TimeIN), DATEPART(minute,h.TimeIN)";
            }
            if (type.getType().Equals("second"))
            {
                return "SELECT (YEAR(h.DateIN)-1970)*3110400+MONTH(h.DateIN)*2592000+" +
                    "DAY(h.DateIN)*86400+DATEPART(hour,h.TimeIN)*3600+DATEPART" +
                    "(minute,h.TimeIN)*60+DATEPART(second,h.TimeIN)FROM History h " +
                    "WHERE h.DateIN is not null AND h.SoftwareID=(SELECT SoftwareID " +
                    "FROM Software s WHERE s.Code=" +
                    "'" + licenseName + "')AND h.UserID!=(SELECT UserID FROM Users " +
                    "u Where u.Name = 'RevitSystem')ORDER BY YEAR(h.DateIN), " +
                    "MONTH(h.DateIN), DAY(h.DateIN), DATEPART(hour,h.TimeIN), " + 
                    "DATEPART(minute,h.TimeIN), DATEPART(second,h.TimeIN)";
            }
            throw new UnknownTimeIntervalType("Unknown time interval type");
        }

        //Получение разницы во времени между получением и возвращением лицензии
        public static string getInBetweenOutLicenses(string licenseName, GropByType type)
        {
            if (type.getType().Equals("year"))
            {
                return "SELECT YEAR(h.DateExit)-YEAR(h.DateIN)FROM History h WHERE  " +
                    "h.DateIN is not null AND h.DateExit is not null AND h.SoftwareID=" +
                    "(SELECT SoftwareID FROM Software s WHERE s.Code=" +
                    "'" + licenseName + "') AND h.UserID!=(SELECT UserID FROM " +
                    "Users u Where u.Name = 'RevitSystem' ) AND YEAR(h.DateExit)-" + 
                    "YEAR(h.DateIN) != 0 ORDER BY YEAR(h.DateIN)";
            }
            if (type.getType().Equals("month"))
            {
                return "SELECT (YEAR(h.DateExit)-YEAR(h.DateIN))*12+MONTH(h.DateExit)-" +
                    "MONTH(h.DateIN)FROM History h WHERE  h.DateIN is not null AND " +
                    "h.DateExit is not null AND h.SoftwareID=(SELECT SoftwareID " +
                    "FROM Software s WHERE s.Code=" +
                    "'" + licenseName + "') AND h.UserID!=(SELECT UserID FROM " +
                    "Users u Where u.Name = 'RevitSystem' ) AND (YEAR(h.DateExit)-" +
                    "YEAR(h.DateIN))*12+MONTH(h.DateExit)-MONTH(h.DateIN) != 0 " + 
                    "ORDER BY YEAR(h.DateIN), MONTH(h.DateIN)";
            }
            if (type.getType().Equals("day"))
            {
                return "SELECT (YEAR(h.DateExit)-YEAR(h.DateIN))*360+(MONTH(h.DateExit)-" +
                    "MONTH(h.DateIN))*30+DAY(h.DateExit)-DAY(h.DateIN)FROM History " +
                    "h WHERE  h.DateIN is not null AND h.DateExit is not null AND " +
                    "h.SoftwareID=(SELECT SoftwareID FROM Software s WHERE s.Code=" +
                    "'" + licenseName + "') AND h.UserID!=(SELECT UserID FROM " +
                    "Users u Where u.Name = 'RevitSystem' ) AND (YEAR(h.DateExit)-" +
                    "YEAR(h.DateIN))*360+(MONTH(h.DateExit)-MONTH(h.DateIN))*30+" +
                    "DAY(h.DateExit)-DAY(h.DateIN) != 0 ORDER BY YEAR(h.DateIN), " + 
                    "MONTH(h.DateIN), DAY(h.DateIN)";
            }
            if (type.getType().Equals("hour"))
            {
                return "SELECT (YEAR(h.DateExit)-YEAR(h.DateIN))*8640+(MONTH(h.DateExit" +
                    ")-MONTH(h.DateIN))*720+(DAY(h.DateExit)-DAY(h.DateIN))*24+" +
                    "DATEPART(hour,h.TimeExit)-DATEPART(hour,h.TimeIN) FROM History " +
                    "h WHERE  h.DateIN is not null AND h.DateExit is not null AND " +
                    "h.SoftwareID=(SELECT SoftwareID FROM Software s WHERE s.Code=" +
                    "'" + licenseName + "') AND h.UserID!=(SELECT UserID FROM Users " +
                    "u Where u.Name = 'RevitSystem' ) AND (YEAR(h.DateExit)-YEAR(" +
                    "h.DateIN))*8640+(MONTH(h.DateExit)-MONTH(h.DateIN))*720+(DAY(" +
                    "h.DateExit)-DAY(h.DateIN))*24+DATEPART(hour,h.TimeExit)-" +
                    "DATEPART(hour,h.TimeIN)  != 0 ORDER BY YEAR(h.DateIN), " + 
                    "MONTH(h.DateIN), DAY(h.DateIN), DATEPART(hour,h.TimeIN)";
            }
            if (type.getType().Equals("minute"))
            {
                return "SELECT (YEAR(h.DateExit)-YEAR(h.DateIN))*518400+(MONTH(h.DateExit)-" +
                    "MONTH(h.DateIN))*720+(DAY(h.DateExit)-DAY(h.DateIN))*1440+" +
                    "(DATEPART(hour,h.TimeExit)-DATEPART(hour,h.TimeIN))*60+" +
                    "DATEPART(minute,h.TimeExit)-DATEPART(minute,h.TimeIN)  " +
                    "FROM History h WHERE  h.DateIN is not null AND h.DateExit is not " +
                    "null AND h.SoftwareID=(SELECT SoftwareID FROM Software s WHERE " +
                    "s.Code='" + licenseName + "') AND h.UserID!=(SELECT UserID FROM " +
                    "Users u Where u.Name = 'RevitSystem' ) AND (YEAR(h.DateExit)-YEAR" +
                    "(h.DateIN))*518400+(MONTH(h.DateExit)-MONTH(h.DateIN))*720+(" +
                    "DAY(h.DateExit)-DAY(h.DateIN))*1440+(DATEPART(hour,h.TimeExit)-" +
                    "DATEPART(hour,h.TimeIN))*60+DATEPART(minute,h.TimeExit)-" +
                    "DATEPART(minute,h.TimeIN) != 0 ORDER BY YEAR(h.DateIN), " +
                    "MONTH(h.DateIN), DAY(h.DateIN), DATEPART(hour,h.TimeIN), " + 
                    "DATEPART(minute,h.TimeIN)";
            }
            if (type.getType().Equals("second"))
            {
                return "SELECT (YEAR(h.DateExit)-YEAR(h.DateIN))*31104000+" +
                    "(MONTH(h.DateExit)-MONTH(h.DateIN))*2592000+(DAY(h.DateExit)-" +
                    "DAY(h.DateIN))*86400+(DATEPART(hour,h.TimeExit)-DATEPART(hour," +
                    "h.TimeIN))*3600+(DATEPART(minute,h.TimeExit)-DATEPART(minute," +
                    "h.TimeIN))*60+DATEPART(second,h.TimeExit)-DATEPART(second,h.TimeIN)" +
                    "    FROM History h WHERE  h.DateIN is not null AND h.DateExit " +
                    "is not null AND h.SoftwareID=(SELECT SoftwareID FROM Software s " +
                    "WHERE s.Code='" + licenseName + "') AND h.UserID!=(SELECT " +
                    "UserID FROM Users u Where u.Name = 'RevitSystem' ) AND " +
                    "(YEAR(h.DateExit)-YEAR(h.DateIN))*31104000+(MONTH(h.DateExit)-" +
                    "MONTH(h.DateIN))*2592000+(DAY(h.DateExit)-DAY(h.DateIN))*" +
                    "86400+(DATEPART(hour,h.TimeExit)-DATEPART(hour,h.TimeIN))*" +
                    "3600+(DATEPART(minute,h.TimeExit)-DATEPART(minute,h.TimeIN))*" +
                    "60+DATEPART(second,h.TimeExit)-DATEPART(second,h.TimeIN) != 0 " +
                    "ORDER BY YEAR(h.DateIN), MONTH(h.DateIN), DAY(h.DateIN), " +
                    "DATEPART(hour,h.TimeIN), DATEPART(minute,h.TimeIN), " + 
                    "DATEPART(second,h.TimeIN)";
            }
            throw new UnknownTimeIntervalType("Unknown time interval type");
        }

        //Получение данных о количестве полученных лицензий за определенный промежуток времени
        public static string getNumberOfLicenesForTime(string licenseName, GropByType type)
        {
            if (type.getType().Equals("year"))
            {
                return "SELECT COUNT(*)FROM History h WHERE YEAR(h.DateIN) != 0 AND YEAR(h.DateExit) != 0 AND h.SoftwareID=(SELECT SoftwareID FROM Software s WHERE s.Code='85787BDSPRM_F')GROUP BY YEAR(h.DateIN)";
            }
            if (type.getType().Equals("month"))
            {
                return "SELECT COUNT(*)FROM History h WHERE YEAR(h.DateIN) != 0 AND YEAR(h.DateExit) != 0 AND h.SoftwareID=(SELECT SoftwareID FROM Software s WHERE s.Code='85787BDSPRM_F')GROUP BY YEAR(h.DateIN), MONTH(h.DateIN)";
            }
            if (type.getType().Equals("day"))
            {
                return "SELECT COUNT(*)FROM History h WHERE YEAR(h.DateIN) != 0 AND YEAR(h.DateExit) != 0 AND h.SoftwareID=(SELECT SoftwareID FROM Software s WHERE s.Code='85787BDSPRM_F')GROUP BY YEAR(h.DateIN), MONTH(h.DateIN), DAY(h.DateIN)";
            }
            if (type.getType().Equals("hour"))
            {
                return "SELECT COUNT(*) FROM History h WHERE YEAR(h.DateIN) != 0 AND YEAR(h.DateExit) != 0 AND h.SoftwareID=(SELECT SoftwareID FROM Software s WHERE s.Code='85787BDSPRM_F')GROUP BY YEAR(h.DateIN), MONTH(h.DateIN), DAY(h.DateIN), DATEPART(hour,h.TimeIN)";
            }
            if (type.getType().Equals("minute"))
            {
                return "SELECT COUNT(*) FROM History h WHERE YEAR(h.DateIN) != 0 AND YEAR(h.DateExit) != 0 AND h.SoftwareID=(SELECT SoftwareID FROM Software s WHERE s.Code='85787BDSPRM_F')GROUP BY YEAR(h.DateIN), MONTH(h.DateIN), DAY(h.DateIN), DATEPART(hour,h.TimeIN), DATEPART(minute,h.TimeIN)";
            }
            if (type.getType().Equals("second"))
            {
                return "SELECT COUNT(*) FROM History h WHERE YEAR(h.DateIN) != 0 AND YEAR(h.DateExit) != 0 AND h.SoftwareID=(SELECT SoftwareID FROM Software s WHERE s.Code='85787BDSPRM_F')GROUP BY YEAR(h.DateIN), MONTH(h.DateIN), DAY(h.DateIN), DATEPART(hour,h.TimeIN), DATEPART(minute,h.TimeIN), DATEPART(second,h.TimeIN)";
            }
            throw new UnknownTimeIntervalType("Unknown time interval type");
        }

        //Обновление данных о количестве лицензий
        public static string updateCountOfLicense(string licenseName, double newCount)
        {
            return "UPDATE Software SET NumberOfPurchased="+ newCount.ToString().Replace(',','.') + 
                " WHERE Code='" + licenseName + "'";
        }
        //Обновление данных о распределнии бюджета на покупку лицензий
        public static string updatePartInPersentOfPurchasedLicenses(string licenseName,
            double newPersent)
        {
            return "UPDATE Software SET AmountOfInvestments="+
                newPersent.ToString().Replace(',', '.') +
                " WHERE Code='" + licenseName + "'";
        }
    }
}
