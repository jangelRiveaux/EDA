using System;
using EDA;
using System.IO;

namespace Trafego
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SistemaDeTrafago!");

            SistemaDeTrafego s = new SistemaDeTrafego();

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "exit")
                {
                    return;
                }
                if (!File.Exists(input))
                    Console.WriteLine("o diretorio indicado nao existe");
                else
                {
                    s.ReadFile(input);
                    s.ConsoleWrite();
                }
            }
        }
    }
}
