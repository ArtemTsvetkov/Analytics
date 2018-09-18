using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.SecurityComponent
{
    class SecurityUser : SecurityUserInterface
    {
        private string login;
        private string password;
        private bool admin;
        private bool enterIntoSystem;

        public SecurityUser(string login, string password)
        {
            this.login = login;
            this.password = password;
            admin = false;
            enterIntoSystem = false;
        }

        public void setPassword(string password)
        {
            this.password = password;
        }

        public void setAdmin(bool isAdmin)
        {
            admin = isAdmin;
        }

        public void setEnterIntoSystem(bool isEnter)
        {
            enterIntoSystem = isEnter;
        }

        public string getLogin()
        {
            return login;
        }

        public string getPassword()
        {
            return password;
        }

        public bool isAdmin()
        {
            return admin;
        }

        public bool isEnterIntoSystem()
        {
            return enterIntoSystem;
        }

        public SecurityUserInterface copy()
        {
            SecurityUserInterface copyUser = new SecurityUser(login,password);
            copyUser.setAdmin(isAdmin());
            copyUser.setEnterIntoSystem(isEnterIntoSystem());

            return copyUser;
        }
    }
}
