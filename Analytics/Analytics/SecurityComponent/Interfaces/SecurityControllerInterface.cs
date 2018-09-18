using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.SecurityComponent.Interfaces
{
    interface SecurityControllerInterface
    {
        void signIn();
        void changeUserPassword(string oldPassword, string newPassword);
        void addNewUser(string login, string password, bool isAdmin);
        void setConfig(string login, string password);
        void setConfig(string login, string password, bool isAdmin);
    }
}
