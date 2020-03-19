using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SingletonImplementationDemo
{
    public interface IDatabase
    {
        int GetPopulationOfCity(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;

        // make the constructor private so that noone can creat an instance
        private SingletonDatabase()
        {
            WriteLine("Initializing the database...");
            capitals = File.ReadAllLines("capitals.txt").Batch(2).ToDictionary(x => x.ElementAt(0).Trim(), x => int.Parse(x.ElementAt(1).Trim()));
        }

        public int GetPopulationOfCity(string name)
        {
            return capitals[name];
        }

        private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase()); // this only works once;
        public static SingletonDatabase Instance => instance.Value;  // this is the singleton property

        //private static SingletonDatabase instance = new SingletonDatabase();
        //public static SingletonDatabase Instance => instance;
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("The prgram is running.");

            var db = SingletonDatabase.Instance;

            var city = "Tokyo";
            var population = db.GetPopulationOfCity(city);

            WriteLine($"{city} has a population of {population}");

            Console.ReadLine();
        }
    }
}
