using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Jesus christ this one was difficult
//Nevermind I severely under-estimated day4
//Now I have to go fix my awful spelling of 'collum'
namespace AdventOfCode2021.Solutions
{
    public static class Day4
    {
        public class Bingo
        {
            public int[,] table = new int[5, 5];

            public static List<int> CrossedNumbers { get; private set; } = new();

            public bool HasWon
            {
                get
                {
                    //Check for row
                    for (int i = 0; i < 5; i++)
                    {
                        if (CheckRow(i))
                            return true;
                    }

                    for (int i = 0; i < 5; i++)
                    {
                        if (Checkcolumn(i))
                            return true;
                    }

                    return false;
                }
            }

            private bool CheckRow(int row)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (!CrossedNumbers.Contains(table[row, i]))
                        return false;
                }
                return true;
            }

            private bool Checkcolumn(int column)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (!CrossedNumbers.Contains(table[i, column]))
                        return false;
                }
                return true;
            }

            public int Sum()
            {
                int sum = 0;
                for (int i = 0; i < table.GetLength(0); i++)
                {
                    for (int j = 0; j < table.GetLength(1); j++)
                    {
                        if (CrossedNumbers.Contains(table[i, j]))
                            continue;

                        sum += table[i, j];
                    }
                }
                return sum;
            }

            public static void Cross(int number)
            {
                CrossedNumbers.Add(number);
            }

            public Bingo(string tableString)
            {
                string[] rows = tableString.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                table = new int[5, 5];
                for (int i = 0; i < rows.Length; i++)   //Rows.length = columns
                {
                    int[] currentRow = GetNumbers(rows[i], ' ');
                    for (int j = 0; j < currentRow.Length; j++)
                    {
                        table[i, j] = currentRow[j];
                    }
                }
            }

            public override string ToString()
            {
                string returnVal = "";
                for (int i = 0; i < table.GetLength(0); i++)
                {
                    for (int j = 0; j < table.GetLength(1); j++)
                    {
                        if (CrossedNumbers.Contains(table[i, j]))
                            returnVal += "*" + table[i, j] + ",";
                        else
                            returnVal += " " + table[i, j] + ",";
                    }

                    returnVal += "\r\n";
                }

                return returnVal;
            }
        }

        private static int[] GetNumbers(string line, char seperator)
        {
            string[] numberStrings = line.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
            int[] returnvalue = new int[numberStrings.Length];

            for (int i = 0; i < numberStrings.Length; i++)
            {
                returnvalue[i] = int.Parse(numberStrings[i]);
            }
            return returnvalue;
        }

        private static List<Bingo> GetBingos(string[] input)
        {
            string lastSheet = "";
            List<Bingo> bingos = new();
            for (int i = 2; i < input.Length; i++)
            {
                if (input[i] == "")
                {
                    bingos.Add(new Bingo(lastSheet));
                    lastSheet = "";
                    continue;
                }

                lastSheet += "\n" + input[i];
            }

            bingos.Add(new Bingo(lastSheet));

            return bingos;
        }

        public static int SolveA()
        {
            string path = Extensions.GetPath("Inputs/Day4.txt");
            string[] inputs = File.ReadAllLines(path);

            int[] numbers = GetNumbers(inputs[0], ',');

            List<Bingo> bingos = GetBingos(inputs);

            for (int i = 0; i < numbers.Length; i++)
            {
                Bingo.Cross(numbers[i]);

                for (int j = 0; j < bingos.Count; j++)
                {
                    if (bingos[j].HasWon)
                        return bingos[j].Sum() * numbers[i];
                }
            }

            Console.WriteLine("ERROR: NO BOARDS WON");
            return -1;
        }


        public static int SolveB()
        {
            string path = Extensions.GetPath("Inputs/Day4.txt");
            string[] inputs = File.ReadAllLines(path);

            int[] numbers = GetNumbers(inputs[0], ',');

            List<Bingo> bingos = GetBingos(inputs);

            List<int> winnerSums = new();
            List<Bingo> winners = new();
            for (int i = 0; i < numbers.Length; i++)
            {
                Bingo.Cross(numbers[i]);

                for (int j = 0; j < bingos.Count; j++)
                {
                    if (bingos[j].HasWon && !winners.Contains(bingos[j]))
                    {
                        winners.Add(bingos[j]);
                        winnerSums.Add(bingos[j].Sum() * numbers[i]);
                    }
                }

            }

            Console.WriteLine($"ERROR: NO BOARDS WON. len = {bingos.Count}");
            return winnerSums[^1];
        }
    }
}
