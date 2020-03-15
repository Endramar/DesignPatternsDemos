using System;
using System.Collections.Generic;

namespace OpenClosedDemo
{
    public enum Color
    {
        Red, Blue, Green
    }

    public enum Size
    {
        Small, Medium, Large
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            Name = name;
            Color = color;
            Size = size;
        }
    }

    /// <summary>
    /// The product filter class has been modified for all new filtering methods. 
    /// Instead It must be open for extension but close for modification. This ProductFilter breaks the Open-Closed Princible. 
    /// </summary>
    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
            {
                if (p.Size == size)
                {
                    yield return p;
                }
            }
        }

        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
            {
                if (p.Color == color)
                {
                    yield return p;
                }
            }
        }
    }



    /// <summary>
    /// These iterfaces created to satisfy open-closed principle. The correct way !!!!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    /// <summary>
    /// Color specification
    /// </summary>
    public class ColorSpecification : ISpecification<Product>
    {
        private readonly Color Color;

        public ColorSpecification(Color color)
        {
            Color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Color == Color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private readonly Size Size;

        public SizeSpecification(Size size)
        {
            Size = size;
        }

        public bool IsSatisfied(Product p)
        {
            return p.Size == Size;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first ?? throw new ArgumentNullException(nameof(first));
            this.second = second ?? throw new ArgumentNullException(nameof(second));
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var p in items)
            {
                if (spec.IsSatisfied(p))
                {
                    yield return p;
                }
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };

            var pf = new ProductFilter();
            Console.WriteLine("Green Products (OLD) : ");

            foreach (var p in pf.FilterByColor(products, Color.Green))
            {
                Console.WriteLine($"-- {p.Name} is a green product");
            }


            // Below is the correct way to do it..
            // We do not modify the better filter we just add new classes to create new filters.
            var bf = new BetterFilter();
            Console.WriteLine("Green Products (NEW) : ");
            foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($"-- {p.Name} is a green product");
            }

            Console.WriteLine("Large Products Are : ");
            foreach (var p in bf.Filter(products, new SizeSpecification(Size.Large)))
            {
                Console.WriteLine($"-- {p.Name} is a large product");
            }

            // In order to filter by both size and color the code below is enough
            Console.WriteLine("Large And Green Products Are : ");
            foreach (var p in bf.Filter(products, new AndSpecification<Product>(new ColorSpecification(Color.Blue), new SizeSpecification(Size.Large))))
            {
                Console.WriteLine($"-- {p.Name} is a large and blue product");
            }

            Console.ReadLine();
        }
    }
}
