using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace EDA
{
    public class SistemaDeTrafego
    {
        HashTable<int, Daily_Fluxo> calendar;

        public SistemaDeTrafego()
        {
            calendar = new HashTable<int, Daily_Fluxo>();
        }

        public void ReadFile(string file)
        {
            FileStream f = File.OpenRead(file);
            StreamReader str = new StreamReader(f);
            string line;
            while ((line = str.ReadLine()) != null)
            {
                if (line != "")
                {
                    string[] words = line.Split(',');
                    if (!calendar.Contain(int.Parse(words[2])))
                    {
                        calendar.Add(int.Parse(words[2]), new Daily_Fluxo());
                    }                    
                    Daily_Fluxo flux = calendar[int.Parse(words[2])];
                    flux.Insert(new Fluxo { Data = words[2], Sector = words[0][0],
                        Rodovia = int.Parse(words[1]), Value = double.Parse(words[3])});
                }
                // ConsoleWrite();
                // Console.WriteLine("????????????????????????????????????????????????");
            }
        }

        public void ConsoleWrite()
        {
            calendar.Reset();
            foreach (var item in calendar)
            {
                double min = item.Item2.Min;
                double max = item.Item2.Max;
                double med = min + (max - min) * 0.8;
                Console.WriteLine(item.Item1 + " (min: " + min + "  med: "+ med + " max: " + max +")");
                Console.WriteLine("\t\tSect\t\tFluxo");
                // item.Item2.
                foreach (var fluxo in item.Item2.Get_Fluxos())
                {
                    Console.WriteLine("\t\t" + fluxo.Item1 + "\t\t" + fluxo.Item2);
                }
                Console.WriteLine("-------------------------------------------");
            }
        }
    }

    public class Daily_Fluxo
    {
        HashTable<char, Node<double, int>> dictionary;
        AVL<double, int> fluxo_sector;

        public Daily_Fluxo()
        {
            dictionary = new HashTable<char, Node<double, int>>();
            fluxo_sector = new AVL<double, int>();
        }

        public void Insert(Fluxo f)
        {
            if (dictionary.Contain(f.Sector))
            {
                Node<double, int> current = dictionary[f.Sector];
                if (f.Value != 0)
                {
                    double fluxo_atual = current.Key + f.Value;
                    current.Delete(f.Sector);
                    fluxo_sector.InsertElement(fluxo_atual, f.Sector);
                    dictionary[f.Sector] = fluxo_sector.Last_Inserted_Node;
                }
                
            }
            else
            {
                fluxo_sector.InsertElement(f.Value, f.Sector);
                dictionary.Add(f.Sector, fluxo_sector.Last_Inserted_Node);
            }
        }

        public IEnumerable<Tuple<char, double>> Get_Fluxos()
        {
            double min = fluxo_sector.MinKey;
            double delta_08 = (fluxo_sector.MaxKey - min) * 0.8 + min;
            foreach (var item in fluxo_sector.GreaterEgualsThan(delta_08))
            {
                yield return new Tuple<char, double>((char)item.Item2,item.Item1);
            }
        }

        public double Min { get { return fluxo_sector.MinKey; } }

        public double Max { get { return fluxo_sector.MaxKey; } }
    }

	public class Fluxo
	{
		public string Data { get; set; }
		public char Sector { get; set; }
		public double Value { get; set; }	
        public int Rodovia { get; set; }
	}
}
