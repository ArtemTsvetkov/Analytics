using Analytics.CommonComponents.DataConverters;
using Analytics.CommonComponents.Interfaces.Data;
using Analytics.CommonComponents.MsSqlServersQueryConfigurator;
using Analytics.CommonComponents.WorkWithDataBase.MsSqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.HandModifiedDataPanel
{
    class StoreLoader
    {
        public HandModifiedDataState loadData()
        {
            HandModifiedDataState state = new HandModifiedDataState();
            //Получение уникальных имен лицензий
            DataConverter<DataSet, string[]> unucNamesConverter =
            new DistinctSoftwareNamesConverter();
            DataSet ds = configProxyForLoadDataFromBDAndExecute(
            MsSqlServersQueryConfigurator.getUnicLicensesName());
            state.unicSoftwareNames = unucNamesConverter.convert(ds);

            //Число закупленных лицензий читается из таблицы PurchasedLicenses
            ds = configProxyForLoadDataFromBDAndExecute(
                MsSqlServersQueryConfigurator.getNumberOfPurchasedLicenses());
            DataTable table = ds.Tables[0];
            state.numberOfPurcharedLicenses = new double[state.unicSoftwareNames.Count()];
            for (int i = 0; i < state.unicSoftwareNames.Count(); i++)
            {
                state.numberOfPurcharedLicenses[i] = int.Parse(table.Rows[i][1].ToString());
            }

            //Распределение в процентах закупленных лицензий
            ds = configProxyForLoadDataFromBDAndExecute(
                    MsSqlServersQueryConfigurator.getPartsInPersentOfPurchasedLicenses());
            table = ds.Tables[0];
            state.percents = new double[state.unicSoftwareNames.Count()];
            for (int i = 0; i < state.unicSoftwareNames.Count(); i++)
            {
                state.percents[i] = double.Parse(table.Rows[i][1].ToString());
            }

            return state;
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
