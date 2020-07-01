using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpTest
{
    public class vectorb
    {
        private int N, blockN;

        public int GetN { get => this.N; }
        public int GetBlockN { get => this.blockN; }

        private double[][] vec;

        public void Function(Action<int, int> f)
        {
            for (var i = 0; i < N; i++)
                for (var j = 0; j < blockN; j++)
                    f(i, j);
        }

        public vectorb()
        {
            N = 0;
            blockN = 0;
            vec = null;
        }

        public vectorb(int n, int blockn)
        {
            N = n;
            blockN = blockn;
            vec = new double[N][];
            for (int i = 0; i < N; i++)
                vec[i] = new double[blockN];
            this.Function((i, j) => vec[i][j] = 0);

        }
        public vectorb(int n, int blockn, bool test)
        {
            N = n;
            blockN = blockn;
            vec = new double[N][];
            for (int i = 0; i < N; i++)
                vec[i] = new double[blockN];
            this.Function((i, j) => vec[i][j] = j);

        }

        public vectorb(double[] d, int blockn)
        {
            N = 1;
            blockN = blockn;
            vec = new double[N][];
            for (int i = 0; i < N; i++)
                vec[i] = new double[blockN];
            this.Function((i, j) => vec[i][j] = d[j]);
        }
        public vectorb(double[] d, int n, int blockn)
        {
            N = n;
            blockN = blockn;
            vec = new double[N][];
            for (int i = 0; i < N; i++)
                vec[i] = new double[blockN];

            int k = 0;
            for (var i = 0; i < N; i++)
                for (var j = 0; j < blockN; j++)
                    vec[i][j] = d[k++];
        }
        public double[] this[int x]
        {
            get
            {
                return this.vec[x];
            }
            set
            {
                this.vec[x] = value;
            }
        }

        public override string ToString()
        {
            string s = "(";

            for (var i = 0; i < N; ++i)
            {
                s += "[";
                for (var j = 0; j < blockN; ++j)
                {
                    s += vec[i][j];
                    if (j != blockN - 1)
                        s += ',';
                }
                s += "]";
                if (i != N - 1)
                    s += ',';

            }

            s += ")\n";

            return s;
        }

        public static vectorb operator +(vectorb one, vectorb two)
        {
            if (one.N != two.N || one.blockN != two.blockN)
                throw new ArgumentException("matrixes have different dimensions");
            var res = new vectorb(one.N, one.blockN);
            for (var i = 0; i < one.N; i++)
                for (var j = 0; j < one.blockN; j++)
                    res[i][j] = one[i][j] + two[i][j];
            return res;
        }

        public static vectorb operator -(vectorb one, vectorb two)
        {
            if (one.N != two.N || one.blockN != two.blockN)
                throw new ArgumentException("matrixes have different dimensions");
            var res = new vectorb(one.N, one.blockN);
            for (var i = 0; i < one.N; i++)
                for (var j = 0; j < one.blockN; j++)
                    res[i][j] = one[i][j] - two[i][j];
            return res;
        }
    }
}
