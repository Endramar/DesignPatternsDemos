using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CopyConstructorDemo
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// This is my serialization copy by json serialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(this T self)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(self));
        }


        /// <summary>
        /// Xlm serializion has a restriction that every class must have a parametreless constructor!!!!.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T DeepCopyByXml<T>(this T self)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var ms = new MemoryStream())
            {
                serializer.Serialize(ms,self);
                ms.Position = 0;
                return (T)serializer.Deserialize(ms);
            }

        }
    }
 
    public class Person 
    {
        public string [] Names;
        public Address Address;

        /// <summary>
        /// A constructor for xlm serialization
        /// </summary>
        public Person()
        {

        }

        public Person(string [] names, Address address)
        {
            Names = names ?? throw new ArgumentNullException(nameof(names));
            Address = address ?? throw new ArgumentNullException(nameof(address));
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


        /// <summary>
        /// A constructor for xlm serialization
        /// </summary>
        public Address()
        {

        }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
            HouseNumber = houseNumber;
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
                 new[] { "John", "Smith" },
                 new Address
                (
                    "Winchester Road",
                    123
                )
            );

            // a deep copy of john for jane. John is the prototype 
            //var jane = john.DeepCopy();

            var jane = john.DeepCopyByXml();

            jane.Names[0] = "Jane";
            jane.Address.HouseNumber = 321;

            Console.WriteLine(john);
            Console.WriteLine(jane);

            Console.ReadLine();
        }
    }
}
