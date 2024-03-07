using BTL0.Controllers;
using System;

namespace BTL0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------------------------------- ");
            ManageStudent AppController = new ManageStudent();
            AppController.LoadStudentsFromFile();
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
                        AppController.ReadStudentsList();
                        break;
                    case 3:
                        AppController.SaveStudentsToFile();
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
            ManageStudent AppController = new ManageStudent();
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
                        AppController.CreateStudent();
                        break;
                    case 2:
                        AppController.CRUDByID("read");
                        break;
                    case 3:
                        AppController.CRUDByID("update");
                        break;
                    case 4:
                        AppController.CRUDByID("delete");
                        break;
                    case 5:
                        AppController.RankPercent();
                        break;
                    case 6:
                        AppController.GPAPercent();
                        break;
                    case 7:
                        AppController.ListOfStudentsByRanking();
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