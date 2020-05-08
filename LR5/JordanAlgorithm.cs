using System;

namespace LR5
{
    public class JordanAlgorithm
    {
        public static Tuple<double, double, double, double> FindJordan(double[,] TaskArray) // Вычисление методом Гаусса — Жордана
        {
            double x1, x2, x3, x4;
            double[,] diffArray = TaskArray;

            for (int j = 0; j < 4; j++)
            {
                diffArray = Sort(diffArray, j);
                diffArray = MinValueToOne(diffArray, j);
                diffArray = ColumnValuesToZero(diffArray, j);
            }

            x1 = diffArray[0, 4]; // 1 0 0 0 | x1
            x2 = diffArray[1, 4]; // 0 1 0 0 | x2
            x3 = diffArray[2, 4]; // 0 0 1 0 | x3
            x4 = diffArray[3, 4]; // 0 0 0 1 | x4

            return Tuple.Create(x1, x2, x3, x4);
        }

        private static double[,] Sort(double[,] oldArray, int placeJ) // Сортировка массива (ради более понятного кода)
        {
            double[,] newArray = (double[,])oldArray.Clone();

            double min = oldArray[placeJ, placeJ];
            int rowMin = placeJ;

            for (int i = placeJ + 1; i < 4; i++)
                if ((Math.Abs(oldArray[i, placeJ]) < Math.Abs(min) && oldArray[i, placeJ] != 0) || min == 0)
                {
                    min = oldArray[i, placeJ];
                    rowMin = i;
                }

            for (int j = 0; j < 5; j++)
            {
                newArray[placeJ, j] = oldArray[rowMin, j]; // Шок с ссылочным типом. 
                newArray[rowMin, j] = oldArray[placeJ, j]; // Исправлен.
            }

            return newArray;
        }

        private static double[,] MinValueToOne(double[,] oldArray, int placeJ) // Приведение минимального ненулевого 
        {                                                               // элемента к единице для удобства
            double[,] newArray = (double[,])oldArray.Clone();
            double min = oldArray[placeJ, placeJ];

            for (int j = 0; j < 5; j++)
                newArray[placeJ, j] = oldArray[placeJ, j] / min;

            return newArray;
        }

        private static double[,] ColumnValuesToZero(double[,] oldArray, int placeJ) // Остальные строки по столбцу 
        {                                                                    // обнуляются с помощью формул
            double[,] newArray = (double[,])oldArray.Clone();

            for (int i = 0; i < 4; i++)
                if (oldArray[i, placeJ] != 0 && i != placeJ)
                    for (int j = placeJ; j < 5; j++)
                        newArray[i, j] = oldArray[i, j] - (oldArray[placeJ, j] * oldArray[i, placeJ]);
                else
                    continue;

            return newArray;
        }
    }
}
