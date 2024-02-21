using System;

namespace BTL0.Models
{
    internal class Student : Person
    {
        public string StudentCode { get; set; }
        public string School { get; set; }
        public int StartYear { get; set; }
        public double GPA { get; set; }
        public Rank AcademicPerformance { get; set; }

        public static Rank AutoUpdateRank(double gpa)
        {
            if (gpa < 3)
                return Rank.Poor;
            if (gpa < 5)
                return Rank.Weak;
            if (gpa < 6.5)
                return Rank.Average;
            if (gpa < 7.5)
                return Rank.Good;
            if (gpa < 9)
                return Rank.VeryGood;
            return Rank.Excellent;
        }

        public Student() { }

        public Student(int id, string name, DateTime birthDay, string address, double? height, double? weight,
                        string studentCode, string school, int startYear, double gpa)
                       : base(id, name, birthDay, address, height, weight)
        {
            StudentCode = studentCode;
            School = school;
            StartYear = startYear;
            GPA = gpa;
            AcademicPerformance = AutoUpdateRank(gpa);
        }

        public override string ToString()
        {
            return $"{base.ToString()}" + $"CodeStudent={StudentCode}\n" + $"School={School}\n" + $"StartYear={StartYear}\n" + $"GPA={GPA}\n" + $"AcademicPerformance={AutoUpdateRank(GPA)}";
        }
    }
}
