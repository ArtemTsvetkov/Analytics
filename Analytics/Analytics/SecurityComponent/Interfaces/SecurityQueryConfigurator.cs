using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.SecurityComponent.Interfaces
{
    interface SecurityQueryConfigurator
    {
        string checkUser(string login, string password);
        string changePassword(string login, string newPassword);
        string checkUserStatus(string login);
        string addNewUser(string login, string password, string sult, bool isAdmin);
        string getSult(string login);
    }
}
