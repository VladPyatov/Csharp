using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    enum SubjectSet { MatAn, PE, English }
    class Program
    {
        static void Main(string[] args)
        {
            Test t = new Test(SubjectSet.MatAn, new DateTime(2019,10,9), true);
            Console.WriteLine(t);
            Console.WriteLine(t.D_M);

            Test[] mas = new Test[5] { new Test(SubjectSet.PE, new DateTime(2017, 12, 21), true), new Test(SubjectSet.English, new DateTime(2019, 12, 18), true),
                                         new Test(SubjectSet.MatAn, new DateTime(2017, 12, 18), true), new Test(SubjectSet.English, new DateTime(2018, 5, 25), false),
                                        new Test(SubjectSet.PE, new DateTime(2018, 5, 20), true)};

            Console.WriteLine("Array:");
            for (int j = 0; j < mas.Length; j++)
                Console.WriteLine(mas[j]);

            Test[][] mas2 = new Test[Enum.GetNames(typeof(SubjectSet)).Length][];

            for (int j = 0; j < mas2.Length; j++){
                int k = 0;
                for (int i = 0; i < mas.Length; i++)
                {
                    if (mas[i].sub == (SubjectSet)j)
                        k++;
                }
                mas2[j] = new Test[k];
                k = 0;
                for (int i = 0; i < mas.Length; i++)
                {
                    if (mas[i].sub == (SubjectSet)j)
                    {
                        mas2[j][k] = mas[i];
                        k++;
                    }
                }
            }
            
            Console.WriteLine("Jagged array:");
            for (int j = 0; j < mas2.Length; j++)
            {
                for (int i = 0; i < mas2[j].Length; i++)
                    Console.Write(mas2[j][i] + "; ");
                Console.WriteLine();
            }
        }
    }
}
