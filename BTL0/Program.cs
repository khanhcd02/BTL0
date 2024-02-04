using BTL0.Controllers;
using BTL0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                int key = int.Parse(Console.ReadLine());
                switch (key)
                {
                    case 1:
                        DisplayMenu();
                        break;
                    case 2:
                        AppController.ReadStudents();
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
        static void DisplayMenu()
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
                int key = int.Parse(Console.ReadLine());
                Student student;
                switch (key)
                {
                    case 1:
                        AppController.CreateStudent();
                        break;
                    case 2:
                        student = AppController.InputID();
                        AppController.ReadStudentInfo(student);
                        break;
                    case 3:
                        student = AppController.InputID();
                        AppController.UpdateStudent(student);
                        break;
                    case 4:
                        student = AppController.InputID();
                        AppController.DeleteStudent(student);
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
            Console.WriteLine("3. Exit");
            Console.WriteLine("--------------------------------------");
        }
    }
}
