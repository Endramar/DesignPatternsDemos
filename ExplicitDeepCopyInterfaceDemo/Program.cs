using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyConstructorDemo
{
    /// <summary>
    /// A protptype interface for deepcopy .. // better than IClonable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPrototype<T>
    {
        T DeepCopy();
    }

    public class Person : IPrototype<Person>
    {
        public string Name;
        public Address Address;

        public Person(string name, Address address)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public Person DeepCopy()
        {
            return new Person(Name, Address.DeepCopy());
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    public class Address : IPrototype<Address>
    {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
            HouseNumber = houseNumber;
        }

        public Address DeepCopy()
        {
            return new Address(StreetName, HouseNumber);
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
                 "John",
                 new Address
                (
                    "Winchester Road",
                    123
                )
            );

            // a deep copy of john for jane. John is the prototype 
            var jane = john.DeepCopy();
            jane.Name = "Jane";
            jane.Address.HouseNumber = 321;

            Console.WriteLine(john);
            Console.WriteLine(jane);

            Console.ReadLine();
        }
    }
}
