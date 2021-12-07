using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//O(no!)
//it's O(n^3) -_-
//I'm not proud of it BUT it works so who cares
namespace AdventOfCode2021.Solutions
{
    public static class Day7
    {
        private static int[] numbers;
        private static void Init()
        {
            string[] lines = File.ReadAllLines(Extensions.GetPath("Inputs/Day7.txt"));
            string[] positions = lines[0].Split(',');
            numbers = new int[positions.Length];

            for (int i = 0; i < positions.Length; i++)
            {
                numbers[i] = int.Parse(positions[i]);
            }
        }
        
        private static int Difference(int a, int b)
        {
            if (b > a)
                return b - a;
            return a - b;
        }

        public static int SolveA()
        {
            Init();
            int highest = numbers.Max();

            int lowestFuel = int.MaxValue;

            for (int i = 0; i < highest; i++)
            {
                int fuelCosumption = CheckPos(i);
                if (fuelCosumption < lowestFuel)
                    lowestFuel = fuelCosumption;
            }
            return lowestFuel;
        }

        public static int SolveB()
        {
            Init();
            int highest = numbers.Max();

            int lowestFuel = int.MaxValue;

            for (int i = 0; i < highest; i++)
            {
                int fuelCosumption = CheckPos_B(i);
                if (fuelCosumption < lowestFuel)
                {
                    lowestFuel = fuelCosumption;
                    Console.WriteLine($"Fuel for {i} is {fuelCosumption}. Accepted");
                }
                else
                    Console.WriteLine($"Fuel for {i} is {fuelCosumption}");
            }

            Console.WriteLine(Sumnation(16, 5));
            return lowestFuel;

            //return Sumnation(100, 10);
        }

        private static int CheckPos(int position)
        {
            int fuelCost = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                fuelCost += Difference(numbers[i], position);
            }
            return fuelCost;
        }

        private static int CheckPos_B(int position)
        {
            int fuelCost = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                int fuelForThat = Sumnation(numbers[i], position);
                fuelCost += fuelForThat;

                if (position == 5)
                    Console.WriteLine($"{numbers[i]}-{position} {fuelForThat}. Total = {fuelCost}");
            }

            return fuelCost;
        }

        private static int Sumnation(int start, int end)
        {
            if (start > end)
            {
                int oldStart = start;
                start = end;
                end = oldStart;
            }

            //to get it going from 0-x
            end -= start;
            start = 0;

            int returnValue = 0;
            for (int i = start; i <= end; i++)
            {
                returnValue += i;
            }
            return returnValue;
        }
    }
}
