﻿using System;
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


        private  Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"Point is ({x},{y})";
        }


        /// <summary>
        /// THE FACTORY IS AN INNER CLASSS THEREFORE WE CAN ACCESS THE PRIVATE CONSTRUCTOR
        /// We will move factory methods to this class to imlement a factory class which is responsible for all creations.
        /// </summary>
        public static class Factory
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
    }



    class Program
    {
        static void Main(string[] args)
        {
            var point = Point.Factory.NewPolarPoint(1, 0.5);
 
            Console.WriteLine(point);
            Console.ReadLine();
        }
    }
}
