using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Solutions
{
    public static class Day3
    {
        public static int SolveA()
        {
            string path = Extensions.GetPath("Inputs/Day3.txt");
            string[] inputs = File.ReadAllLines(path);

            string gammaBinary = "", epsilonBinary = "";
            for (int i = 0; i < inputs[0].Length; i++)
            {
                string mostCommonDigit = MostCommonDigit(inputs, i);
                gammaBinary += mostCommonDigit;
                epsilonBinary += mostCommonDigit == "0" ? "1" : "0";
            }

            int gamma = Convert.ToInt32(gammaBinary, 2);
            int epsilon = Convert.ToInt32(epsilonBinary, 2);

            return gamma * epsilon;
        }

        public static int SolveB()
        {
            string path = Extensions.GetPath("Inputs/Day3.txt");
            string[] inputs = File.ReadAllLines(path);

            List<string> oxygenGenRating = inputs.ToList();
            List<string> CO2GenRating = inputs.ToList();

            for (int i = 0; i < inputs[0].Length; i++)
            {
                GetOxygensForDigit(oxygenGenRating, i, false);
                GetOxygensForDigit(CO2GenRating, i, true);
            }

            int oxygenRate = Convert.ToInt32(oxygenGenRating[0], 2);
            int CO2Rate = Convert.ToInt32(CO2GenRating[0], 2);
            return oxygenRate * CO2Rate;
        }

        private static void GetOxygensForDigit(List<string> oxygenGenRating, int digitIndex, bool isReverse)
        {
            char digit = MostCommonDigit(oxygenGenRating.ToArray(), digitIndex)[0];

            if (isReverse)
                digit = digit == '1' ? '0' : '1';

            List<string> badInputs = new();
            for (int i = 0; i < oxygenGenRating.Count; i++)
            {
                if (badInputs.Count == oxygenGenRating.Count - 1)
                    break;
                if (oxygenGenRating[i][digitIndex] == digit)
                    continue;

                badInputs.Add(oxygenGenRating[i]);
            }

            for (int i = 0; i < badInputs.Count; i++)
            {
                oxygenGenRating.Remove(badInputs[i]);
            }
        }

        private static string MostCommonDigit(string[] inputs, int getAtIndex)
        {
            int zeros = 0, ones = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i][getAtIndex] == '0')
                    zeros++;
                else if (inputs[i][getAtIndex] == '1')
                    ones++;
            }

            if (zeros == ones)
                Console.WriteLine($"UH OH. ZEROS == ONES at {getAtIndex}");

            if (zeros > ones)
                return "0";
            else
                return "1";
        }
    }
}
