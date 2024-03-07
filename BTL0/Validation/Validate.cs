using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BTL0.Models;
namespace BTL0.Validation
{
    internal class Validate
    {
        public static (bool isSuccess, string Message) GetValidName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return (false, "Name is not null or empty!");
            if (name.Length > Constant.Constant.Max_Length_Name)
                return (false, $"Name must be less than {Constant.Constant.Max_Length_Name} characters!");
            return (true, string.Empty);
        }

        public static (bool isSuccess, string Message) GetValidBirthDay(string date)
        {
            if (string.IsNullOrEmpty(date))
                return (false, "DayOfBirth is not null or empty!");
            if (!DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var convertDate))
                return (false, "Enter the date in the format dd/MM/yyyy.");
            if (convertDate.Year < Constant.Constant.Min_Year_Of_Admission)
                return (false, $"Year must be greater than {Constant.Constant.Min_Year_Of_Admission}!");
            return (true, string.Empty);
        }

        public static (bool isSuccess, string Message) GetValidAddres(string address)
        {
            if (address.Length > Constant.Constant.Max_Length_Address)
                return (false, $"Address must be less than {Constant.Constant.Max_Length_Address} characters!");
            return (true, string.Empty);
        }

        public static (bool isSuccess, string Message) GetValidHeight(string height)
        {
            if (!double.TryParse(height, out var check))
                return (false, "Incorrect input format!");
            if (check < Constant.Constant.Min_Height || check > Constant.Constant.Max_Height)
                return (false, $"Height must be between {Constant.Constant.Min_Height} and {Constant.Constant.Max_Height}!");
            return (true, string.Empty);
        }

        public static (bool isSuccess, string Message) GetValidWeight(string weight)
        {
            if (!double.TryParse(weight, out var convertWeight))
                return (false, "Incorrect input format!");
            if (convertWeight < Constant.Constant.Min_Weight || convertWeight > Constant.Constant.Max_Weight)
                return (false, $"Weight must be between {Constant.Constant.Min_Weight} and {Constant.Constant.Max_Weight}!");
            return (true, string.Empty);
        }

        public static (bool isSuccess, string Message) GetValidStudentCode(string studentCode, List<Student> students)
        {
            if (string.IsNullOrEmpty(studentCode))
                return (false, "Student code is not null or empty!");
            if (studentCode.Contains(" "))
                return (false, "Student code cannot contain spaces!");
            if (studentCode.Length != Constant.Constant.Max_Length_Student_Code)
                return (false, $"Student code must have {Constant.Constant.Max_Length_Student_Code} characters!");
            if (students.Any(student => student != null && student.StudentCode == studentCode))
                return (false, "Student code already exists");
            return (true, string.Empty);
        }

        public static (bool isSuccess, string Message) GetValidSchool(string school)
        {
            if (string.IsNullOrEmpty(school))
                return (false, "School is not null or empty!");
            if (school.Length > Constant.Constant.Max_Length_School_Name)
                return (false, $"School must be less than {Constant.Constant.Max_Length_School_Name} characters!");
            return (true, string.Empty);
        }

        public static (bool isSuccess, string Message) GetValidStartYear(string year)
        {
            if (string.IsNullOrEmpty(year))
                return (false, "Year is not null or empty!");
            if (!int.TryParse(year, out var convertYear))
                return (false, "Incorrect input format!");
            if (convertYear < Constant.Constant.Min_Year_Of_Admission || year.Length != Constant.Constant.Length_Year_Of_Admission)
                return (false, $"Year must be a valid {Constant.Constant.Length_Year_Of_Admission}-digit number from {Constant.Constant.Min_Year_Of_Admission} onwards!");
            return (true, string.Empty);
        }

        public static (bool isSuccess, string Message) GetValidGPA(string gpa)
        {
            if (string.IsNullOrEmpty(gpa))
                return (false, "GPA is not null or empty!");
            if (!double.TryParse(gpa, out var convertGPA))
                return (false, "Incorrect input format!");
            if (convertGPA < Constant.Constant.Min_GPA || convertGPA > Constant.Constant.Max_GPA)
                return (false, $"GPA must be between {Constant.Constant.Min_GPA} and {Constant.Constant.Max_GPA}!");
            return (true, string.Empty);
        }

        public static (bool isSuccess, string Message) GetValidAcademicPerformance(string academicPerformance)
        {
            if (string.IsNullOrEmpty(academicPerformance))
                return (false, "AcademicPerformance is not null or empty!");
            return (true, string.Empty);
        }

        public static string GetUserInput(string prompt, Func<string, (bool isSuccess, string Message)> validator)
        {
            string userInput;
            bool isFieldValid;
            do
            {
                isFieldValid = true;
                Console.Write(prompt);
                userInput = Console.ReadLine()?.Trim();
                var ValidationMessage = validator(userInput);
                if (!ValidationMessage.isSuccess)
                {
                    Console.WriteLine(ValidationMessage.Message);
                    isFieldValid = false;
                }
            } while (!isFieldValid);
            return userInput;
        }
    }
}