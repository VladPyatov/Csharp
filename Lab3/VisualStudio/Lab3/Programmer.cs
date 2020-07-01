using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3
{
    class Programmer: Person, IDeepCopy
    {
        public double Exp { get; set; }
        public string Topic { get; set; }

        public Programmer(string N = "Prog_Name", string L = "Prog_Last_Name", DateTime d = new DateTime(), string T = "C#", double e = 0)
        {
            this.Topic = T;
            this.Exp = e;
            this.Name[0] = N;
            this.Name[1] = L;
            this.Date = d;
        }

        public override string ToString()
        {
            return Name[0] + " " + Name[1] + " " + Date.ToShortDateString() + " " + Topic + " " + Exp;
        }
    }
}
