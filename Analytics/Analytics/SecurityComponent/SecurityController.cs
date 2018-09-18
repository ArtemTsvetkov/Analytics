using Analytics.SecurityComponent;
using Analytics.SecurityComponent.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.SecurityComponent
{
    class SecurityController : SecurityControllerInterface
    {
        private SecurityModel model;

        public SecurityController(SecurityModel model)
        {
            this.model = model;
        }

        public void addNewUser(string login, string password, bool isAdmin)
        {
            if (model.checkUser())
            {
                SecurityUserInterface user = new SecurityUser(login, password);
                user.setAdmin(isAdmin);

                model.addNewUser(user);
            }
            else
            {
                //ADD EXCEPTION: INCORRECT USERS DATA
            }
        }

        public void changeUserPassword(string oldPassword, string newPassword)
        {
            model.changeUserPassword(oldPassword, newPassword);
        }

        public void setConfig(string login, string password, bool isAdmin)
        {
            SecurityUserInterface user = new SecurityUser(login, password);
            user.setAdmin(isAdmin);

            model.setConfig(user);
        }

        public void setConfig(string login, string password)
        {
            SecurityUserInterface user = new SecurityUser(login, password);
            model.setConfig(user);
        }

        public void signIn()
        {
            if (model.checkUser())
            {
                model.signIn();
            }
            else
            {
                //ADD EXCEPTION: INCORRECT USERS DATA
            }
        }
    }
}
