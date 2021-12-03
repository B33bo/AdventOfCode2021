using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Solutions
{
    public static class Day1
    {
        public static int SolveA()
        {
            string path = Extensions.GetPath("Inputs/Day1.txt");
            string[] inputs = File.ReadAllLines(path);

            int previous = int.Parse(inputs[0]);
            int higherNumbers = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                if (!int.TryParse(inputs[i], out int numberCurrent))
                    continue;

                if (numberCurrent > previous)
                    higherNumbers++;

                previous = numberCurrent;
            }

            return higherNumbers;
        }

        public static int SolveB()
        {
            string path = Extensions.GetPath("Inputs/Day1.txt");
            string[] numbersString = File.ReadAllLines(path);

            //Parse all the numbers
            int[] numbers = new int[numbersString.Length];
            for (int i = 0; i < numbersString.Length; i++)
                numbers[i] = int.Parse(numbersString[i]);

            //Get all sums
            List<int> sums = new();
            for (int i = 0; i < numbersString.Length; i++)
            {
                if (i + 2 < numbersString.Length)
                    sums.Add(numbers[i] + numbers[i + 1] + numbers[i + 2]);
                else if (i + 1 < numbersString.Length)
                    sums.Add(numbers[i] + numbers[i + 1]);
                else
                    sums.Add(numbers[i]);
            }

            //Find increases
            int sumIncreases = 0;
            for (int i = 1; i < sums.Count; i++)
            {
                if (sums[i] > sums[i - 1])
                    sumIncreases++;
            }

            return sumIncreases;
        }
    }
}
