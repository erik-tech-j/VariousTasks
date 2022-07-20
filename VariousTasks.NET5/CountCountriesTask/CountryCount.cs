using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariousTasks.NET5.CountCountriesTask
{
    public class CountryCount
    {
        /// <summary>
        /// Test the task of counting countries
        /// </summary>
        public static void Test()
        {
            const int rowsCount = 100;
            const int columnsCount = 10;

            Random rnd = new Random();
            int[][] array = new int[rowsCount][];
            for (int i = 0; i < rowsCount; i++)
            {
                array[i] = new int[columnsCount];
                for (int j = 0; j < columnsCount; j++)
                    array[i][j] = rnd.Next(1, 7);
            }

            CountryCount counter = new CountryCount();
            int result = counter.CountCountries(array);

            Console.WriteLine($"\nКількість країн: {result}");
        }

        /// <summary>
        /// Count countries in array
        /// </summary>
        /// <param name="array">Input array with countries and their colours inside</param>
        /// <returns>Country count in array</returns>
        public int CountCountries(int[][] array)
        {
            Dictionary<int, List<(int, int)>> result = new Dictionary<int, List<(int, int)>>();
            int countryIndex = 0;

            for (int row = 0; row < array.Length; row++)
            {
                for (int column = 0; column < array[row].Length; column++)
                {
                    if (result.Where(x => x.Value.Contains((row, column))).Count() == 0)
                    {
                        countryIndex++;
                        result.Add(countryIndex, new List<(int, int)> { (row, column) });

                        Console.WriteLine($"Формування країни номер {countryIndex}:");
                        Console.Write($"Перший елемент ({row}, {column}).");

                        ConnectElementsIntoCountry(ref result, ref array, row, column, countryIndex);
                        CountryOutputInTheConsole(result.Last(), ref array);

                        Console.WriteLine($"---------------------------------");
                    }
                }
            }

            return countryIndex;
        }

        /// <summary>
        /// Make full country with coordinates
        /// </summary>
        /// <param name="countries">Countries dictionary</param>
        /// <param name="array">Input array</param>
        /// <param name="row">Current row</param>
        /// <param name="column">Current column</param>
        /// <param name="countryIndex">Current country index (or number)</param>
        private void ConnectElementsIntoCountry(ref Dictionary<int, List<(int, int)>> countries,
            ref int[][] array, int row, int column, int countryIndex)
        {
            int currentValue = array[row][column];

            if (IsTopElementExists(ref array, row, out int topRowIndex) && currentValue == array[topRowIndex][column])
            {
                if (!IsAnElementInCountry(countries[countryIndex], (topRowIndex, column)))
                {
                    Console.Write($"\nВгору. З елементу ({row}, {column}) до ({topRowIndex}, {column}).");
                    countries[countryIndex].Add((topRowIndex, column));
                    ConnectElementsIntoCountry(ref countries, ref array, topRowIndex, column, countryIndex);
                }
            }

            if (IsBottomElementExists(ref array, row, out int bottomRowIndex) && currentValue == array[bottomRowIndex][column])
            {
                if (!IsAnElementInCountry(countries[countryIndex], (bottomRowIndex, column)))
                {
                    Console.Write($"\nВниз. З елементу ({row}, {column}) до ({bottomRowIndex}, {column}).");
                    countries[countryIndex].Add((bottomRowIndex, column));
                    ConnectElementsIntoCountry(ref countries, ref array, bottomRowIndex, column, countryIndex);
                }
            }

            if (IsLeftElementExists(ref array, row, column, out int leftColumnIndex) && currentValue == array[row][leftColumnIndex])
            {
                if (!IsAnElementInCountry(countries[countryIndex], (row, leftColumnIndex)))
                {
                    Console.Write($"\nВліво. З елементу ({row}, {column}) до ({row}, {leftColumnIndex}).");
                    countries[countryIndex].Add((row, leftColumnIndex));
                    ConnectElementsIntoCountry(ref countries, ref array, row, leftColumnIndex, countryIndex);
                }
            }

            if (IsRightElementExists(ref array, row, column, out int rightColumnIndex) && currentValue == array[row][rightColumnIndex])
            {
                if (!IsAnElementInCountry(countries[countryIndex], (row, rightColumnIndex)))
                {
                    Console.Write($"\nВправо. З елементу ({row}, {column}) до ({row}, {rightColumnIndex}).");
                    countries[countryIndex].Add((row, rightColumnIndex));
                    ConnectElementsIntoCountry(ref countries, ref array, row, rightColumnIndex, countryIndex);
                }
            }
        }

        /// <summary>
        /// Check that top element exists
        /// </summary>
        /// <param name="array">Input array</param>
        /// <param name="currentRowIndex">Current row index</param>
        /// <param name="topRowIndex">Found top row index</param>
        /// <returns></returns>
        private bool IsTopElementExists(ref int[][] array, int currentRowIndex, out int topRowIndex)
        {
            topRowIndex = -1;

            if (currentRowIndex < 1)
            {
                return false;
            }

            topRowIndex = currentRowIndex - 1;

            return topRowIndex >= 0 && array.Length - 1 >= topRowIndex ? true : false;
        }

        /// <summary>
        /// Check that bottom element exists
        /// </summary>
        /// <param name="array">Input array</param>
        /// <param name="currentRowIndex">Current row index</param>
        /// <param name="bottomRowIndex">Found bottom row index</param>
        /// <returns></returns>
        private bool IsBottomElementExists(ref int[][] array, int currentRowIndex, out int bottomRowIndex)
        {
            bottomRowIndex = -1;

            if (currentRowIndex < 0 || currentRowIndex >= array.Length - 1)
            {
                return false;
            }

            bottomRowIndex = currentRowIndex + 1;

            return bottomRowIndex >= 0 && array.Length - 1 >= bottomRowIndex ? true : false;
        }

        /// <summary>
        /// Check that left element exists
        /// </summary>
        /// <param name="array">Input array</param>
        /// <param name="currentRowIndex">Current row index</param>
        /// <param name="currentColumnIndex">Current column indxe</param>
        /// <param name="leftColumnIndex">Found left column index</param>
        /// <returns></returns>
        private bool IsLeftElementExists(ref int[][] array, int currentRowIndex, int currentColumnIndex, out int leftColumnIndex)
        {
            leftColumnIndex = -1;

            if (currentColumnIndex < 1)
            {
                return false;
            }

            leftColumnIndex = currentColumnIndex - 1;

            return leftColumnIndex >= 0 && array[currentRowIndex].Length - 1 >= leftColumnIndex ? true : false;
        }

        /// <summary>
        /// Check that right element exists
        /// </summary>
        /// <param name="array">Input array</param>
        /// <param name="currentRowIndex">Current row index</param>
        /// <param name="currentColumnIndex">Current column indxe</param>
        /// <param name="rightColumnIndex">Found right column index</param>
        /// <returns></returns>
        private bool IsRightElementExists(ref int[][] array, int currentRowIndex, int currentColumnIndex, out int rightColumnIndex)
        {
            rightColumnIndex = -1;

            if (currentColumnIndex >= array[currentRowIndex].Length - 1)
            {
                return false;
            }

            rightColumnIndex = currentColumnIndex + 1;

            return rightColumnIndex >= 0 && array[currentRowIndex].Length - 1 >= rightColumnIndex ? true : false;
        }

        /// <summary>
        /// Check is an element in current country
        /// </summary>
        /// <param name="countryCoordinates">Country coordinates list</param>
        /// <param name="currentValue">Current coordinates</param>
        /// <returns></returns>
        private bool IsAnElementInCountry(List<(int, int)> countryCoordinates, (int, int) currentValue)
        {
            return countryCoordinates.Contains(currentValue);
        }

        /// <summary>
        /// Output country data in the console
        /// </summary>
        /// <param name="country">Country coordinates</param>
        /// <param name="array">Input array</param>
        private void CountryOutputInTheConsole(KeyValuePair<int, List<(int, int)>> country, ref int[][] array)
        {
            Console.WriteLine($"\nСформована країна номер {country.Key}.");
            Console.WriteLine("Координати країни:");
            foreach (var item in country.Value)
            {
                Console.WriteLine($"(Рядок: {item.Item1}; Стовпець: {item.Item2}) Значення: {array[item.Item1][item.Item2]}.");
            }
        }
    }
}
