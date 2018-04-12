using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class MarcovitsModel : Model
    {
        MarcovitsModelState state = new MarcovitsModelState();
        Observer observer;
        MarcovitsDataTableConverter converter = new MarcovitsDataTableConverter();
        MarcovitsDistinctSoftwareNamesConverter unucNamesConverter = new MarcovitsDistinctSoftwareNamesConverter();

        public MarcovitsModel(string pathOfDataBase, string tableOfDataBase)
        {
            state.pathOfDataBase = pathOfDataBase;
            state.tableOfDataBase = tableOfDataBase;
        }

        public void calculationStatistics()
        {
            //Получение уникальных имен лицензий
            MSAccessProxy accessProxy = new MSAccessProxy();
            accessProxy.setConfig(state.pathOfDataBase, "SELECT DISTINCT software FROM " + state.tableOfDataBase);
            DataSet ds = accessProxy.execute();
            state.unicSoftwareNames = (string[])unucNamesConverter.convert(ds);
            //Формирование запроса на получение данных
            string query = "SELECT  i.year_in, i.month_in, i.day_in, i.hours_in";
            for(int i=0; i<state.unicSoftwareNames.Length; i++)
            {
                query += ", (SELECT COUNT(*) FROM Information ii WHERE ii.software='"+ state.unicSoftwareNames[i]+ "' AND ii.year_in = i.year_in  AND ii.month_in =  i.month_in AND ii.day_in =  i.day_in AND ii.hours_in = i.hours_in)";
            }
            query += "FROM Information i WHERE hours_in IS NOT NULL GROUP BY hours_in, day_in, month_in, year_in ORDER BY year_in, month_in, day_in, hours_in";
            //Получение данных об использовании
            accessProxy.setConfig(state.pathOfDataBase, query);
            ds = accessProxy.execute();
            state.data = (List<MarcovitsDataTable>)converter.convert(ds);

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
            
            //Пока для тестов число закупленных лицензий равно среденему числу
            state.numberBuyLicense = (double[])state.avgNumbersUseLicense.Clone();
            state.numberBuyLicense[2] = 1;

            //Для тестов вшил разные значения
            state.numberBuyLicense[0] = 8;
            state.numberBuyLicense[1] = 3;
            state.numberBuyLicense[2] = 1;
            state.numberBuyLicense[3] = 4;
            state.numberBuyLicense[4] = 1;
            //Расчет разницы между кол-вом закупленных и текущих лицензий
            for (int i = 0; i < state.data.Count; i++)
            {
                for (int j = 0; j < state.data.ElementAt(i).licenses.Length; j++)
                {
                    state.data.ElementAt(i).licenses[j] = (state.data.ElementAt(i).licenses[j]- state.numberBuyLicense[j])/ state.numberBuyLicense[j];
                }
            }

            state.avgDeviationFromPurchasedNumber = new double[state.unicSoftwareNames.Length];
            //расчет ковариации
            double[,] covarMas = new double[state.unicSoftwareNames.Length, state.unicSoftwareNames.Length];
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
            //Пока вшил равные соотношения в процентах
            state.percents = new double[5,1];
            for(int i=0; i<5; i++)
            {
                state.percents[i,0] = 0.2;
            }
            //Замена некоторых просто для теста
            state.percents[0, 0] = 0.4;
            state.percents[1, 0] = 0.1;
            state.percents[2, 0] = 0.2;
            state.percents[3, 0] = 0.3;
            state.percents[4, 0] = 0.1;

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

            notifyObserver();
        }

        public void subscribe(Observer newObserver)
        {
            observer = newObserver;
        }

        public ModelsState copySelf()
        {
            return state;
        }

        public void recoverySelf(ModelsState state)
        {
            this.state = (MarcovitsModelState)state;
        }

        public void loadStore()//загрузка данных из базы данных
        {
            MSAccessProxy accessProxy = new MSAccessProxy();
            //получение значения id
            accessProxy.setConfig(state.pathOfDataBase, "SELECT user_name, user_host, software FROM " + state.tableOfDataBase);
            DataSet ds = accessProxy.execute();
            state.data = (List<MarcovitsDataTable>)converter.convert(ds);
            notifyObserver();
        }

        public void notifyObserver()
        {
            observer.notify(state);//Пока отправляю весь стейт, но потом сделаю через стратегию, чтобы сама вью выбирала что ей отправлять из списка.
                                   //список будет формироваться из части полей State. То есть State будет содержать два блока-обще доступный и приватный
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
                        // Multiply the row of A by the column of B to get the row, column of product.  
                        for (int inner = 0; inner < aMatrix.GetLength(1); inner++)
                        {
                            product[row, col] += aMatrix[row, inner] * bMatrix[inner, col];
                        }
                        //std::cout << product[row][col] << "  ";  
                    }
                    //std::cout << "\n";  
                }
                return product;
            }
            else//Должно совпадать, иначе нет смысла считать
            {
                throw new Exception();
            }
        }
    }
}
