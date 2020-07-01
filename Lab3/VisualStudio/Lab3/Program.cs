using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    interface IDeepCopy { object DeepCopy(); }
    class Program
    {
        static bool HasPublication(Person ps)
        {
            if (ps is Researcher && (ps as Researcher).num > 0) return true;
            else return false;
        }
        static void Main(string[] args)
        {
            Person person1 = new Person("Vlad", "Pyatov", new DateTime(2019, 9, 10));
            Person person2 = new Person("Vlad", "Pyatov", new DateTime(2019, 10, 10));
            Person person3 = null;
            Console.WriteLine(person1 == person2);
            Console.WriteLine(person3 == person1);
            Console.WriteLine(person3 == null);
            Researcher researcher1 = new Researcher("Vlad", "Pyatov", new DateTime(2019, 10, 10), "Physics", 2);
            Researcher researcher2 = (Researcher)researcher1.DeepCopy();
            researcher2.Name[0] = "Vladislav";
            researcher2.Topic = "Biology";
            researcher2.num = 10;
            Console.WriteLine(researcher1);
            Console.WriteLine(researcher2);
            Person ps = new Researcher();
            Researcher rs = (Researcher)ps.DeepCopy();
            Console.WriteLine(ps);
            Console.WriteLine(rs);

            /////////////Lab3
            Programmer p = new Programmer("V", "P", new DateTime(2019, 12, 1), "C++", 5);

            //1
            Team t = new Team("MSU");
            t.AddDefaults();
            Console.WriteLine("..1..\n"+t);

            //2
            Team tt = (Team)t.DeepCopy();
            tt.Topic[0] = p;
            Console.WriteLine("..2..\n" + tt);

            //3
            Console.WriteLine("..3..");
            foreach (Person pers in tt.Subset(tt.IsProgrammer))
            {
                Console.Write(pers + "; ");
            }
            Console.WriteLine();
            //4
            Console.WriteLine("..4..");
            tt.AddPerson(researcher1);
            foreach(Person pers in tt.Subset(HasPublication))
            {
                Console.Write(pers + "; ");
            }

        }
    }
}

