using Analytics.SecurityComponent.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.SecurityComponent
{
    class SecurityMsSqlServerQueryConfigurator : SecurityQueryConfigurator
    {
        public string addNewUser(string login, string password, string sult, bool isAdmin)
        {
            if (isAdmin)
            {
                return "INSERT INTO UST VALUES('" + login + "', '" + password +
                    "', '" + sult + "' ,1)";
            }
            else
            {
                return "INSERT INTO UST VALUES('" + login + "', '" + password +
                    "', '" + sult + "' ,0)";
            }
        }

        public string changePassword(string login, string newPassword)
        {
            return "UPDATE UST SET Password='"+ newPassword + 
                "' WHERE Login='" + login + "';";
        }

        public string checkUser(string login, string password)
        {
            return "SELECT COUNT(*) FROM UST WHERE Login='"+ login + 
                "' AND Password='" + password + "';";
        }

        public string checkUserStatus(string login)
        {
            return "SELECT Status FROM UST WHERE Login='"+ login + "'";
        }
    }
}
