using Analytics.CommonComponents.Exceptions;
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
                throw new InvalidArraySize("Check the size of the arrays involved in the" +
                    " covariance calculation");
            }
        }

        //Расчет корелляции
        public static double corellation(double[] matrixA, double[] matrixB)
        {
            double unswer = 0;
            double matrixCovar = covar(matrixA, matrixB);
            unswer = matrixCovar / (standardDeviation(matrixA)*standardDeviation(matrixB));

            return unswer;
        }

        //Расчет стандартного (среднеквадратичного отклонения)
        public static double standardDeviation(double[] matrix)
        {
            double unswer = 0;
            double matrixAvg = avg(matrix);
            for(int i=0; i < matrix.Length; i++)
            {
                unswer += System.Math.Pow((matrix[i]-matrixAvg), 2);
            }
            unswer = unswer / (matrix.Length-1);
            unswer = System.Math.Pow(unswer, 0.5);

            return unswer;
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
            else//Должно совпадать, иначе нет смысла сравнивать
            {
                throw new InvalidArraySize("Check the size of the arrays involved in the" +
                    " multiply matrix calculation");
            }
        }
    }
}
