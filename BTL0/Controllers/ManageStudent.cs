using BTL0.Models;
using BTL0.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace BTL0.Controllers
{
    internal class ManageStudent
    {
        public static List<Student> studentsList = new List<Student>();

        public void ReadStudentInfo(Student studentInfo)
        {
            Console.WriteLine(studentInfo.ToString());
            Console.WriteLine("--------------------------");
        }

        public void ReadStudentsList()
        {
            Console.WriteLine("LIST:");
            foreach (var student in studentsList)
            {
                ReadStudentInfo(student);
            }
        }

        public Student InputId()
        {
            var id = 0;
            Console.Write("Enter student ID:");
            while (true)
            {
                if (!Int32.TryParse(Console.ReadLine(), out var result))
                {
                    Console.WriteLine("Input again!");
                    Console.Write("Enter student ID:");
                    continue;
                }
                id = result;
                break;
            }
            var resultStudent = studentsList.FirstOrDefault(x => x.ID == id);
            if (resultStudent == null)
            {
                Console.WriteLine("Student does not exist!");
                return null;
            }
            return resultStudent;
        }
        
        public int IncreaseId()
        {
            return studentsList.Count > 0 ? studentsList.Max(s => s.ID) + 1 : 1;
        }

        public void CreateStudent()
        {
            var id = IncreaseId();
            var name = Validate.GetUserInput("Please enter name: ", Validate.GetValidName);
            var birthDay = Validate.GetUserInput("Please enter birth day  (dd/MM/yyyy): ", Validate.GetValidBirthDay);
            var address = Validate.GetUserInput("Please enter address: ", Validate.GetValidAddres);
            var height = Validate.GetUserInput("Please enter height (cm): ", Validate.GetValidHeight);
            var weight = Validate.GetUserInput("Please enter weight (kg):", Validate.GetValidWeight);
            var studentCode = Validate.GetUserInput("Please enter student code : ", (input) => Validate.GetValidStudentCode(input, studentsList));
            var school = Validate.GetUserInput("Please enter school: ", Validate.GetValidSchool);
            var startYear = Validate.GetUserInput("Please enter start year (yyyy): ", Validate.GetValidStartYear);
            var gpa = Validate.GetUserInput("Please enter GPA (double): ", Validate.GetValidGPA);
            var newStudent = new Student(id, name, DateTime.ParseExact(birthDay, "dd/MM/yyyy", CultureInfo.InvariantCulture),
            address, double.Parse(height), double.Parse(weight), studentCode, school, int.Parse(startYear), double.Parse(gpa));
            studentsList.Add(newStudent);
            Console.WriteLine("Student created successfully!");
        }

        public void UpdateStudent(Student studentUpdate)
        {
            ReadStudentInfo(studentUpdate);
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
                if (!int.TryParse(Console.ReadLine(), out var choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        studentUpdate.Name = Validate.GetUserInput("Enter new name: ", Validate.GetValidName);
                        break;
                    case 2:
                        studentUpdate.Address = Validate.GetUserInput("Enter new address: ", Validate.GetValidAddres);
                        break;
                    case 3:
                        studentUpdate.School = Validate.GetUserInput("Enter new school: ", Validate.GetValidSchool);
                        break;
                    case 4:
                        studentUpdate.GPA = double.Parse(Validate.GetUserInput("Enter new GPA: ", Validate.GetValidGPA));
                        studentUpdate.AcademicPerformance = Student.AutoUpdateRank(studentUpdate.GPA);
                        break;
                    case 5:
                        studentUpdate.BirthDay = DateTime.ParseExact(Validate.GetUserInput("Enter new birth day (dd/MM/yyyy): ", Validate.GetValidBirthDay), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        break;
                    case 6:
                        studentUpdate.StartYear = int.Parse(Validate.GetUserInput("Enter new start year (yyyy): ", Validate.GetValidStartYear));
                        break;
                    case 7:
                        studentUpdate.Height = double.Parse(Validate.GetUserInput("Enter new height (cm): ", Validate.GetValidHeight));
                        break;
                    case 8:
                        studentUpdate.Weight = double.Parse(Validate.GetUserInput("Enter new weight (kg): ", Validate.GetValidWeight));
                        break;
                    case 9:
                        Console.WriteLine("Update completed.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
                Console.WriteLine("Update successful!");
                ReadStudentInfo(studentUpdate);
            }
        }

        public void DeleteStudent(Student studentDelete)
        {
            studentsList.Remove(studentDelete);
            Console.WriteLine("Student deleted successfully!");
            ReadStudentsList();
        }

        public void CRUDById(string key)
        {
            var resultStudent = InputId();
            if (resultStudent == null)
                return;
            if (key == "read")
                ReadStudentInfo(resultStudent);
            if (key == "update")
                UpdateStudent(resultStudent);
            if (key == "delete")
                DeleteStudent(resultStudent);
        }

        public void RankPercent()
        {
            var performanceCount = new Dictionary<Rank, int>();
            var totalStudents = studentsList.Count;

            foreach (var student in studentsList)
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
                var percentage = (double)kvp.Value / totalStudents * 100;
                Console.WriteLine($"{kvp.Key}: {kvp.Value} students, {percentage}%");
            }
        }

        public void GPAPercent()
        {
            var gpaCount = new Dictionary<double, int>();
            var totalStudents = studentsList.Count;

            foreach (var student in studentsList)
                if (gpaCount.ContainsKey(student.GPA))
                    gpaCount[student.GPA]++;
                else
                    gpaCount[student.GPA] = 1;
            var sorted = gpaCount.OrderBy(x => x.Key).ToList();
            Console.WriteLine("\nSorted by GPA (ascending):");
            foreach (var kvp in sorted)
            {
                var percentage = (double)kvp.Value / totalStudents * 100;
                Console.WriteLine($"{kvp.Key}: {kvp.Value} students, {percentage}%");
            }
        }

        public void ListOfStudentsByRanking()
        {
            int inputKey;
            var rankMappings = new Dictionary<int, string>{ { 1, "Poor" }, { 2, "Weak" }, { 3, "Average" }, { 4, "Good" }, { 5, "VeryGood" }, { 6, "Excellent" } };
            Console.WriteLine("Enter the corresponding number for each rank:\n1 - Poor, 2 - Weak, 3 - Average, 4 - Good, 5 - VeryGood, 6 - Excellent");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out inputKey))
                    if (rankMappings.ContainsKey(inputKey))
                        break;
                Console.WriteLine("Enter a valid number from 1 to 6:");
            }
            var filteredStudents = studentsList.FindAll(student => student.AcademicPerformance.ToString().Equals(rankMappings[inputKey], StringComparison.OrdinalIgnoreCase));
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
            var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var folderPath = Path.Combine(projectDirectory, "Data");
            var filePath = Path.Combine(folderPath, "students.txt");
            var sb = new StringBuilder();
            foreach (Student student in studentsList)
            {
                sb.AppendLine(student.ToString());
                sb.AppendLine("--------------------------");
            }
            File.WriteAllText(filePath, sb.ToString());
            Console.WriteLine("Students saved to file successfully.");
        }

        public void LoadStudentsFromFile()
        {
            var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var filePath = Path.Combine(projectDirectory, "Data", "students.txt");
            try
            {
                var lines = File.ReadAllLines(filePath);
                var id = 0;
                var name = string.Empty;
                var birthDay = DateTime.Now;
                var address = string.Empty;
                var height = 0.0;
                var weight = 0.0;
                var studentCode = string.Empty;
                var school = string.Empty;
                var startYear = 0;
                var gpa = 0.0;
                var rank = Rank.Poor;
                foreach (string line in lines)
                {
                    if (line.StartsWith("ID="))
                        int.TryParse(line.Substring(3), out id);
                    else if (line.StartsWith("Name="))
                        name = line.Substring(5);
                    else if (line.StartsWith("BirthDay="))
                        birthDay = DateTime.ParseExact(line.Substring(9), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    else if (line.StartsWith("Address="))
                        address = line.Substring(8);
                    else if (line.StartsWith("Height="))
                        double.TryParse(line.Substring(7), out height);
                    else if (line.StartsWith("Weight="))
                        double.TryParse(line.Substring(7), out weight);
                    else if (line.StartsWith("CodeStudent="))
                        studentCode = line.Substring(12);
                    else if (line.StartsWith("School="))
                        school = line.Substring(7);
                    else if (line.StartsWith("StartYear="))
                        int.TryParse(line.Substring(10), out startYear);
                    else if (line.StartsWith("GPA="))
                        double.TryParse(line.Substring(4), out gpa);
                    else if (line.StartsWith("AcademicPerformance="))
                    {
                        Enum.TryParse(line.Substring(20), true, out rank);
                        var student = new Student
                        {
                            ID = id,
                            Name = name,
                            BirthDay = birthDay,
                            Address = address,
                            Height = height,
                            Weight = weight,
                            StudentCode = studentCode,
                            School = school,
                            StartYear = startYear,
                            GPA = gpa,
                            AcademicPerformance = rank
                        };
                        studentsList.Add(student);
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