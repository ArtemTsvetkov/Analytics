using Analytics.CommonComponents;
using Analytics.CommonComponents.BasicObjects;
using Analytics.CommonComponents.BasicObjects.Statistics;
using Analytics.CommonComponents.DataConverters;
using Analytics.CommonComponents.ExceptionHandler;
using Analytics.CommonComponents.Interfaces.Data;
using Analytics.CommonComponents.Math;
using Analytics.CommonComponents.MsSqlServersQueryConfigurator;
using Analytics.CommonComponents.WorkWithDataBase.MsSqlServer;
using Analytics.MarcovitsComponent.Config;
using Analytics.MarcovitsComponent.Exceptions;
using Analytics.Modeling.GroupByTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class MarcovitsModel : BasicStatisticsModel<MarcovitsModelState, MarcovitsConfig>
    {
        private MarcovitsModelState state;
        DataConverter<DataSet, List<MarcovitsDataTable>> converter = 
            new MarcovitsDataTableConverter();
        DataConverter<DataSet, string[]> unucNamesConverter = 
            new DistinctSoftwareNamesConverter();

        public MarcovitsModel()
        {
            state = new MarcovitsModelState();
            DataSet ds = configProxyForLoadDataFromNewBDAndExecute(
                MsSqlServersQueryConfigurator.getUnicLicensesName());
            string[] newnames = unucNamesConverter.convert(ds);
        }

        public override void calculationStatistics()
        {
            try
            {
                //Получение уникальных имен лицензий
                /*DataSet ds = configProxyForLoadDataFromNewBDAndExecute(
                MsSqlServersQueryConfigurator.getUnicLicensesName());
                state.unicSoftwareNames = unucNamesConverter.convert(ds);*/
                state.unicSoftwareNames = (string[])config.UnicSoftwareNames.Clone();
                if(state.unicSoftwareNames.Count() < 2)
                {
                    throw new NotEnoughDataToAnalyze("Not enough data to analyze");
                }
                //Формирование запроса на получение данных
                DataSet ds = configProxyForLoadDataFromNewBDAndExecute(MsSqlServersQueryConfigurator.
                    getDataOfUseAllLicenses(state.unicSoftwareNames, config.getInterval()));
                state.data = converter.convert(ds);
                if (state.data.Count < 5)
                {
                    throw new NotEnoughDataToAnalyze("Not enough data to analyze");
                }


                //Рассчет средних значений кол-ва лицензий
                state.avgNumbersUseLicense = new double[state.unicSoftwareNames.Length];
                for (int i = 0; i < state.data.Count; i++)
                {
                    for (int j = 0; j < state.data.ElementAt(i).licenses.Length; j++)
                    {
                        state.avgNumbersUseLicense[j] += state.data.ElementAt(i).licenses[j];
                    }
                }
                for (int j = 0; j < state.avgNumbersUseLicense.Length; j++)
                {
                    state.avgNumbersUseLicense[j] = state.avgNumbersUseLicense[j] / state.data.Count;
                }

                //Число закупленных лицензий читается из таблицы PurchasedLicenses
                /*ds = configProxyForLoadDataFromNewBDAndExecute(
                    MsSqlServersQueryConfigurator.getNumberOfPurchasedLicenses());
                DataTable table = ds.Tables[0];
                state.numberBuyLicense = new double[state.unicSoftwareNames.Count()];
                for (int i = 0; i < state.unicSoftwareNames.Count(); i++)
                {
                    state.numberBuyLicense[i] = int.Parse(table.Rows[i][1].ToString());
                }*/
                state.numberBuyLicense = (double[])config.NumberOfPurcharedLicenses.Clone();
                double licenseCount = 0;
                for (int i=0; i<state.numberBuyLicense.Length; i++)
                {
                    if(state.numberBuyLicense[i] <= 0)
                    {
                        throw new NotEnoughDataToAnalyze("Not enough data to analyze. Count of " +
                            "purchased license must have more than zero. Plese check it " + 
                            "(Меню/Закупленные лицензии). For exclude one or more license " +
                            "types from calculating please set zero in table with the " +
                            "percentage of purchased licenses (Меню/Закупленные лицензии) in " +
                            "row(rows) with this license types.");
                    }
                    licenseCount += state.numberBuyLicense[i];
                }
                if (licenseCount <= 0)
                {
                    throw new NotEnoughDataToAnalyze("Not enough data to analyze. Please, fill "+
                        "number of licenses purchased (Меню/Закупленные лицензии)");
                }

                //Расчет разницы между кол-вом закупленных и текущих лицензий
                for (int i = 0; i < state.data.Count; i++)
                {
                    for (int j = 0; j < state.data.ElementAt(i).licenses.Length; j++)
                    {
                        state.data.ElementAt(i).licenses[j] = (state.data.ElementAt(i).licenses[j] -
                            state.numberBuyLicense[j]) / state.numberBuyLicense[j];
                    }
                }

                state.avgDeviationFromPurchasedNumber = new double[state.unicSoftwareNames.Length];
                //расчет ковариации
                double[,] covarMas = new double[state.unicSoftwareNames.Length, state.unicSoftwareNames.
                    Length];
                for (int i = 0; i < state.unicSoftwareNames.Length; i++)
                {
                    for (int j = 0; j < state.unicSoftwareNames.Length; j++)
                    {
                        double[] matrixA = new double[state.data.Count];
                        double[] matrixB = new double[state.data.Count];
                        for (int m = 0; m < state.data.Count; m++)
                        {
                            matrixA[m] = state.data.ElementAt(m).licenses[i];
                            matrixB[m] = state.data.ElementAt(m).licenses[j];
                        }
                        covarMas[i, j] = MathWorker.covar(matrixA, matrixB);

                        //Для рассчета доходности считаю доходность по каждой отдельной лицензии
                        state.avgDeviationFromPurchasedNumber[i] = (1 - Math.Abs(MathWorker.avg(matrixA)));
                    }
                }
                //Cоотношения в процентах читается из таблицы PercentageOfLicense
                /*ds = configProxyForLoadDataFromNewBDAndExecute(
                    MsSqlServersQueryConfigurator.getPartsInPersentOfPurchasedLicenses());
                DataTable table = ds.Tables[0];
                state.percents = new double[state.unicSoftwareNames.Count(), 1];
                for (int i = 0; i < state.unicSoftwareNames.Count(); i++)
                {
                    state.percents[i, 0] = double.Parse(table.Rows[i][1].ToString());
                }*/
                state.percents = new double[state.unicSoftwareNames.Count(), 1];
                for (int i = 0; i < state.unicSoftwareNames.Count(); i++)
                {
                    state.percents[i, 0] = config.Percents[i];
                }
                double partsSum = 0;
                for (int i = 0; i < state.percents.Length; i++)
                {
                    partsSum += state.percents[i, 0];
                }
                if (partsSum <= 0 || partsSum > 1)
                {
                    throw new NotEnoughDataToAnalyze("Not enough data to analyze. " +
                        "Please fill in the ratio of purchased licenses as a percentage" + " (Меню/Закупленные лицензии)");
                }
                //Подсчет общего риска
                state.risk = MathWorker.multiplyMatrix(covarMas, state.percents);

                double[,] transpPercents = new double[1, state.percents.Length];
                for (int i = 0; i < state.percents.Length; i++)
                {
                    transpPercents[0, i] = state.percents[i, 0];
                }

                state.risk = MathWorker.multiplyMatrix(transpPercents, state.risk);

                //Подсчет общего дохода
                state.income = 0;
                for (int i = 0; i < state.avgDeviationFromPurchasedNumber.Length; i++)
                {
                    state.income += state.avgDeviationFromPurchasedNumber[i] * state.percents[i, 0];
                }

                notifyObservers();
            }
            catch(Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }

        public DataSet configProxyForLoadDataFromNewBDAndExecute(string query)
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

        public override void loadStore()//загрузка данных
        {
            DataWorker<MsSQLServerStateFields, DataSet> accessProxy = new MsSQLServerProxy();
            //получение значения id
            DataSet ds = configProxyForLoadDataFromNewBDAndExecute(
                MsSqlServersQueryConfigurator.getDataOfUsersUseLicenses());
            state.data = converter.convert(ds);
            notifyObservers();
        }

        public override ModelsState copySelf()
        {
            MarcovitsModelState copy = new MarcovitsModelState();
            copy.income = state.income;
            copy.interval = state.interval;

            if (state.unicSoftwareNames != null)
            {
                copy.unicSoftwareNames = (string[])state.unicSoftwareNames.Clone();
            }

            if (state.avgNumbersUseLicense != null)
            {
                copy.avgNumbersUseLicense = (double[])state.avgNumbersUseLicense.Clone();
            }

            if (state.avgDeviationFromPurchasedNumber != null)
            {
                copy.avgDeviationFromPurchasedNumber = (double[])state.
                    avgDeviationFromPurchasedNumber.Clone();
            }

            if (state.numberBuyLicense != null)
            {
                copy.numberBuyLicense = (double[])state.numberBuyLicense.Clone();
            }

            if (state.percents != null)
            {
                copy.percents = (double[,])state.percents.Clone();
            }

            if (state.risk != null)
            {
                copy.risk = (double[,])state.risk.Clone();
            }

            for(int i=0; i<state.data.Count; i++)
            {
                copy.data.Add(state.data.ElementAt(i).copy());
            }

            return copy;
        }

        public override void recoverySelf(ModelsState oldState)
        {
            MarcovitsModelState oldMarcovitsState = (MarcovitsModelState)oldState;
            state.income = oldMarcovitsState.income;
            state.interval = oldMarcovitsState.interval;

            if (oldMarcovitsState.unicSoftwareNames != null)
            {
                state.unicSoftwareNames = (string[])oldMarcovitsState.unicSoftwareNames.Clone();
            }

            if (oldMarcovitsState.avgNumbersUseLicense != null)
            {
                state.avgNumbersUseLicense = (double[])oldMarcovitsState.avgNumbersUseLicense.Clone();
            }

            if (oldMarcovitsState.avgDeviationFromPurchasedNumber != null)
            {
                state.avgDeviationFromPurchasedNumber = (double[])oldMarcovitsState.
                    avgDeviationFromPurchasedNumber.Clone();
            }

            if (oldMarcovitsState.numberBuyLicense != null)
            {
                state.numberBuyLicense = (double[])oldMarcovitsState.numberBuyLicense.Clone();
            }

            if (oldMarcovitsState.percents != null)
            {
                state.percents = (double[,])oldMarcovitsState.percents.Clone();
            }

            if (oldMarcovitsState.risk != null)
            {
                state.risk = (double[,])oldMarcovitsState.risk.Clone();
            }

            for (int i = 0; i < oldMarcovitsState.data.Count; i++)
            {
                state.data.Add(oldMarcovitsState.data.ElementAt(i).copy());
            }

            notifyObservers();
        }

        public override void setConfig(MarcovitsConfig configData)
        {
            config = configData.copy();
            state.interval = config.getInterval();
            state.income = 0;
            state.risk[0, 0] = 0;
            if (configData.NotifyObservers)
            {
                notifyObservers();
            }
            else
            {
                configData.NotifyObservers = true;
            }
        }

        public override MarcovitsModelState getResult()
        {
            return state;
        }

        public override MarcovitsConfig getConfig()
        {
            return config.copy();
        }
    }
}
