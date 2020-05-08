using System;
using System.Windows.Forms;

namespace LR5
{
    class KramerAlgorithm
    {
        public static Tuple<double, double, double, double> FindKramer(double[,] TaskArray) // Вычисление методом Крамера
        {
            double x1, x2, x3, x4;

            double finalDet = MainDeterminant(TaskArray);

            double Det1 = XDeterminant(1, TaskArray);
            double Det2 = XDeterminant(2, TaskArray);
            double Det3 = XDeterminant(3, TaskArray);
            double Det4 = XDeterminant(4, TaskArray);

            x1 = Det1 / finalDet;
            x2 = Det2 / finalDet;
            x3 = Det3 / finalDet;
            x4 = Det4 / finalDet;

            return Tuple.Create(x1, x2, x3, x4);
        }

        public static double MainDeterminant(double[,] TaskArray) // Вычисляется главный определитель
        {
            double result;

            result = (TaskArray[0, 0] * Determinant(1, TaskArray)) - (TaskArray[0, 1] * Determinant(2, TaskArray))
                   + (TaskArray[0, 2] * Determinant(3, TaskArray)) - (TaskArray[0, 3] * Determinant(4, TaskArray));

            if (result == 0)
            {
                MessageBox.Show("Главный определитель равен нулю.", "Информация",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return result;
        }

        private static double XDeterminant(int num, double[,] TaskArray) // Вычисляются определители
        {
            double result;
            double[,] diffArray = new double[4, 4];

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    diffArray[i, j] = TaskArray[i, j];

            for (int i = 0; i < 4; i++)
                diffArray[i, num - 1] = TaskArray[i, 4];

            result = (diffArray[0, 0] * Determinant(1, diffArray)) - (diffArray[0, 1] * Determinant(2, diffArray))
                   + (diffArray[0, 2] * Determinant(3, diffArray)) - (diffArray[0, 3] * Determinant(4, diffArray));

            return result;
        }

        private static double Determinant(int num, double[,] array) // Вычисляется детерминант
        {
            double result;
            double[,] currArray = new double[3, 3];
            int iC = 0, jC = 0;

            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                    continue;

                for (int j = 0; j < 4; j++)
                {
                    if (j == num - 1)
                        continue;
                    currArray[iC, jC] = array[i, j];

                    if (jC == 2)
                    {
                        jC = 0;
                        iC++;
                    }
                    else
                        jC++;
                }
            }

            result = (currArray[0, 0] * currArray[1, 1] * currArray[2, 2]) +
                     (currArray[0, 1] * currArray[1, 2] * currArray[2, 0]) +
                     (currArray[1, 0] * currArray[2, 1] * currArray[0, 2]) -

                     (currArray[2, 0] * currArray[1, 1] * currArray[0, 2]) -
                     (currArray[0, 1] * currArray[1, 0] * currArray[2, 2]) -
                     (currArray[1, 2] * currArray[2, 1] * currArray[0, 0]);

            return result;
        }
    }
}
