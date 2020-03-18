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
    /// This is the main factoy to prepare hot drinks
    /// </summary>
    public class HotDrinkMachine
    {
        public enum AvailableDrink
        {
            Tea, Coffee
        }

        private readonly Dictionary<AvailableDrink, IHotDrinkFactory> factories = new Dictionary<AvailableDrink, IHotDrinkFactory>();

        public HotDrinkMachine()
        {

            // create factory instances with available drink names!!!!
            foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            {
                var factoryType = Type.GetType("AbstractFactory." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory");
                var factory = (IHotDrinkFactory)Activator.CreateInstance(factoryType);
                factories.Add(drink, factory);
            }
        }

        // call the appropriate factory depends on the drink
        public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        {
            return factories[drink].Prepare(amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();

            machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100);
            machine.MakeDrink(HotDrinkMachine.AvailableDrink.Coffee, 150);

            Console.ReadLine();
        }
    }
}
