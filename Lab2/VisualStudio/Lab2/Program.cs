using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    interface IDeepCopy { object DeepCopy(); }
    class Program
    {
        static void Main(string[] args)
        {
            Person person1 = new Person("Vlad", "Pyatov", new DateTime(2019, 10, 10));
            Person person2 = new Person("Vlad", "Pyatov", new DateTime(2019, 10, 10));
            Person person3 = null;
            Console.WriteLine(person1 == person2);
            Console.WriteLine(person3 == person1);
            Console.WriteLine(person3 == null);
            Researcher researcher1 = new Researcher("Vlad", "Pyatov", new DateTime(2019, 10, 10), "Physics", 2);
            Researcher researcher2 = (Researcher) researcher1.DeepCopy();
            researcher2.Name[0] = "Vladislav";
            researcher2.Topic = "Biology";
            researcher2.num = 10;
            Console.WriteLine(researcher1);
            Console.WriteLine(researcher2);
            Person ps = new Researcher();
            Researcher rs = (Researcher) ps.DeepCopy();
            Console.WriteLine(ps);
            Console.WriteLine(rs);


        }
    }
}
