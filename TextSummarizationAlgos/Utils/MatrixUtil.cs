using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextSummarizationAlgos.Utils
{
    class MatrixUtil
    {
        private MatrixUtil()
        {
        }

        public static double vectorNorm(double[] vector)
        {
            double norm = 0;

            for (int i = 0; i < vector.Length; i++)
            {
                norm += Math.Abs(vector[i]);
            }

            return (norm);
        }

        public static double[] subtract(double[] mat1, double[] mat2)
        {
            double[] result = new double[mat1.Length];

            for (int i = 0; i < mat1.Length; i++)
            {
                result[i] = mat1[i] - mat2[i];
            }

            return (result);
        }

        public static double[] multiply(double[][] mat1, double[] mat2)
        {
            double[] result = new double[mat2.Length];

            for (int i = 0; i < result.Length; i++)
                result[i] = 0;

            for (int i = 0; i < mat1.Length; i++)
            {
                for (int j = 0; j < mat1.Length; j++)
                {
                    result[i] += mat1[i][j] * mat2[j];
                }
            }

            return (result);
        }

        public static string printMatrix(double[] matrix)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < matrix.Length; i++)
            {
                result.Append(String.Format("{0:0.00000000}", matrix[i]) + "\t\t");
            }
            result.Append(Environment.NewLine);

            return (result.ToString());
        }

        public static string printMatrix(double[][] matrix)
        {
            //*
            StringBuilder result = new StringBuilder();

            double[] colSum = new double[matrix.Length];

            for (int i = 0; i < matrix.Length; i++)
            {
                double rowsum = 0;
                for (int j = 0; j < matrix.Length; j++)
                {
                    result.Append(String.Format("{0:0.00000000}", matrix[i][j]) + "\t\t");
                    rowsum += matrix[i][j];
                    colSum[i] += matrix[i][j];
                }
                //result += "|" + rowsum;
                result.Append(Environment.NewLine);
            }

            result.Append(Environment.NewLine);

            return (result.ToString());
            //*/

            /*
            string result = "";

            double[] colSum = new double[matrix.Length];

            for (int i = 0; i < matrix.Length; i++)
            {
                double rowsum = 0;
                for (int j = 0; j < matrix.Length; j++)
                {
                    result += String.Format("{0:0.00000000}", matrix[i][j]) + "\t\t";
                    rowsum += matrix[i][j];
                    colSum[i] += matrix[i][j];
                }
                //result += "|" + rowsum;
                result += Environment.NewLine;
            }

            result += Environment.NewLine;

            return (result);
            //*/
        }
    }
}
