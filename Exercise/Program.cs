using System;

namespace Exercise
{
    public sealed class SingletonClass
    {
        public static Lazy<SingletonClass> lazyInstance = new Lazy<SingletonClass>(() => new SingletonClass());
        public static SingletonClass Instance => lazyInstance.Value;

        public string Identity;

        private SingletonClass()
        {
            Identity = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return $"The identity is {Identity}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SingletonTester.IsSingleton(() => { return SingletonClass.Instance; }));
        }
    }

    public class SingletonTester
    {
        public static bool IsSingleton(Func<object> func)
        {

            var instance1 = func.Invoke();
            var instance2 = func.Invoke();

            Console.WriteLine(instance1.ToString());
            Console.WriteLine(instance2.ToString());

            return instance1.Equals(instance2);

        }
    }
}
