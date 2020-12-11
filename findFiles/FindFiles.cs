using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace findFiles
{
    public class FindFiles
    {
        public const string CURRENT_DIRECTORY = "ShowFilesCurrentDirectory";
        public const string SPECIFIED_DIRECTORY = "ShowFilesSpecifiedDirectory";
        public const string SPECIFIED_PERMISSION_MASK = "ShowFilesWithSpecifiedPermissionMask";
        public const string BELONGING_GROUP = "ShowFilesBelongingToGroup";
        public const string OWNED_USER = "ShowFilesOwnedByUser";
        public const string SPECIFIED_SIZE = "ShowFilesSpecifiedSize";
        public const string NAME_FILE = "ShowFilesByName";

        private readonly string action;
        private string pathToSearchFile;
        private readonly Stopwatch stopWatch;

        public FindFiles()
        {
            this.action = "";
            this.pathToSearchFile = "";
            this.stopWatch = new Stopwatch();
        }

        public FindFiles(string action)
        {
            this.action = action;
            this.pathToSearchFile = "";
            this.stopWatch = new Stopwatch();
        }

        public void StartTimer()
        {
            this.stopWatch.Start();
        }

        public string StopTimer()
        {
            this.stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

            return elapsedTime;
        }

        public string ShowFiles(Dictionary<string, string> paramsFindFiles)
        {
            string result = "";

            switch (this.action)
            {
                case CURRENT_DIRECTORY:
                    result = this.FindFilesInCurrentDirectory();
                    break;
                case SPECIFIED_DIRECTORY:
                    result = this.FindFilesInSpecifiedDirectory(paramsFindFiles["directoryName"]);
                    break;
                case SPECIFIED_PERMISSION_MASK:
                    result = this.FindFilesWithSpecifiedPermissionMask(paramsFindFiles["permMask"]);
                    break;
                case BELONGING_GROUP:
                    result = this.FindFilesBelongingToGroup(paramsFindFiles["groupName"]);
                    break;
                case OWNED_USER:
                    result = this.FindFilesOwnedByUser(paramsFindFiles["userName"]);
                    break;
                case SPECIFIED_SIZE:
                    result = this.FindFilesSpecifiedSize(paramsFindFiles["sizeFiles"]);
                    break;
                case NAME_FILE:
                    result = this.FindFilesByName(paramsFindFiles["fileName"]);
                    break;
                default:
                    result = "files not found";
                    break;
            }

            return result;
        }

        private string FindFilesInCurrentDirectory()
        {
            return Helper.Bash("find");
        }

        private string FindFilesInSpecifiedDirectory(string directoryName)
        {
            return Helper.Bash("find " + directoryName);
        }

        private string FindFilesWithSpecifiedPermissionMask(string permMask)
        {
            return Helper.Bash("find /home/ * f -perm " + permMask);
        }

        private string FindFilesBelongingToGroup(string groupName)
        {
            return Helper.Bash("find /var/www -group " + groupName);
        }

        private string FindFilesOwnedByUser(string userName)
        {
            return Helper.Bash("find . -user " + userName);
        }

        private string FindFilesSpecifiedSize(string sizeFiles)
        {
            return Helper.Bash("find /home/ -size " + sizeFiles);
        }

        private string FindFilesByName(string fileName)
        {
            // если указали полный путь и файл существует
            if (File.Exists(fileName))
            {
                this.pathToSearchFile = fileName;
            }
            else
            {
                Dictionary<string, string> pathFile = Helper.GetPathToFile(fileName);
                this.DirSearch(pathFile["directory"], pathFile["file"]);
            }

            if (String.IsNullOrEmpty(this.pathToSearchFile))
            {
                this.pathToSearchFile = "file not found";
            }

            return this.pathToSearchFile;
        }

        /*Алгоритм: после того, как получили имя файла, начинаем с корневого каталога,
получаем все имена файлов и сравниваем их со своим именем.
Если ничего не найдено, рекурсивно повторяем этот алгоритм для каждого подкаталога, пока не найдем файл.*/
        private void DirSearch(string sDir, string fileName)
        {
            try
            {
                foreach (string directory in Directory.GetDirectories(sDir))
                {
                    // смотрим файлы в дирректории, подходящие по шаблону
                    foreach (string file in Directory.GetFiles(directory, fileName))
                    {
                        this.pathToSearchFile += file;
                        this.pathToSearchFile += "\n";
                    }

                    DirSearch(directory, fileName);
                }
            }
            catch (System.Exception exception)
            {
                this.pathToSearchFile = exception.Message;
            }
        }
    }
}