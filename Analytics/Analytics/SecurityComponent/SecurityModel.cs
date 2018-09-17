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
            throw new NotImplementedException();
        }

        public void changeUserPassword(string login, string newPassword)
        {
            throw new NotImplementedException();
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
            if(converter.convert(configProxyForLoadDataFromBDAndExecute(
                queryConfigurator.checkUser(
                    currentUser.getLogin(), currentUser.getPassword()))) == 1)
            {
                currentUser.setEnterIntoSystem(true);

                if (converter.convert(configProxyForLoadDataFromBDAndExecute(
                    queryConfigurator.checkUserStatus(currentUser.getLogin()))) == 1)
                {
                    currentUser.setAdmin(true);
                }

                notifyObservers();
            }
            else
            {
                //ADD INTO THIS PLACE EXCEPTION: INCORRECT USER DATA
            }
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
    }
}
