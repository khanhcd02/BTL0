using System;

namespace BTL0.Models
{
    internal class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public string Address { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }

        public Person() { }

        public Person(int id, string name, DateTime birthDay, string address, double? height, double? weight)
        {
            ID = id;
            Name = name;
            BirthDay = birthDay;
            Address = address;
            Height = height;
            Weight = weight;
        }

        public override string ToString()
        {
            return $"ID={ID}\n" + $"Name={Name}\n" + $"BirthDay={BirthDay.ToString("dd/MM/yyyy")}\n" + $"Address={Address}\n" + $"Height={Height}\n" + $"Weight={Weight}\n";
        }
    }
}
