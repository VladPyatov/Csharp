using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    class Team: IDeepCopy
    {
        public string Group { get; set; }
        public List<Person> Topic { get; set; }

        public Team(string g)
        {
            this.Group = g;
            this.Topic = new List<Person>();
        }

        public void AddPerson(params Person[] person)
        {
            int f;
            foreach (Person i in person)
            {
                f = 0;
                foreach (Person j in Topic)
                {
                    if (i == j)
                    {
                        f = 1;
                        break;
                    }
                }
                if(f == 0)
                {
                    Topic.Add(i);
                }
            }
        }

        public void AddDefaults()
        {
            Topic.Add(new Person());
            Topic.Add(new Programmer());
            Topic.Add(new Researcher());
        }

        public bool IsProgrammer(Person ps)
        {
            if (ps is Programmer) return true;
            else return false;
        }

        public override string ToString()
        {
            string str = Group;
            foreach(Person i in Topic)
            {
                str += "; " + i.ToString();
            }
            return str;
        }
        public virtual object DeepCopy()
        {
            Team P = new Team(this.Group);
            foreach (Person pers in this.Topic)
            {
                P.AddPerson(pers);
            }
            return P;

        }

        public IEnumerable<Person> Subset(Predicate<Person> Filter)
        {
            foreach(Person pers in Topic)
            {
                if (Filter(pers))
                    yield return pers;
            }
        }
    }
}
