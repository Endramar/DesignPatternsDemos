using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityDemo
{
    /// <summary>
    /// This is a class responsible for only Journal related business, SaveToFile method for persistency was not added to this class 
    /// and a new Persistence class was created to prevent the code from comprimising the Single Responsibility Princible.
    /// </summary>
    public class Journal
    {
        private readonly List<string> Entries = new List<string>();

        private static int Count = 0;

        public int AddEntry(string text)
        {
            Entries.Add($"{++Count} : {text}");

            return Count;
        }

        public void RemoveEntry(int index)
        {
            Entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Entries);
        }
    }

    public class Persistence
    {
        public void SaveToFile(Journal j, string fileName, bool overwrite = false)
        {
            if (overwrite || !File.Exists(fileName))
            {
                File.WriteAllText(fileName, j.ToString());
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Journal j = new Journal();
            j.AddEntry("I cried today.");
            j.AddEntry("I laughed today.");

            Console.WriteLine(j);
            Console.ReadLine();

            Persistence p = new Persistence();
            var fileName = @"c:\tempfiles\journal.txt";
            p.SaveToFile(j, fileName);

            Process.Start(fileName);
        }
    }
}
