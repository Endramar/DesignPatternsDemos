using Newtonsoft.Json;
using System;

namespace Monostate
{
    /// <summary>
    /// We need 1 CEO at a time. Will do that by monostate pattern
    /// </summary>
    public class CEO
    {
        // static values in a non-static class
        private static string name;
        private static int age;

        /// <summary>
        /// Non static properties to reach static ones.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ceo1 = new CEO();
            ceo1.Name = "Ali";
            ceo1.Age = 33;

            var ceo2 = new CEO(); // Always getting the same properties no matter how many instances we create. 

            Console.WriteLine(ceo2);
        }
    }
}
