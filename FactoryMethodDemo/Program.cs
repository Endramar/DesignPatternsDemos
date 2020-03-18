using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodDemo
{
    public enum CoordinateType
    {
        Cartesian, Polar
    }

    /// <summary>
    /// This is simple point implementation without factory method
    /// We have two types of coordinates : one is cartesian and other is polar
    /// Therefore we have to use an enum in order to distinguish them. and that is a problem
    /// </summary>
    public class WrongPoint
    {
        private readonly double x;
        private readonly double y;

        public WrongPoint(double a, double b,CoordinateType coordinateType = CoordinateType.Cartesian)
        {
            switch (coordinateType)
            {
                case CoordinateType.Cartesian:
                    x = a;
                    y = b;
                    break;
                case CoordinateType.Polar:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(coordinateType));
            }
        }
    }


    public class Point
    {
        private readonly double x;
        private readonly double y;

        // The constructor is private and not accessible from outer classes
        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

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


        public override string ToString()
        {
            return $"Point is ({x},{y})";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var point = Point.NewPolarPoint(1, 0.5);

            Console.WriteLine(point);
            Console.ReadLine();
        }
    }
}
