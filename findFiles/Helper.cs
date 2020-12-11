using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace findFiles
{
    public static class Helper
    {
        public static string Bash(string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }

        public static Dictionary<string, string> GetPathToFile(string path)
        {
            Dictionary<string, string>  result = new Dictionary<string, string>();
            string directory = "";
            string file = "";

            string[] subsWay = path.Split("/");

            if (subsWay.Length > 1)
            {
                for (int i = 0; i < subsWay.Length - 1; i++)
                {
                    directory += "/";
                    directory += subsWay[i];
                }
                directory = directory.Replace("//", "/");
                file = subsWay.Last();
            }
            else
            {
                // TODO:: тут указывается корневая дирректория поиска. У тебя другая будет. (modzi - это имя моего пользователя)
                directory = "/home/modzi";
                file = subsWay.Last();
            }

            result.Add("directory", directory);
            result.Add("file", file);

            return result;
        }
    }
}