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
            if (name.Length > Constant.Constant.MaxLengthName)
                return (false, $"Name must be less than {Constant.Constant.MaxLengthName} characters!");
            return (true, string.Empty);
        }

        public static (bool isSuccess, string Message) GetValidBirthDay(string date)
        {
            DateTime convertDate;
            if (string.IsNullOrEmpty(date))
                return (false, "DayOfBirth is not null or empty!");
            if (!DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out convertDate))
                return (false, "Enter the date in the format dd/MM/yyyy.");
            if (convertDate.Year < Constant.Constant.MinYearOfAdmission)
                return (false, $"Year must be greater than {Constant.Constant.MinYearOfAdmission}!");
            return (true, string.Empty);
        }

        public static (bool isSuccess, string Message) GetValidAddres(string address)
        {
            if (address.Length > Constant.Constant.MaxLengthAddress)
                return (false, $"Address must be less than {Constant.Constant.MaxLengthAddress} characters!");
            return (true, string.Empty);
        }

        public static (bool isSuccess, string Message) GetValidHeight(string height)
        {
            double check;
            if (!double.TryParse(height, out check))
                return (false, "Incorrect input format!");
            if (check < Constant.Constant.MinHeight || check > Constant.Constant.MaxHeight)
                return (false, $"Height must be between {Constant.Constant.MinHeight} and {Constant.Constant.MaxHeight}!");
            return (true, string.Empty);
        }

        public static (bool isSuccess, string Message) GetValidWeight(string weight)
        {
            double convertWeight;
            if (!double.TryParse(weight, out convertWeight))
                return (false, "Incorrect input format!");
            if (convertWeight < Constant.Constant.MinWeight || convertWeight > Constant.Constant.MaxWeight)
                return (false, $"Weight must be between {Constant.Constant.MinWeight} and {Constant.Constant.MaxWeight}!");
            return (true, string.Empty);
        }

        public static (bool isSuccess, string Message) GetValidStudentCode(string studentCode, List<Student> students)
        {
            if (string.IsNullOrEmpty(studentCode))
                return (false, "Student code is not null or empty!");
            if (studentCode.Contains(" "))
                return (false, "Student code cannot contain spaces!");
            if (studentCode.Length != Constant.Constant.MaxLengthStudentCode)
                return (false, $"Student code must have {Constant.Constant.MaxLengthStudentCode} characters!");
            if (students.Any(student => student != null && student.StudentCode == studentCode))
                return (false, "Student code already exists");
            return (true, string.Empty);
        }

        public static (bool isSuccess, string Message) GetValidSchool(string school)
        {
            if (string.IsNullOrEmpty(school))
                return (false, "School is not null or empty!");
            if (school.Length > Constant.Constant.MaxLengthSchoolName)
                return (false, $"School must be less than {Constant.Constant.MaxLengthSchoolName} characters!");
            return (true, string.Empty);
        }

        public static (bool isSuccess, string Message) GetValidStartYear(string year)
        {
            int convertYear;
            if (string.IsNullOrEmpty(year))
                return (false, "Year is not null or empty!");
            if (!int.TryParse(year, out convertYear))
                return (false, "Incorrect input format!");
            if (convertYear < Constant.Constant.MinYearOfAdmission || year.Length != Constant.Constant.LengthYearOfAdmission)
                return (false, $"Year must be a valid {Constant.Constant.LengthYearOfAdmission}-digit number from {Constant.Constant.MinYearOfAdmission} onwards!");
            return (true, string.Empty);
        }

        public static (bool isSuccess, string Message) GetValidGPA(string gpa)
        {
            double convertGPA;
            if (string.IsNullOrEmpty(gpa))
                return (false, "GPA is not null or empty!");
            if (!double.TryParse(gpa, out convertGPA))
                return (false, "Incorrect input format!");
            if (convertGPA < Constant.Constant.MinGPA || convertGPA > Constant.Constant.MaxGPA)
                return (false, $"GPA must be between {Constant.Constant.MinGPA} and {Constant.Constant.MaxGPA}!");
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
