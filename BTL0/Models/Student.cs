using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL0.Models
{
    internal class Student : Person
    {
        public string StudentCode { get; set; }
        public string School { get; set; }
        public int StartYear { get; set; }
        public double GPA { get; set; }
        public Rank AcademicPerformance { get; set; }

        public static Rank AutoUpdateRank(double GPA)
        {
            if (GPA < 3)
                return Rank.Poor;
            if (GPA < 5)
                return Rank.Weak;
            if (GPA < 6.5)
                return Rank.Average;
            if (GPA < 7.5)
                return Rank.Good;
            if (GPA < 9)
                return Rank.VeryGood;
            return Rank.Excellent;
        }
        public Student()
        {

        }

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
            return $"{base.ToString()}" +
                   $"CodeStudent={StudentCode}\n" +
                   $"School={School}\n" +
                   $"StartYear={StartYear}\n" +
                   $"GPA={GPA}\n" +
                   $"AcademicPerforman={AutoUpdateRank(GPA)}";
        }

    }
}
