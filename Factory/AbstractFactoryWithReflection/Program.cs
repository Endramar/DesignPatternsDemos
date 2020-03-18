using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("You are drinking a cup of tea.");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("You are drinking a cup of coffee.");
        }
    }



    // creating the absraction by using a factory interface
    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    public class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"You preapred {amount} ml coffee");
            return new Coffee();
        }
    }

    public class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"You preapred {amount} ml tea");
            return new Tea();
        }
    }


    /// <summary>
    /// A different implementation from Abstract factory which uses reflection
    /// </summary>
    public class HotDrinkMachine
    {

        private readonly Dictionary<string, IHotDrinkFactory> factories = new Dictionary<string, IHotDrinkFactory>();

        public HotDrinkMachine()
        {

            // create factory instances with available drink names by reflection!!!!
            foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
                {
                    factories.Add(t.Name.Replace("Factory", string.Empty), (IHotDrinkFactory)Activator.CreateInstance(t));
                }
            }

        }

        // call the appropriate factory depends on the drink
        public IHotDrink MakeDrink()
        {
            for (var i = 0; i < factories.Count; i++)
            {
                Console.WriteLine($"{i} : {factories.Keys.ToArray()[i]}");
            }

            Console.WriteLine("Please enter the number you prefer.");
            while (true)
            {
                var inputString = Console.ReadLine();

                if (inputString != null && int.TryParse(inputString, out int result) && result >= 0 && result < factories.Count)
                {
                    Console.WriteLine("Please specify the amount:");
                    var amountString = Console.ReadLine();
                    if (amountString != null && int.TryParse(amountString, out int amount))
                    {
                        return factories.Values.ToArray()[result].Prepare(amount);
                    }
                }

                Console.WriteLine("Incorrect value. Please try again.");


            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();

            var drink = machine.MakeDrink();

            drink.Consume();

            Console.ReadLine();
        }
    }
}
