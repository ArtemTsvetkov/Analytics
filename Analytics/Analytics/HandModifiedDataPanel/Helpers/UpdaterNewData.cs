using Analytics.CommonComponents.Interfaces.Data;
using Analytics.CommonComponents.MsSqlServersQueryConfigurator;
using Analytics.CommonComponents.WorkWithDataBase.MsSqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.HandModifiedDataPanel.Helpers
{
    class UpdaterNewData
    {
        private HandModifiedDataState state;

        public UpdaterNewData(HandModifiedDataState state)
        {
            this.state = state;
        }

        public void update()
        {
            for(int i=0; i<state.unicSoftwareNames.Count(); i++)
            {
                configProxyForLoadDataFromBDAndExecute(
                    MsSqlServersQueryConfigurator.updateCountOfLicense(
                        state.unicSoftwareNames[i], state.numberOfPurcharedLicenses[i]));
                configProxyForLoadDataFromBDAndExecute(
                    MsSqlServersQueryConfigurator.updatePartInPersentOfPurchasedLicenses(
                        state.unicSoftwareNames[i], state.percents[i]));
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
