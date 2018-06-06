using Analytics.CommonComponents.BasicObjects;
using Analytics.CommonComponents.Interfaces.Data;
using Analytics.CommonComponents.WorkWithMSAccess;
using Analytics.MarcovitsComponent.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class MarcovitsModel : BasicModel<MarcovitsModelState, MarcovitsConfig>
    {
        private MarcovitsModelState state;
        DataConverter<DataSet, List<MarcovitsDataTable>> converter = 
            new MarcovitsDataTableConverter();
        DataConverter<DataSet, string[]> unucNamesConverter = 
            new MarcovitsDistinctSoftwareNamesConverter();

        public MarcovitsModel()
        {
            state = new MarcovitsModelState();
        }

        public override void calculationStatistics()
        {
            //Получение уникальных имен лицензий
            DataWorker<List<string>, string, DataSet> accessProxy = new MSAccessProxy();
            StorageForData<DataSet> newData = new MSAccessStorageForData();
            accessProxy.setConfig(config.getPathOfDataBase(), "SELECT DISTINCT software FROM " + config.
                getTableOfDataBase(), newData);
            accessProxy.execute();
            DataSet ds = newData.getData();
            state.unicSoftwareNames = unucNamesConverter.convert(ds);
            //Формирование запроса на получение данных
            string query = "SELECT  i.year_in, i.month_in, i.day_in, i.hours_in";
            for(int i=0; i<state.unicSoftwareNames.Length; i++)
            {
                query += ", (SELECT COUNT(*) FROM Information ii WHERE ii.software='"+ state.
                    unicSoftwareNames[i]+ "' AND ii.year_in = i.year_in  AND ii.month_in =  "+
                    "i.month_in AND ii.day_in =  i.day_in AND ii.hours_in = i.hours_in)";
            }
            query += "FROM Information i WHERE hours_in IS NOT NULL GROUP BY hours_in, day_in, "+
                "month_in, year_in ORDER BY year_in, month_in, day_in, hours_in";
            //Получение данных об использовании
            accessProxy.setConfig(config.getPathOfDataBase(), query, newData);
            accessProxy.execute();
            ds = newData.getData();
            state.data = converter.convert(ds);

            //Рассчет средних значений кол-ва лицензий
            state.avgNumbersUseLicense = new double[state.unicSoftwareNames.Length];
            for (int i=0; i< state.data.Count; i++)
            {
                for(int j=0; j<state.data.ElementAt(i).licenses.Length; j++)
                {
                    state.avgNumbersUseLicense[j] += state.data.ElementAt(i).licenses[j];
                }
            }
            for (int j = 0; j < state.avgNumbersUseLicense.Length; j++)
            {
                state.avgNumbersUseLicense[j] = state.avgNumbersUseLicense[j] / state.data.Count;        
            }

            //Пока для тестов число закупленных лицензий читается из таблицы PurchasedLicenses
            accessProxy.setConfig(config.getPathOfDataBase(), "SELECT type, count FROM PurchasedLicenses",
                newData);
            accessProxy.execute();
            ds = newData.getData();
            DataTable table = ds.Tables[0];
            state.numberBuyLicense = new double[state.unicSoftwareNames.Count()];
            for (int i=0;i<state.unicSoftwareNames.Count();i++)
            {
                state.numberBuyLicense[i] = int.Parse(table.Rows[i][1].ToString());
            }
            //Расчет разницы между кол-вом закупленных и текущих лицензий
            for (int i = 0; i < state.data.Count; i++)
            {
                for (int j = 0; j < state.data.ElementAt(i).licenses.Length; j++)
                {
                    state.data.ElementAt(i).licenses[j] = (state.data.ElementAt(i).licenses[j] - 
                        state.numberBuyLicense[j])/ state.numberBuyLicense[j];
                }
            }

            state.avgDeviationFromPurchasedNumber = new double[state.unicSoftwareNames.Length];
            //расчет ковариации
            double[,] covarMas = new double[state.unicSoftwareNames.Length, state.unicSoftwareNames.
                Length];
            for(int i=0; i< state.unicSoftwareNames.Length; i++)
            {
                for (int j = 0; j < state.unicSoftwareNames.Length; j++)
                {
                    double[] matrixA = new double[state.data.Count];
                    double[] matrixB = new double[state.data.Count];
                    for(int m=0; m< state.data.Count; m++)
                    {
                        matrixA[m] = state.data.ElementAt(m).licenses[i];
                        matrixB[m] = state.data.ElementAt(m).licenses[j];
                    }
                    covarMas[i, j] = covar(matrixA, matrixB);

                    //Для рассчета доходности считаю доходность по каждой отдельной лицензии
                    state.avgDeviationFromPurchasedNumber[i] = (1 - Math.Abs(avg(matrixA)));
                }
            }
            //Пока для тестов соотношения в процентах читается из таблицы PercentageOfLicense
            accessProxy.setConfig(config.getPathOfDataBase(), "SELECT type, percent FROM PercentageOf"+
                "License", newData);
            accessProxy.execute();
            ds = newData.getData();
            table = ds.Tables[0];
            state.percents = new double[state.unicSoftwareNames.Count(),1];
            for (int i = 0; i < state.unicSoftwareNames.Count(); i++)
            {
                state.percents[i,0] = double.Parse(table.Rows[i][1].ToString());
            }

            //Подсчет общего риска
            state.risk = MultiplyMatrix(covarMas, state.percents);

            double[,] transpPercents = new double[1, 5];
            for (int i = 0; i < 5; i++)
            {
                transpPercents[0, i] = state.percents[i, 0];
            }

            state.risk = MultiplyMatrix(transpPercents, state.risk);

            //Подсчет общего дохода
            state.income = 0;
            for (int i=0; i<state.avgDeviationFromPurchasedNumber.Length; i++)
            {
                state.income += state.avgDeviationFromPurchasedNumber[i] * state.percents[i,0];
            }

            notifyObservers();
        }

        public override void loadStore()//загрузка данных
        {
            DataWorker<List<string>, string, DataSet> accessProxy = new MSAccessProxy();
            StorageForData<DataSet> newData = new MSAccessStorageForData();
            //получение значения id
            accessProxy.setConfig(config.getPathOfDataBase(), "SELECT user_name, user_host, software FROM " +
                config.getTableOfDataBase(), newData);
            accessProxy.execute();
            DataSet ds = newData.getData();
            state.data = converter.convert(ds);
            notifyObservers();
        }

        double avg(double[] matrix)//Расчет среднего значения
        {
            double sum = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                sum += matrix[i];
            }
            return (sum / (double)matrix.Length);
        }

        double covar(double[] matrixA, double[] matrixB)//Расчет ковариации
        {
            double avgMatrixA = avg(matrixA);
            double avgMatrixB = avg(matrixB);

            if (matrixA.Length == matrixB.Length)
            {
                double kovar = 0;
                for (int i = 0; i < matrixA.Length; i++)
                {
                    kovar += (matrixA[i] - avgMatrixA) * (matrixB[i] - avgMatrixB);
                }
                kovar = kovar / (matrixA.Length - 1);
                return kovar;
            }
            else//Должно совпадать, иначе нет смысла сравнивать
            {
                throw new Exception();
            }
        }

        double[,] MultiplyMatrix(double[,] aMatrix, double[,] bMatrix)//Умножение матриц
        {
            if (aMatrix.GetLength(1) == bMatrix.GetLength(0))
            {
                double[,] product = new double[aMatrix.GetLength(0), bMatrix.GetLength(1)];


                for (int row = 0; row < aMatrix.GetLength(0); row++)
                {
                    for (int col = 0; col < bMatrix.GetLength(1); col++)
                    {
                        for (int inner = 0; inner < aMatrix.GetLength(1); inner++)
                        {
                            product[row, col] += aMatrix[row, inner] * bMatrix[inner, col];
                        }
                    }
                }
                return product;
            }
            else//Должно совпадать, иначе нет смысла считать
            {
                throw new Exception();
            }
        }

        public override ModelsState copySelf()
        {
            MarcovitsModelState copy = new MarcovitsModelState();
            //copy.pathOfDataBase = state.pathOfDataBase;
            //copy.tableOfDataBase = config.getTableOfDataBase();
            copy.income = state.income;

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
            //state.pathOfDataBase = oldState.pathOfDataBase;
            //config.setTableOfDataBase(oldState.tableOfDataBase);
            MarcovitsModelState oldMarcovitsState = (MarcovitsModelState)oldState;
            state.income = oldMarcovitsState.income;

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
        }

        public override void setConfig(MarcovitsConfig configData)
        {
            config = configData;
        }

        public override MarcovitsModelState getResult()
        {
            return state;
        }
    }
}
