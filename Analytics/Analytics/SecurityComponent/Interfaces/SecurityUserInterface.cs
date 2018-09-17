using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.SecurityComponent
{
    interface SecurityUserInterface
    {
        string getPassword();
        string getLogin();
        bool isAdmin();
        bool isEnterIntoSystem();
        void setAdmin(bool isAdmin);
        void setEnterIntoSystem(bool isEnter);
        SecurityUserInterface copy();
    }
}
