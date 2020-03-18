using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodDemo
{

    public class Point
    {
        private readonly double x;
        private readonly double y;

        
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"Point is ({x},{y})";
        }
    }


    /// <summary>
    /// We will move factory methods to this class to imlement a factory class which is responsible for all creations.
    /// </summary>
    public static class PointFactory
    {
        /// <summary>
        /// A factory method which creates cartesian coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        /// <summary>
        /// A factory method which creates polar coordinates
        /// </summary>
        /// <param name="rho"></param>
        /// <param name="theta"></param>
        /// <returns></returns>
        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Sin(theta), rho * Math.Cos(theta));
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var point = PointFactory.NewPolarPoint(1, 0.5);

            // the public constructor is a problem because anyone can create an object with using directly the constructor as below

            var consPoint = new Point(1, 1); // This can be fixed with INNER FACTORY !!!!!

            Console.WriteLine(point);
            Console.ReadLine();
        }
    }
}
