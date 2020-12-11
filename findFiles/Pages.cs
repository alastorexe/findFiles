using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace findFiles
{
    public static class Pages
    {
        public static void ShowFilesCurrentDirectory()
        {
            Dictionary<string, string> paramsFindFiles = new Dictionary<string, string>();

            Console.WriteLine("Текущая директория: ");
            Console.WriteLine(Helper.Bash("pwd"));

            ShowFiles(FindFiles.CURRENT_DIRECTORY, paramsFindFiles);
        }
        public static void ShowFilesSpecifiedDirectory()
        {
            Dictionary<string, string> paramsFindFiles = new Dictionary<string, string>();

            Console.WriteLine("Укажите директорию: ");
            string directoryName = Console.ReadLine();
            paramsFindFiles.Add("directoryName", directoryName);

            ShowFiles(FindFiles.SPECIFIED_DIRECTORY, paramsFindFiles);
        }
        public static void ShowFilesWithSpecifiedPermissionMask()
        {
            Dictionary<string, string> paramsFindFiles = new Dictionary<string, string>();

            Console.WriteLine("Укажите маску прав (0664): ");
            string permMask = Console.ReadLine();
            paramsFindFiles.Add("permMask", permMask);

            ShowFiles(FindFiles.SPECIFIED_PERMISSION_MASK, paramsFindFiles);
        }
        public static void ShowFilesBelongingToGroup()
        {
            Dictionary<string, string> paramsFindFiles = new Dictionary<string, string>();

            Console.WriteLine("Укажите группу): ");

            string groupName = Console.ReadLine();
            paramsFindFiles.Add("groupName", groupName);

            ShowFiles(FindFiles.BELONGING_GROUP, paramsFindFiles);
        }
        public static void ShowFilesOwnedByUser()
        {
            Dictionary<string, string> paramsFindFiles = new Dictionary<string, string>();

            Console.WriteLine("Укажите пользователя): ");
            string userName = Console.ReadLine();
            paramsFindFiles.Add("userName", userName);

            ShowFiles(FindFiles.OWNED_USER, paramsFindFiles);
        }
        public static void ShowFilesSpecifiedSize()
        {
            Dictionary<string, string> paramsFindFiles = new Dictionary<string, string>();

            Console.WriteLine("Укажите размер файла в формате (50М): ");
            string sizeFiles = Console.ReadLine();
            paramsFindFiles.Add("sizeFiles", sizeFiles);

            ShowFiles(FindFiles.SPECIFIED_SIZE, paramsFindFiles);
        }
        public static void ShowFilesByName()
        {
            Dictionary<string, string> paramsFindFiles = new Dictionary<string, string>();

            Console.WriteLine("Укажите имя файла: ");
            string fileName = Console.ReadLine();
            paramsFindFiles.Add("fileName", fileName);

            ShowFiles(FindFiles.NAME_FILE, paramsFindFiles);
        }
        private static void ShowFiles(string action, Dictionary<string, string> paramsFindFiles)
        {
            FindFiles findFiles = new FindFiles(action);
            Console.WriteLine("Файлы: ");

            findFiles.StartTimer();

            Console.WriteLine(findFiles.ShowFiles(paramsFindFiles));

            Console.WriteLine("RunTime: " + findFiles.StopTimer());

            Console.ReadKey();
        }
    }
}