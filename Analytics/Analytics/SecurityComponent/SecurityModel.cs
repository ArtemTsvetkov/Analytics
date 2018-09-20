using Analytics.CommonComponents.BasicObjects;
using Analytics.CommonComponents.DataConverters;
using Analytics.CommonComponents.Exceptions.Security;
using Analytics.CommonComponents.Interfaces.Data;
using Analytics.CommonComponents.WorkWithDataBase.MsSqlServer;
using Analytics.SecurityComponent.Hash;
using Analytics.SecurityComponent.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
        private HashWorkerInterface<HashConfig> hashWorker;

        public SecurityModel()
        {
            queryConfigurator = new SecurityMsSqlServerQueryConfigurator();
            hashWorker = new HashWorker();
            HashConfig hc = new HashConfig();
            hc.numberOfHashing = 100000;
            hc.sultLength = 20;
            hashWorker.setConfig(hc);
        }

        public void addNewUser(SecurityUserInterface user)
        {
            string sult = hashWorker.getSult(user);


            if (currentUser.isAdmin())
            {
                configProxyForLoadDataFromBDAndExecute(queryConfigurator.addNewUser(
                    user.getLogin(), hashWorker.getHash(user.getPassword(),sult), sult, user.isAdmin()));
            }
            else
            {
                throw new InsufficientPermissionsException("This user does not"
                    + "have sufficient rights to perform the specified operation");
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
                throw new IncorrectOldPassword("Exception: old password is not a right");
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
                FromDataSetToString newConverter = new FromDataSetToString();
                if(newConverter.convert(configProxyForLoadDataFromBDAndExecute(
                        queryConfigurator.getSult(
                        currentUser.getLogin()))) == null)
                {
                    return false;
                }

                string currentSult = newConverter.convert(configProxyForLoadDataFromBDAndExecute(
                    queryConfigurator.getSult(
                    currentUser.getLogin())));
                //MS Sql Server дописывает пробелы в конец, их нужно убрать
                bool goNext = false;
                while (goNext == false)
                {
                    if (currentSult.ElementAt(currentSult.Length - 1).Equals(' '))
                    {
                        currentSult = currentSult.Remove(currentSult.Length - 1);
                    }
                    else
                    {
                        goNext = true;
                    }
                }

                string currentPassword = hashWorker.getHash(currentUser.getPassword(), currentSult);
                

                if (converter.convert(configProxyForLoadDataFromBDAndExecute(
                        queryConfigurator.checkUser(
                        currentUser.getLogin(), currentPassword))) == 1)
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
