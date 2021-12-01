using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Extensions
    {
        public static string GetPath(string RelativeLoc)
        {
            List<string> currentDir = System.Reflection.Assembly.GetEntryAssembly().Location.Split(@"\").ToList();

            currentDir.RemoveAt(currentDir.Count - 1);
            string path = currentDir.ToRealString(@"\");
            return path + @"\" + RelativeLoc;
        }

        public static string ToRealString(this List<string> inputList, string seperator)
        {
            if (inputList.Count == 0)
                return "";

            string str = "";
            for (int i = 0; i < inputList.Count; i++)
            {
                str += seperator + inputList[i];
            }

            return str[seperator.Length..];
        }
    }
}
