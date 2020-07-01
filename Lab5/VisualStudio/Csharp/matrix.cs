using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpTest
{
    public class matrix
    {
        private int N;
        public int GetN { get => this.N; }

        private double[,] mat;

        public void Function(Action<int, int> f)
        {
            for (var i = 0; i < N; i++)
                for (var j = 0; j < N; j++)
                    f(i, j);
        }
        public matrix()
        {
            N = 0;
            mat = null;
        }

        public matrix(int ord)
        {
            N = ord;
            mat = new double[N, N];
            this.Function((i, j) => mat[i, j] = 0);
        }

        public matrix(int k, int ord)
        {
            N = ord;
            mat = new double[N, N];
            this.Function((i, j) => mat[i, j] = i == j ? k : 0);
        }
        public matrix(double[] d, int ord)
        {
            N = ord;
            mat = new double[N, N];
            this.Function((i, j) => mat[i, j] = i == j ? d[i] : 0);
        }

        public double this[int x, int y]
        {
            get
            {
                return this.mat[x, y];
            }
            set
            {
                this.mat[x, y] = value;
            }
        }


        public static matrix operator +(matrix one, matrix two)
        {
            if (one.N != two.N)
                throw new ArgumentException("matrixes have different dimensions");
            var res = new matrix(one.N);
            res.Function((i, j) => res[i, j] = one[i, j] + two[i, j]);
            return res;
        }

        public static matrix operator -(matrix one, matrix two)
        {
            if (one.N != two.N)
                throw new ArgumentException("matrixes have different dimensions");
            var res = new matrix(one.N);
            res.Function((i, j) => res[i, j] = one[i, j] - two[i, j]);
            return res;
        }

        public static matrix operator *(matrix one, matrix two)
        {
            if (one.N != two.N)
                throw new ArgumentException("matrixes have different dimensions");
            var res = new matrix(one.N);
            res.Function((i, j) =>
            {
                for (var k = 0; k < one.N; k++)
                    res[i, j] += one[i, k] * two[k, j];
            });
            return res;
        }

        public static vectorb operator *(matrix one, vectorb v)
        {
            if (one.N != v.GetBlockN || v.GetN > 1)
                throw new ArgumentException("different dimensions");
            vectorb res = new vectorb(1, v.GetBlockN);
            for (var i = 0; i < v.GetBlockN; i++)
                for (var j = 0; j < v.GetBlockN; j++)
                    res[0][j] += one[i, j] * v[0][j];

            return res;
        }
        public static vectorb operator *(matrix one, double[] d)
        {
            vectorb res = new vectorb(1, one.N);
            for (var i = 0; i < one.N; i++)
                for (var j = 0; j < one.N; j++)
                    res[0][j] += one[i, j] * d[j];

            return res;
        }
        public override string ToString()
        {
            string s = "";
            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < N; j++)
                    s += mat[i, j] + " ";
                s += "\n";
            }
            return s;
        }

        public static matrix inverse(matrix m)
        {
            matrix M = new matrix(m.GetN);
            for (int i = 0; i < m.GetN; i++)
                M[i, i] = 1 / m[i, i];
            return M;
        }

    }
}
