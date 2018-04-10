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
        AllDataTableConverter converter = new AllDataTableConverter();

        public MarcovitsModel(string pathOfDataBase, string tableOfDataBase)
        {
            state.pathOfDataBase = pathOfDataBase;
            state.tableOfDataBase = tableOfDataBase;
        }

        public void calculationStatistics()
        {
            double[] matrixA = new double[10];
            matrixA[0] = 5.93;
            matrixA[1] = 5.85;
            matrixA[2] = 5.21;
            matrixA[3] = 5.37;
            matrixA[4] = 4.99;
            matrixA[5] = 4.87;
            matrixA[6] = 4.7;
            matrixA[7] = 4.75;
            matrixA[8] = 4.33;
            matrixA[9] = 3.86;

            double[] matrixB = new double[10];
            matrixB[0] = 2.27;
            matrixB[1] = 2.39;
            matrixB[2] = 3.47;
            matrixB[3] = 3.21;
            matrixB[4] = 2.95;
            matrixB[5] = 2.97;
            matrixB[6] = 3.32;
            matrixB[7] = 3.65;
            matrixB[8] = 3.97;
            matrixB[9] = 3.81;

            double cov = covar(matrixA, matrixB);


            int[,] aMatrix = new int[3, 2];
            aMatrix[0, 0] = 1;
            aMatrix[0, 1] = 4;
            aMatrix[1, 0] = 2;
            aMatrix[1, 1] = 5;
            aMatrix[2, 0] = 3;
            aMatrix[2, 1] = 6;

            int[,] bMatrix = new int[2, 3];
            bMatrix[0, 0] = 7;
            bMatrix[0, 1] = 8;
            bMatrix[0, 2] = 9;
            bMatrix[1, 0] = 10;
            bMatrix[1, 1] = 11;
            bMatrix[1, 2] = 12;

            int[,] unsver = MultiplyMatrix(aMatrix, bMatrix);


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
            observer.notify(state.data);
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


        int[,] MultiplyMatrix(int[,] aMatrix, int[,] bMatrix)//Умножение матриц
        {
            if (aMatrix.GetLength(1) == bMatrix.GetLength(0))
            {
                int[,] product = new int[aMatrix.GetLength(0), bMatrix.GetLength(1)];


                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        // Multiply the row of A by the column of B to get the row, column of product.  
                        for (int inner = 0; inner < 2; inner++)
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
