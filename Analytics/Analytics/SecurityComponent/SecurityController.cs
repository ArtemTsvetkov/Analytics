using Analytics.CommonComponents.ExceptionHandler;
using Analytics.CommonComponents.Exceptions.Security;
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
            try
            {
                if (model.checkUser())
                {
                    SecurityUserInterface user = new SecurityUser(login, password);
                    user.setAdmin(isAdmin);

                    model.addNewUser(user);
                }
                else
                {
                    throw new InsufficientPermissionsException("This user does not"
                        + "have sufficient rights to perform the specified operation");
                }
            }
            catch(Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }

        public void changeUserPassword(string oldPassword, string newPassword)
        {
            try
            {
                model.changeUserPassword(oldPassword, newPassword);
            }
            catch (Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
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
            try
            {
                if (model.checkUser())
                {
                    model.signIn();
                }
                else
                {
                    throw new IncorrectUserData("Invalid login or password");
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }
    }
}
