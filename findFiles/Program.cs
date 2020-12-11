using System;

namespace findFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                PrintMainMenu();

                var menuEntry = Console.ReadLine();

                Console.Clear();

                switch (menuEntry)
                {
                    case "1":
                        // Показать все файлы в текущей директории
                        Pages.ShowFilesCurrentDirectory();
                        break;
                    case "2":
                        // Показать все файлы в указанной директории
                        Pages.ShowFilesSpecifiedDirectory();
                        break;
                    case "3":
                        // Показать все файлы с определённой маской прав
                        Pages.ShowFilesWithSpecifiedPermissionMask();
                        break;
                    case "4":
                        // Показать все файлы, принадлежащие группе
                        Pages.ShowFilesBelongingToGroup();
                        break;
                    case "5":
                        // Показать все файлы, принадлежащие пользователю
                        Pages.ShowFilesOwnedByUser();
                        break;
                    case "6":
                        // Показать все файлы определённого размера
                        Pages.ShowFilesSpecifiedSize();
                        break;
                    case "7":
                        // Найти файл по имени
                        Pages.ShowFilesByName();
                        break;
                    case "exit":
                        // просто выходим
                        return;
                    default:
                        // Если ввели что-то кривое
                        Console.WriteLine("Неверный ввод");
                        Console.ReadKey();
                        break;
                }

                Console.Clear();
            }
        }

        /// <summary>
        /// Вывод главного меню
        /// </summary>
        private static void PrintMainMenu()
        {
            Console.WriteLine($"findFiles - главное меню");
            Console.WriteLine("Выберите пункт меню:");
            Console.WriteLine("1. Поиск всех файлов - (Показать все файлы в текущей директории)");
            Console.WriteLine(
                "2. Поиск файлов в определённой папке - (Показать все файлы в указанной директории)");
            Console.WriteLine(
                "3. Поиск файлов по разрешениям - (Показать все файлы с определённой маской прав)");
            Console.WriteLine(
                "4. Поиск файлов по группам - (Показать все файлы, принадлежащие группе)");
            Console.WriteLine(
                "5. Поиск файлов по пользователю - (Показать все файлы, принадлежащие пользователю)");
            Console.WriteLine(
                "6. Поиск файлов по размеру - (Показать все файлы определённого размера)");
            Console.WriteLine("7. Поиск файла по имени");
            Console.WriteLine("exit. Выход");
            Console.Write("\n> ");
        }
    }
}