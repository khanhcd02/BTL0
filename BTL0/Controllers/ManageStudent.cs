using BTL0.Models;
using BTL0.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL0.Controllers
{
    internal class ManageStudent
    {
        public static List<Student> students = new List<Student>();
        public void ReadStudentInfo(Student student)
        {
            Console.WriteLine(student.ToString());
            Console.WriteLine("--------------------------");
        }

        public void ReadStudents()
        {
            Console.WriteLine("LIST:");
            foreach (Student student in students)
            {
                ReadStudentInfo(student);
            }
        }

        public Student InputID()
        {
            int id = 0;
            Console.Write("Enter student ID:");
            while (true)
            {
                var InputId = Console.ReadLine();
                var isInt = Int32.TryParse(InputId, out int result);
                if (!isInt)
                {
                    Console.WriteLine("Input again!");
                    continue;
                }
                id = result;
                break;
            }

            Student student = students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                Console.WriteLine("Student does not exist!");
                return null;
            }
            return student;
        }
        
        public void CreateStudent()
        {
            int id = students.Count > 0 ? students.Max(s => s.Id) + 1 : 1;
            string name = Valid.GetUserInput("Please enter name: ", Valid.GetValidName);
            string birthDay = Valid.GetUserInput("Please enter birth day  (dd/MM/yyyy): ", Valid.ValidateBirthDay);
            string address = Valid.GetUserInput("Please enter address: ", Valid.GetValidAddres);
            string height = Valid.GetUserInput("Please enter height (cm): ", Valid.ValidateHeight);
            string weight = Valid.GetUserInput("Please enter weight (kg):", Valid.ValidateWeight);
            string studentCode = Valid.GetUserInput("Please enter student code : ", (input) => Valid.ValidateStudentCode(input, students));
            string school = Valid.GetUserInput("Please enter school: ", Valid.ValidateSchool);
            string startYear = Valid.GetUserInput("Please enter start year (yyyy): ", Valid.ValidateStartYear);
            string gpa = Valid.GetUserInput("Please enter GPA (double): ", Valid.ValidateGPA);
            Student newStudent = new Student(id, name, DateTime.ParseExact(birthDay, "dd/MM/yyyy", CultureInfo.InvariantCulture),
            address, double.Parse(height), double.Parse(weight), studentCode, school, int.Parse(startYear), double.Parse(gpa));
            students.Add(newStudent);
            Console.WriteLine("Student created successfully!");
        }

        public void UpdateStudent(Student student)
        {
            ReadStudentInfo(student);
            Console.WriteLine("Select the information you want to update:");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Address");
            Console.WriteLine("3. School");
            Console.WriteLine("4. GPA");
            Console.WriteLine("5. Birth Day");
            Console.WriteLine("6. Start Year");
            Console.WriteLine("7. Height");
            Console.WriteLine("8. Weight");
            Console.WriteLine("9. Exit update");
            while (true)
            {
                Console.Write("Enter your choice: ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        student.Name = Valid.GetUserInput("Enter new name: ", Valid.GetValidName);
                        break;
                    case 2:
                        student.Address = Valid.GetUserInput("Enter new address: ", Valid.GetValidAddres);
                        break;
                    case 3:
                        student.School = Valid.GetUserInput("Enter new school: ", Valid.ValidateSchool);
                        break;
                    case 4:
                        student.GPA = double.Parse(Valid.GetUserInput("Enter new GPA: ", Valid.ValidateGPA));
                        student.AcademicPerformance = Student.AutoUpdateRank(student.GPA);
                        break;
                    case 5:
                        student.BirthDay = DateTime.ParseExact(Valid.GetUserInput("Enter new birth day (dd/MM/yyyy): ", Valid.ValidateBirthDay), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        break;
                    case 6:
                        student.StartYear = int.Parse(Valid.GetUserInput("Enter new start year (yyyy): ", Valid.ValidateStartYear));
                        break;
                    case 7:
                        student.Height = double.Parse(Valid.GetUserInput("Enter new height (cm): ", Valid.ValidateHeight));
                        break;
                    case 8:
                        student.Weight = double.Parse(Valid.GetUserInput("Enter new weight (kg): ", Valid.ValidateWeight));
                        break;
                    case 9:
                        Console.WriteLine("Update completed.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }

                Console.WriteLine("Update successful!");
                ReadStudentInfo(student);
            }

        }

        public void DeleteStudent(Student student)
        {
            students.Remove(student);
            Console.WriteLine("Student deleted successfully!");
            ReadStudents();
        }

        public void RankPercent()
        {
            Dictionary<Rank, int> performanceCount = new Dictionary<Rank, int>();
            int totalStudents = students.Count;

            foreach (var student in students)
            {
                if (performanceCount.ContainsKey(student.AcademicPerformance))
                    performanceCount[student.AcademicPerformance]++;
                else
                    performanceCount[student.AcademicPerformance] = 1;
            }
            var sortedPerformance = performanceCount.OrderByDescending(x => x.Value).ToList();
            Console.WriteLine("\nSorted by percentage (descending):");
            foreach (var kvp in sortedPerformance)
            {
                double percentage = (double)kvp.Value / totalStudents * 100;
                Console.WriteLine($"{kvp.Key}: {kvp.Value} students, {percentage}%");
            }
        }

        public void GPAPercent()
        {
            Dictionary<double, int> GPACount = new Dictionary<double, int>();
            int totalStudents = students.Count;

            foreach (var student in students)
                if (GPACount.ContainsKey(student.GPA))
                    GPACount[student.GPA]++;
                else
                    GPACount[student.GPA] = 1;
            var sorted = GPACount.OrderBy(x => x.Key).ToList();
            Console.WriteLine("\nSorted by GPA (ascending):");
            foreach (var kvp in sorted)
            {
                double percentage = (double)kvp.Value / totalStudents * 100;
                Console.WriteLine($"{kvp.Key}: {kvp.Value} students, {percentage}%");
            }
        }

        public void ListOfStudentsByRanking()
        {
            int inputKey;
            Dictionary<int, string> rankMappings = new Dictionary<int, string>{ { 1, "Poor" }, { 2, "Weak" }, { 3, "Average" }, { 4, "Good" }, { 5, "VeryGood" }, { 6, "Excellent" } };
            Console.WriteLine("Enter the corresponding number for each rank:\n1 - Poor, 2 - Weak, 3 - Average, 4 - Good, 5 - VeryGood, 6 - Excellent");
            while (true)
            {
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out inputKey))
                    if (rankMappings.ContainsKey(inputKey))
                        break;
                Console.WriteLine("Enter a valid number from 1 to 6:");
            }
            List<Student> filteredStudents = students.FindAll(student => student.AcademicPerformance.ToString().Equals(rankMappings[inputKey], StringComparison.OrdinalIgnoreCase));
            if (filteredStudents.Count > 0)
            {
               foreach (var student in filteredStudents)
               {
                    Console.WriteLine(student.ToString());
                    Console.WriteLine("-----------------------");
                }
            }
            else
                Console.WriteLine($"No students found with Academic Performance: {rankMappings[inputKey]}.");
        }

        public void SaveStudentsToFile()
        {
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string folderPath = Path.Combine(projectDirectory, "Data");
            string filePath = Path.Combine(folderPath, "students.txt");

            StringBuilder sb = new StringBuilder();
            foreach (Student student in students)
            {
                sb.AppendLine(student.ToString());
                sb.AppendLine("--------------------------");
            }
            File.WriteAllText(filePath, sb.ToString());
            Console.WriteLine("Students saved to file successfully.");
        }
        public void LoadStudentsFromFile()
        {
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = Path.Combine(projectDirectory, "Data", "students.txt");

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                int id = 0;
                string name = string.Empty;
                DateTime birthday = DateTime.Now;
                string address = string.Empty;
                double height = 0;
                double weight = 0;
                string studentCode = string.Empty;
                string school = string.Empty;
                int startYear = 0;
                double gpa = 0;
                Rank rank = Rank.Poor;
                foreach (string line in lines)
                {
                    if (line.StartsWith("Id="))
                    {
                        int.TryParse(line.Substring(3), out id);
                    }
                    else if (line.StartsWith("Name="))
                    {
                        name = line.Substring(5);
                    }
                    else if (line.StartsWith("BirthDay="))
                    {
                        birthday = DateTime.ParseExact(line.Substring(9), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    else if (line.StartsWith("Address="))
                    {
                        address = line.Substring(8);
                    }
                    else if (line.StartsWith("Height="))
                    {
                        double.TryParse(line.Substring(7), out height);
                    }
                    else if (line.StartsWith("Weight="))
                    {
                        double.TryParse(line.Substring(7), out weight);
                    }
                    else if (line.StartsWith("CodeStudent="))
                    {
                        studentCode = line.Substring(12);
                    }
                    else if (line.StartsWith("School="))
                    {
                        school = line.Substring(7);
                    }
                    else if (line.StartsWith("StartYear="))
                    {
                        int.TryParse(line.Substring(10), out startYear);
                    }
                    else if (line.StartsWith("GPA="))
                    {
                        double.TryParse(line.Substring(4), out gpa);
                    }
                    else if (line.StartsWith("AcademicPerforman="))
                    {
                        Enum.TryParse(line.Substring(18), true, out rank);

                        Student student = new Student
                        {
                            Id = id,
                            Name = name,
                            BirthDay = birthday,
                            Address = address,
                            Height = height,
                            Weight = weight,
                            StudentCode = studentCode,
                            School = school,
                            StartYear = startYear,
                            GPA = gpa,
                            AcademicPerformance = rank
                        };

                        students.Add(student);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading students from file: {ex.Message}");
            }
        }

    }
}