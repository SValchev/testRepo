using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITVanioIvanov
{
    class Program
    {
		//result = 15
//        private static int[][] matrix =
//            {
//                new[] {4, 4, 7, 2, 6},
//                new[] {1, 3, 5, 4, 1},
//                new[] {1, 2, 1, 3, 2},
//                new[] {3, 7, 6, 8, 6},
//                new[] {2, 6, 2, 1, 3},
//                new[] {3, 9, 4, 2, 3},
//                new[] {6, 2, 3, 4, 3},
//                new[] {2, 8, 7, 5, 9},
//            };
		//18 
		private static int[][] matrix =
			            {
			                new[] {5, 8, 7, 5, 2},
			                new[] {1, 3, 5, 4, 1},
			                new[] {6, 7, 6, 8, 3},
			                new[] {3, 9, 4, 2, 3},
			                new[] {6, 4, 7, 2, 4},
			                new[] {3, 2, 3, 4, 6},
			                new[] {9, 8, 8, 3, 1},
			                new[] {9, 6, 2, 1, 9},
			            };


        //private static int[][] matrix =
        //    {
        //       new[]  { 5,3,4,8,9,3,2,},
        //       new [] { 2,3,6,7,8,8,1,},

        //        new[] { 2,1,4,9,4,4,8,},
        //        new[] { 2,9,2,1,8,4,4,},
        //        new [] { 3,3,5,2,8,5,1,},
        //        new [] { 2,1,2,1,3,2,3,},
        //        new[] { 2,4,6,4,5,1,1,},
        //        new[] { 6,6,3,3,8,8,4,},
        //        new [] { 1,9,3,5,1,6,4,},
        //        new [] { 1,5,1,2,3,3,9,},
        //        new [] { 8,1,4,3,1,1,9,}
        //    };

        private static int[] sumOfRows;
        private static int[] sumOfColums;
        private static int[] usedColums;
        private static int[] usedRows;

        private static int rowCounter = 0;

        static void Main()
        {
            sumOfRows = new int[matrix.Length];
            sumOfColums = new int[matrix[0].Length];
            usedColums = new int[matrix[0].Length];
            usedRows = new int[matrix.Length];

            SumRows();
            SumColums();

            PrintMatrix();
            PrintColumSum();

            int lowestCost = FindLowestCost();

            Console.WriteLine("{0}Lowest Sum = {1}{0}", Environment.NewLine, lowestCost);
        }
        private static int FindLowestCost()
        {

            int result = 0;
            for (int i = 0; i < matrix[0].Length; i++)
            {
                int currentColum = GetCurrentColums();
                int currentRow = GetCurrentrow(currentColum);
                result += matrix[currentRow][currentColum];

            }

            for (int i = 0; i < matrix.Length; i++)
            {

                int currentRow = GetCurrentRow();
                int currentColum;
                if (usedRows[currentRow] < 1)
                {
                    currentColum = GetCurrentColum(currentRow);
                    result += matrix[currentRow][currentColum];
                }
                //result += matrix[currentRow][currentColum];
            }
            return result;
        }



        private static void PrintColumSum()
        {
            for (int i = 0; i < sumOfColums.Length; i++)
            {
                char currentChar = (char)('A' + i);
                Console.WriteLine($"Sum of {currentChar} = {sumOfColums[i]}");
            }
        }

        private static void PrintMatrix()
        {
            Console.Write(new string(' ', 5));
            for (char i = 'A'; i < 'A' + matrix[0].Length; i++)
            {
                Console.Write(i + new string(' ', 3));
            }

            Console.WriteLine();

            for (int currentRow = 0; currentRow < matrix.Length; currentRow++)
            {
                Console.WriteLine($"{currentRow + 1}. | {string.Join(" | ", matrix[currentRow])} | Row sum = {sumOfRows[currentRow]}");
            }

            Console.WriteLine();
        }

        private static int GetCurrentrow(int currentColum)
        {
            int currentRow = 0;

            for (int i = 1; i < matrix.Length; i++)
            {
				//TODO: IF sum of columns is the same should take the one with the lowest row( now it takes the last met index)
                if (matrix[currentRow][currentColum] >= matrix[i][currentColum] && usedColums[currentColum] < 2 && usedRows[i] < 1)
                {
                    currentRow = i;
                }
            }

            //for (int i = 1; i < matrix[i].Length; i++)
            //{
            //    if (matrix[i][currentColum] == matrix[currentRow][currentColum] && usedColums[i] < 2)
            //    {
            //        if (sumOfColums[currentRow] < sumOfColums[i])
            //        {
            //            currentRow = i;
            //        }
            //    }
            //    else if (matrix[i][currentColum] > matrix[currentRow][currentColum] && usedColums[i] < 2)
            //    {
            //        currentRow = i;
            //    }
            //}

            usedColums[currentColum]++;
            usedRows[currentRow]++;
            return currentRow;
        }

        private static int GetCurrentColum(int currentRow)
        {
            int smallestIndexValue = 0;

            for (int i = 1; i < matrix[currentRow].Length; i++)
            {
                if (matrix[currentRow][i] == matrix[currentRow][smallestIndexValue] && usedColums[i] < 2)
                {
                    if (sumOfColums[smallestIndexValue] > sumOfColums[i])
                    {
                        smallestIndexValue = i;
                    }
                }
                else if (matrix[currentRow][i] < matrix[currentRow][smallestIndexValue] && usedColums[i] < 2)
                {
                    smallestIndexValue = i;
                }
            }

            usedColums[smallestIndexValue]++;
            return smallestIndexValue;
        }

        private static int GetCurrentColums()
        {
            int biggestIndex = 0;
            for (int i = 1; i < sumOfColums.Length; i++)
            {
                if (sumOfColums[biggestIndex] < sumOfColums[i]) //&& sumOfRows[i] != -1)
                {
                    biggestIndex = i;
                }
            }

			rowCounter++;//TODO: May cause problem!
            sumOfColums[biggestIndex] = -1;
           
			return biggestIndex;
        }

        private static int GetCurrentRow()
        {
            int biggestIndex = 0;
            for (int i = 1; i < sumOfRows.Length; i++)
            {
                if (sumOfRows[biggestIndex] < sumOfRows[i]) //&& sumOfRows[i] != -1)
                {
                    biggestIndex = i;
                }
            }

            rowCounter++;
            sumOfRows[biggestIndex] = -1;
          
			return biggestIndex;
        }

        //NotNeeded
        private static int LowestRowIndex(int row)
        {
            int lowestIndex = 0;
            for (int i = 1; i < matrix[row].Length; i++)
            {
                if (matrix[row][i] < matrix[row][lowestIndex])
                {
                    lowestIndex = i;
                }
            }

            return lowestIndex;
        }

        private static void SumColums()
        {
            for (int colum = 0; colum < matrix[0].Length; colum++)
            {
                for (int row = 0; row < matrix.Length; row++)
                {
                    sumOfColums[colum] += matrix[row][colum];
                }
            }
        }

        private static void SumRows()
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                for (int colum = 0; colum < matrix[row].Length; colum++)
                {
                    sumOfRows[row] += matrix[row][colum];
                }
            }
        }
    }
}
