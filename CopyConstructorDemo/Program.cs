using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyConstructorDemo
{
    public class Person
    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            Names = names ?? throw new ArgumentNullException(nameof(names));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        /// <summary>
        /// This is a copy constructor which is used for deep copy. The idea comes from C++;
        /// </summary>
        /// <param name="other"></param>
        public Person(Person other)
        {
            Names = other.Names;
            // Because address is a reference type we must create a copy constructor inside the address classs as well.
            Address = new Address(other.Address);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    public class Address
    {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
            HouseNumber = houseNumber;
        }

        public Address(Address other)
        {
            StreetName = other.StreetName;
            HouseNumber = other.HouseNumber;
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
            var john = new Person
            (
                 new string[] { "John", "Smith" },
                 new Address
                (
                    "Winchester Road",
                    123
                )
            );

            // a deep copy of john for jane. John is the prototype 
            var jane = new Person(john);
            jane.Names[0] = "Jane";
            jane.Address.HouseNumber = 321;

            Console.WriteLine(john);
            Console.WriteLine(jane);

            Console.ReadLine();
        }
    }
}
