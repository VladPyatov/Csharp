using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Csharp
{
    [Serializable]
    public class TestTime
    {
        List<string> Log { get; set; }

        //public TestTime()
        //{
        //    Log = new List<string>();
        //}
        public void Add(string record)
        {
            Log.Add(record);
        }

        public static bool Save(string filename, TestTime obj)
        {
            bool b = true;
            FileStream fileStream = null;
            try
            {
                fileStream = File.Create(filename);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                binaryFormatter.Serialize(fileStream, obj);
            }
            catch(Exception ex)
            {   Console.WriteLine("Exception: " + ex.Message);
                b = false;
            }
            finally
            { if (fileStream != null) fileStream.Close(); }

            return b;
        }

        public static bool Load(string filename, ref TestTime obj)
        {
            bool b = true;
            FileStream fileStream = null;
            try
            {
                fileStream = File.OpenRead(filename);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                obj = binaryFormatter.Deserialize(fileStream) as TestTime;
            }
            catch(FileNotFoundException ex)
            {
                obj.Log = new List<string>();
                b = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                b = false;
            }
            finally
            { if (fileStream != null) fileStream.Close(); }

            return b;
        }

        public override string ToString()
        {
            string s = "\n";
            for (var i =0; i < Log.Count; i++)
                s += Log[i] + "\n";
            return s;
        }
    }
}
