using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CsharpTest
{
    public class blockmatrix
    {
        private int N, blockN;

        public int GetN { get => this.N; }
        public int GetblockN { get => this.blockN; }
        public matrix[] A;
        public matrix[] C;
        public matrix[] B;


        public blockmatrix()
        {
            N = 0;
            blockN = 0;
            A = null;
            C = null;
            B = null;
        }

        public blockmatrix(int ord, int blockord)
        {
            N = ord;
            blockN = blockord;
            A = new matrix[N];
            C = new matrix[N];
            B = new matrix[N];

            for (var i = 0; i < N; i++)
            {
                A[i] = new matrix(1, blockN);
                C[i] = new matrix(2, blockN);
                B[i] = new matrix(3, blockN);
            }
            A[0] = null;
            B[N - 1] = null;

        }
        public override string ToString()
        {
            string s = "";
            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < blockN; j++)
                {
                    for (int g = 1; g < i; g++)
                    {
                        for (int k = 0; k < blockN; k++)
                            s += 0 + " ";
                        s += " ";
                    }
                    if (i != 0)
                    {
                        for (int k = 0; k < blockN; k++)
                            s += A[i][j, k] + " ";
                        s += " ";
                    }
                    for (int k = 0; k < blockN; k++)
                        s += C[i][j, k] + " ";
                    s += " ";
                    if (i != N - 1)
                    {
                        for (int k = 0; k < blockN; k++)
                            s += B[i][j, k] + " ";
                        s += " ";
                    }
                    for (int g = i + 2; g < N; g++)
                    {
                        for (int k = 0; k < blockN; k++)
                            s += 0 + " ";
                        s += " ";
                    }
                    s += "\n";
                }
                s += "\n";
            }

            return s;
        }

        public vectorb vecMul(vectorb v)
        {
            int i, j;
            if (N != v.GetN || blockN != v.GetBlockN)
                throw new ArgumentException("can't multiply matrix to vector");

            vectorb res = new vectorb(N, blockN);
            vectorb sum;
            for (i = 0; i < N; i++)
            {

                if (i == 0)
                {
                    sum = new vectorb(1, blockN);
                    sum += C[0] * v[0];
                    sum += B[0] * v[1];

                }
                else if (i == N - 1)
                {
                    sum = new vectorb(1, blockN);
                    sum += A[N - 1] * v[N - 2];
                    sum += C[N - 1] * v[N - 1];

                }
                else
                {
                    sum = new vectorb(1, blockN);
                    sum += A[i] * v[i - 1];
                    sum += C[i] * v[i];
                    sum += B[i] * v[i + 1];

                }
                for (j = 0; j < blockN; j++)
                    res[i][j] = sum[0][j];
            }
            return res;

        }
        public vectorb SLAU(vectorb v)
        {
            matrix[] a = new matrix[N];
            matrix m;
            vectorb[] b = new vectorb[N + 1];
            vectorb w;
            vectorb res = new vectorb(N, blockN);

            a[1] = matrix.inverse(C[0]) * B[0];
            for (int i = 1; i <= N - 2; i++)
            {
                m = A[i] * a[i];
                a[i + 1] = matrix.inverse(C[i] - m) * B[i];
            }

            b[1] = matrix.inverse(C[0]) * v[0];
            for (int i = 1; i <= N - 1; i++)
            {
                m = A[i] * a[i];
                w = new vectorb(v[i], blockN);
                w -= A[i] * b[i];
                b[i + 1] = matrix.inverse(C[i] - m) * w;
            }
            for (int i = 0; i < blockN; i++)
                res[N - 1][i] = b[N][0][i];
            for (int i = N - 2; i >= 0; i--)
            {
                w = b[i + 1];
                w -= a[i + 1] * res[i + 1];
                for (int j = 0; j < blockN; j++)
                {
                    res[i][j] = w[0][j];
                }
            }
            return res;
        }

        public void save(vectorb ans, vectorb right)
        {
            string target = @"c:\Lab5_Pyatov";
            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }
            StreamWriter stream = null;
            try
            {
                stream = new StreamWriter(@"c:\Lab5_Pyatov\C#_test.txt", true);
                string s = this.ToString() + "Right side:\n" + right.ToString() + "Answer:\n" + ans.ToString();
                stream.WriteLine(s);
            }
            catch
            {
                Console.WriteLine("Save Error");
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }
    }
}

