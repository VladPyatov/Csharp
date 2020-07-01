using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Test
    {
        private DateTime date;

        public DateTime DT
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        public string D_M
        {
            get
            {
                return "Date:" + date.Day + ' ' + date.Month;
            }
        }

        public SubjectSet sub { get; set; }

        public bool zdan { get; set; }

        public Test(SubjectSet s = 0, DateTime d = new DateTime(), bool b = true)
        {
            this.date = d;
            this.sub = s;
            this.zdan = b;
        }

        public override string ToString()
        {
            return date.ToShortDateString() + ' ' + sub + ' ' + zdan;
        }




    }
}
