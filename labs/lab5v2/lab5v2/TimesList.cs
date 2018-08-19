using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace lab5v2
{
    [Serializable]
    class TimesList
    {
        private List<TimeItem> times;
        private const string path = "..\\..\\data\\";

        public TimesList()
        {
            times = new List<TimeItem>();
        }

        public TimesList(string filename)
        {
            times = new List<TimeItem>();
            if (File.Exists(path + filename + ".bin"))
            { 
                Load(filename);
                Console.WriteLine(this);
            }
            else
            {
                Console.WriteLine("File " + filename + ".bin" + " does not exist");
            }
        }

        public void Add(TimeItem time)
        {
            times.Add(time);
        }

        public void Save(string filename)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, times);
                ms.Position = 0;

                using (FileStream fs = new FileStream(path + filename + 
                    ".bin", FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(fs);

                    ms.Close();
                    fs.Close();
                    Console.WriteLine(filename + ".bin was saved");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Load(string filename)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                IFormatter formatter = new BinaryFormatter();

                using (FileStream fs = new FileStream(path + filename +
                    ".bin", FileMode.Open, FileAccess.Read))
                {
                    ms.SetLength(fs.Length);
                    fs.CopyTo(ms);
                    ms.Position = 0;
                    List<TimeItem> times = (List<TimeItem>) formatter.Deserialize(ms);
                    this.times = times;

                    ms.Close();
                    fs.Close();
                    Console.WriteLine(filename + ".bin was loaded");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public override string ToString()
        {
            string result = "\n\t-TimesList-\n" + 
                string.Format("{0,-10}{1,-10}{2,-10}{3,-10}{4,-10}\n",
                "n", "k", "timeCS", "timeCPP", "ratio");
            
            foreach(TimeItem time in times)
            {
                result += time;
            }
            
            return result;
        }
    }
}
