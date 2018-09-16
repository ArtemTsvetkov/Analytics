using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.SecurityComponent
{
    interface SecurityModelInterface<TStatesType>
    {
        void setCurrentUser(SecurityUserInterface user);
        void signIn();
        void addNewUser(SecurityUserInterface user);
        void changeUserPassword(string login, string newPassword);
        void changeUserStatus(string login, bool isAdmin);
        TStatesType getData();
        void notifyObservers();
    }
}
