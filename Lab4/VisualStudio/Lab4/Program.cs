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

            ///////////////Lab3
            //Programmer p = new Programmer("V", "P", new DateTime(2019, 12, 1), "C++", 5);
            //Programmer p3 = new Programmer("V", "P", new DateTime(2019, 12, 1), "C++", 5);
            //Console.WriteLine(ReferenceEquals(p,p3));
            //Programmer pr = new Programmer("P", "V", new DateTime(2019, 12, 1), "C++", 4);

            ////1
            //Team t = new Team("MSU");
            //t.AddDefaults();
            //Console.WriteLine("..1..\n"+t);

            ////2
            //Team tt = (Team)t.DeepCopy();
            //tt.Topic[0].Name[0] = "VV";
            //Console.WriteLine("..2..\n" + tt);
            //Console.WriteLine(t);
            //tt.AddPerson(p);
            //tt.AddPerson(pr);

            ////3
            //Console.WriteLine("..3..");
            //foreach (Person pers in tt.Subset(tt.IsProgrammer))
            //{
            //    Console.Write(pers + "; ");
            //}
            //Console.WriteLine();
            ////4
            //Console.WriteLine("..4..");
            //tt.AddPerson(researcher1);
            //foreach (Person pers in tt.Subset(HasPublication))
            //{
            //    Console.Write(pers + "; ");
            //}
            //Console.WriteLine();
            ////dop

            //Console.WriteLine("..dop..");
            //foreach (string s in tt.NonUniqueSubject())
            //{
            //    Console.Write(s + "; ");
            //}

            ////////////////////////Lab4
            Console.WriteLine("...Lab4...");
            TeamList tl = new TeamList();

            Console.WriteLine("...AddDef...");
            tl.AddDefaults();
            Console.WriteLine(tl);

            Console.WriteLine("...1...");
            Console.WriteLine("Максимальное число публикаций среди всех элементов типа Researcher");
            int test = tl.MaxPub;
            Console.WriteLine(test);

            Console.WriteLine("...2...");
            Console.WriteLine("Researcher с максимальным числом публикаций:");
            Researcher r_test = tl.MaxR;
            Console.WriteLine(r_test);

            Console.WriteLine("...3...");
            Console.WriteLine("Переменная запроса для перечисления в порядке возрастания стажа всех элементов типа Programmer");
            IEnumerable<Programmer> p_test = tl.ExpProg;
            foreach(Programmer prop in p_test)
            {
                Console.WriteLine(prop);
            }

            Console.WriteLine("...4...");
            Console.WriteLine("Переменная запроса для группирования по значению стажа всех элементов типа Programmer");
            var ps_test = tl.GrProg;
            foreach (var group in ps_test)
            {

                Console.WriteLine("key: "+ group.Key);
                foreach(Programmer pr in group)
                {
                    Console.WriteLine(pr);
                }
            }

            Console.WriteLine("...5...");
            Console.WriteLine("Переменная запроса для перечисления без повторов всех элементов Person, которые встречаются хотя бы в двух элементах Team");
            var allps_test = tl.AllPers;
            foreach(var p in allps_test)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine("...6...");
            Console.WriteLine("Переменная запроса для перечисления без повторов всех элементов Person, которые встречаются хотя бы в двух элементах Team");
            var alltopic_test = tl.AllTopics;
            foreach (var p in alltopic_test)
            {
                Console.WriteLine(p);
            }

        }
    }
}

