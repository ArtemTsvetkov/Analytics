using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.Math
{
    static class MathWorker
    {
        //Расчет среднего значения
        public static double avg(double[] matrix)
        {
            double sum = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                sum += matrix[i];
            }
            return (sum / (double)matrix.Length);
        }

        //Расчет ковариации
        public static double covar(double[] matrixA, double[] matrixB)
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

        //Умножение матриц
        public static double[,] multiplyMatrix(double[,] aMatrix, double[,] bMatrix)
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
    }
}
