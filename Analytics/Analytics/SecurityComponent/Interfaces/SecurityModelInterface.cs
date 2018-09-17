using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.SecurityComponent
{
    interface SecurityModelInterface<TTypeOfResult>
    {
        void signIn();
        void addNewUser(SecurityUserInterface user);
        void changeUserPassword(string oldPassword, string newPassword);
        bool checkUser();
    }
}
