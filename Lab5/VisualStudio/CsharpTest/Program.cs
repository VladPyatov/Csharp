using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Csharp;

namespace CsharpTest
{
    class Program
    {
        [DllImport("C:\\Users\\Владислав\\source\\repos\\Lab5\\Debug\\C++.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void F(double[] A, double[] x, double[] b, double N, double blockN, ref double time);

        public static void Test()
        {
            Console.WriteLine("##########################################");
            Console.WriteLine("///////start///////Test///////start///////");
            Console.WriteLine("##########################################\n");

            blockmatrix blocktest = new blockmatrix(3, 3);
            vectorb vec = new vectorb(3, 3);
            vec[0][0] = 0;
            vec[0][1] = 1;
            vec[0][2] = 2;
            vec[1][0] = 0;
            vec[1][1] = 1;
            vec[1][2] = 2;
            vec[2][0] = 0;
            vec[2][1] = 1;
            vec[2][2] = 2;

            Console.WriteLine("Test matrix:\n"+blocktest);
            Console.WriteLine("Test vector:\n" + vec);
            //Решение СЛАУ на С#
            vectorb ans = blocktest.SLAU(vec);
            
            //Сохранение результатор
            blocktest.save(ans, vec);

            Console.WriteLine("Matrix*answer=");
            Console.WriteLine(blocktest.vecMul(ans));

            //перевод вектора правой части в массив
            double[] b = new double[vec.GetBlockN * vec.GetN];
            int n = 0;
            for (var i = 0; i < vec.GetN; i++)
                for (var j = 0; j < vec.GetBlockN; j++)
                    b[n++] = vec[i][j];

            //перевод блочной 3-диагональной в один массив
            double[] mat = new double[blocktest.GetblockN * 4 + 3 * blocktest.GetblockN * (blocktest.GetN - 2)];
            int mat_index = 0;

            for (var j = 0; j < blocktest.GetN; j++)
            {
                if (j != 0)
                    for (var i = 0; i < blocktest.GetblockN; i++)
                    {
                        mat[mat_index] = blocktest.A[j][i, i];
                        mat_index++;
                    }
                for (var i = 0; i < blocktest.GetblockN; i++)
                {
                    mat[mat_index] = blocktest.C[j][i, i];
                    mat_index++;
                }
                if (j != blocktest.GetN - 1)
                    for (var i = 0; i < blocktest.GetblockN; i++)
                    {
                        mat[mat_index] = blocktest.B[j][i, i];
                        mat_index++;
                    }
            }

            //объявление массива решения
            double[] x = new double[vec.GetN* vec.GetBlockN];

            //Решение системы на С++
            double time=-1;
            F(mat, x, b, vec.GetN, vec.GetBlockN,ref time);

            //приведение массива решения к типу вектор
            vectorb output = new vectorb(x, vec.GetN, vec.GetBlockN);

            Console.WriteLine("Answer=");
            Console.WriteLine(output);

            Console.WriteLine("##########################################");
            Console.WriteLine("////////end////////Test////////end////////");
            Console.WriteLine("##########################################\n");


        }
        static void Main(string[] args)
        {
            Console.WriteLine("Pyatov Vladislav - 301 group.");

            //подготовка
            if (Directory.Exists(@"c:\Lab5_Pyatov"))
            {
                try
                {
                    File.Delete(@"c:\Lab5_Pyatov\C#_test.txt");
                    File.Delete(@"c:\Lab5_Pyatov\C++_test.txt");
                }
                catch(Exception ex) 
                {
                    Console.WriteLine("Delete exception: " + ex.Message);
                }

            }

            Test();
            TestTime t= new TestTime();
            TestTime.Load(@"c:\Lab5_Pyatov\TestTime.txt", ref t);

            int N, blockN;
            Console.WriteLine("Press any key to start programm or <Escape> to exit\n");
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                try
                {
                    string s = "";
                    Console.WriteLine("Enter Matrix order:");
                    N = Convert.ToInt32(Console.ReadLine());
                    if (N < 1)
                        throw new ArgumentException("Matrix order should be greater then zero");
                    Console.WriteLine("Enter block order:");
                    blockN = Convert.ToInt32(Console.ReadLine());
                    if (blockN < 1)
                        throw new ArgumentException("Block order should be greater than zero");
                    s += "Matrix order = " + N + ", block order = " + blockN +", ";

                    //Вычисление на С#
                    blockmatrix b_matrix = new blockmatrix(N, blockN);
                    vectorb b_vector = new vectorb(N, blockN, true);
                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();
                    vectorb ans = b_matrix.SLAU(b_vector);
                    stopwatch.Stop();

                    //Сохранение
                    b_matrix.save(ans, b_vector);

                    double time_Csharp = stopwatch.ElapsedMilliseconds;
                    s += "C# time = " + (stopwatch.ElapsedMilliseconds).ToString() + "ms , ";

                    //Вычисление на С++
                    //перевод вектора правой части в массив
                    double[] b = new double[b_vector.GetBlockN * b_vector.GetN];
                    int n = 0;
                    for (var i = 0; i < b_vector.GetN; i++)
                        for (var j = 0; j < b_vector.GetBlockN; j++)
                            b[n++] = b_vector[i][j];

                    //перевод блочной 3-диагональной в один массив
                    double[] mat = new double[b_matrix.GetblockN * 4 + 3 * b_matrix.GetblockN * (b_matrix.GetN - 2)];
                    int mat_index = 0;

                    for (var j = 0; j < b_matrix.GetN; j++)
                    {
                        if (j != 0)
                            for (var i = 0; i < b_matrix.GetblockN; i++)
                            {
                                mat[mat_index] = b_matrix.A[j][i, i];
                                mat_index++;
                            }
                        for (var i = 0; i < b_matrix.GetblockN; i++)
                        {
                            mat[mat_index] = b_matrix.C[j][i, i];
                            mat_index++;
                        }
                        if (j != b_matrix.GetN - 1)
                            for (var i = 0; i < b_matrix.GetblockN; i++)
                            {
                                mat[mat_index] = b_matrix.B[j][i, i];
                                mat_index++;
                            }
                    }

                    //объявление массива решения
                    double[] x = new double[b_vector.GetN * b_vector.GetBlockN];

                    //Решение системы на С++
                    double time = -1;
                    F(mat, x, b, b_vector.GetN, b_vector.GetBlockN, ref time);

                    //приведение массива решения к типу вектор
                    vectorb output = new vectorb(x, b_vector.GetN, b_vector.GetBlockN);

                    s += "C++ time = " + time.ToString() +"ms, ratio(C#/C++) = " +(time>0?time_Csharp/time:0)+";";

                    //Добавление в TestTime
                    t.Add(s);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine("Press any key to continue or <Escape> to exit");

            }
            Console.WriteLine(t);
            TestTime.Save(@"c:\Lab5_Pyatov\TestTime.txt", t);


        }

        
    }
}
