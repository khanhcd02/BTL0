using BTL0.Controllers;
using System;

namespace BTL0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------------------------------- ");
            var appController = new ManageStudent();
            appController.LoadStudentsFromFile();
            while (true)
            {
                MainMenu();
                int.TryParse(Console.ReadLine(), out var key);
                switch (key)
                {
                    case 1:
                        ManagerMenu();
                        break;
                    case 2:
                        appController.ReadStudentsList();
                        break;
                    case 3:
                        appController.SaveStudentsToFile();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid number from the menu.");
                        break;
                }
            }
        }

        static void ManagerMenu()
        {
            var appController = new ManageStudent();
            while (true)
            {
                Console.WriteLine("1. ADD STUDENT ");
                Console.WriteLine("2. Search Student for id");
                Console.WriteLine("3. Update for id ");
                Console.WriteLine("4. Delete for id ");
                Console.WriteLine("5. Show ranking By Percent");
                Console.WriteLine("6. Show GPA By Percent ");
                Console.WriteLine("7. Show List Of Students By Input Ranking");
                Console.WriteLine("8. Back");
                Console.WriteLine("--------------------------------------");
                int.TryParse(Console.ReadLine(), out var key);
                switch (key)
                {
                    case 1:
                        appController.CreateStudent();
                        break;
                    case 2:
                        appController.CRUDById("read");
                        break;
                    case 3:
                        appController.CRUDById("update");
                        break;
                    case 4:
                        appController.CRUDById("delete");
                        break;
                    case 5:
                        appController.RankPercent();
                        break;
                    case 6:
                        appController.GPAPercent();
                        break;
                    case 7:
                        appController.ListOfStudentsByRanking();
                        break;
                    case 8:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid number from the menu.");
                        break;
                }
            }
        }

        static void MainMenu()
        {
            Console.WriteLine("1. Manage Student ");
            Console.WriteLine("2. List Student ");
            Console.WriteLine("3. Save & Exit");
            Console.WriteLine("--------------------------------------");
        }
    }
}