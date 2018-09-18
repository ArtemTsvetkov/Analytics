using Analytics.CommonComponents.BasicObjects;
using Analytics.CommonComponents.DataConverters;
using Analytics.CommonComponents.Interfaces.Data;
using Analytics.CommonComponents.WorkWithDataBase.MsSqlServer;
using Analytics.SecurityComponent.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.SecurityComponent
{
    class SecurityModel : BasicModel<SecurityUserInterface, SecurityUserInterface>,
        SecurityModelInterface<SecurityUserInterface>
    {
        private SecurityQueryConfigurator queryConfigurator;
        private DataConverter<DataSet, int> converter =
            new FromDataSetToIntDataConverter();
        private SecurityUserInterface currentUser;

        public SecurityModel()
        {
            queryConfigurator = new SecurityMsSqlServerQueryConfigurator();
        }

        public void addNewUser(SecurityUserInterface user)
        {
            if (currentUser.isAdmin())
            {
                configProxyForLoadDataFromBDAndExecute(queryConfigurator.addNewUser(
                    user.getLogin(), user.getPassword(), "SULT", user.isAdmin()));
            }
            else
            {
                //ADD EXCEPTION: CURRENT USER IS NOT A ADMIN.
            }
        }

        public void changeUserPassword(string oldPassword, string newPassword)
        {
            if (converter.convert(configProxyForLoadDataFromBDAndExecute(
                queryConfigurator.checkUser(
                    currentUser.getLogin(), oldPassword))) == 1)
            {
                configProxyForLoadDataFromBDAndExecute(
                    queryConfigurator.changePassword(currentUser.getLogin(), newPassword));
                currentUser.setPassword(newPassword);

                notifyObservers();
            }
            else
            {
                //ADD exception: incorrect oldpassword!
            }
        }

        public override SecurityUserInterface getResult()
        {
            return currentUser.copy();
        }

        public override void loadStore()
        {
            
        }

        public override void setConfig(SecurityUserInterface configData)
        {
            currentUser = configData.copy();
        }

        public void signIn()
        {
            currentUser.setEnterIntoSystem(true);

            if (converter.convert(configProxyForLoadDataFromBDAndExecute(
                queryConfigurator.checkUserStatus(currentUser.getLogin()))) == 1)
            {
                currentUser.setAdmin(true);
            }

            notifyObservers();
        }

        private DataSet configProxyForLoadDataFromBDAndExecute(string query)
        {
            DataWorker<MsSQLServerStateFields, DataSet> accessProxy = new MsSQLServerProxy();
            List<string> list = new List<string>();
            list.Add(query);
            MsSQLServerStateFields configProxy =
                new MsSQLServerStateFields(list);
            accessProxy.setConfig(configProxy);
            accessProxy.execute();
            list.Clear();
            return accessProxy.getResult();
        }

        public bool checkUser()
        {
            if (currentUser.isEnterIntoSystem())
            {
                return true;
            }
            else
            {
                if (converter.convert(configProxyForLoadDataFromBDAndExecute(
                    queryConfigurator.checkUser(
                        currentUser.getLogin(), currentUser.getPassword()))) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
