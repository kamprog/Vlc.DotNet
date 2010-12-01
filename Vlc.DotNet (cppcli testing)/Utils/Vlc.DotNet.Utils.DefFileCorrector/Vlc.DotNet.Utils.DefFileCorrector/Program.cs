using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace Vlc.DotNet.Utils.DefFileCorrector
{
    class Program
    {
        private static readonly StringBuilder sbErrors = new StringBuilder();

        static void Main(string[] args)
        {
            var parameters = GetParameters(args);

            if (parameters == null)
            {
                Console.WriteLine(sbErrors.ToString());
                return;
            }

            if (parameters.ContainsKey("DEFFILE"))
            {
                if (ProcessFile(parameters["DEFFILE"]))
                {
                    Console.WriteLine("Ok");
                    return;
                }
                Console.WriteLine(sbErrors.ToString());
            }
        }

        private static bool ProcessFile(string file)
        {
            if (!File.Exists(file))
            {
                sbErrors.AppendLine("File not found");
                return false;
            }
            var result = new List<string>();
            var lines = File.ReadAllLines(file);

            result.Add("EXPORTS");

            var indexPositionName = -1;
            var firstLinePassed = false;
            for (int index = 0; index < lines.Length; index++)
            {
                var line = lines[index];
                if (line.Contains("ordinal") && line.Contains("name"))
                {
                    indexPositionName = line.IndexOf("name");
                }
                else if (indexPositionName > -1)
                {
                    if (line.Length == 0 && firstLinePassed)
                    {
                        indexPositionName = -1;
                        continue;
                    }
                    else if (line.Length == 0)
                    {
                        firstLinePassed = true;
                    }
                    else
                    {
                        result.Add(line.Substring(indexPositionName).Trim());
                    }
                }
            }
            File.WriteAllLines(file, result.ToArray());
            return true;
        }

        private static Dictionary<string, string> GetParameters(string[] args)
        {
            var result = new Dictionary<string, string>();
            foreach (var arg in args)
            {
                if (!arg.StartsWith("/") || !arg.Contains(":"))
                {
                    sbErrors.AppendLine("Aragument error");
                    return null;
                }
                result.Add(arg.Remove(0, 1).Substring(0, arg.IndexOf(":") - 1).ToUpper(), arg.Substring(arg.IndexOf(":") + 1));
            }

            return result;
        }
    }
}
