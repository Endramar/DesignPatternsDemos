using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VectorRasterDemo
{
    public class Point
    {
        public int X, Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

    }


    public class Line
    {
        public Point Start, End;

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }
    }

    /// <summary>
    /// A collection of lines
    /// </summary>
    public class VectorObject : Collection<Line>
    {

    }

    /// <summary>
    /// A sample shape that extends the vector object
    /// </summary>
    public class Rectangle : VectorObject
    {
        public Rectangle(int x, int y, int width, int height)
        {
            Add(new Line(new Point(x, y), new Point(x + width, y)));
            Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
            Add(new Line(new Point(x + width, y + height), new Point(x, y + height)));
            Add(new Line(new Point(x, y + height), new Point(x, y)));

        }
    }

    /// <summary>
    /// An adapter that adapts line to a collection of point to be able to draw a line via point class
    /// </summary>
    public class LineToPointAdapter : Collection<Point>
    {
        /// <summary>
        /// counts for calling the adapter
        /// </summary>
        private static int count;

        public LineToPointAdapter(Line line)
        {
            Console.WriteLine($"{ ++ count} : Generating points for [{line.Start.X},{line.Start.Y}] , [{line.End.X},{line.End.Y}]");

            int left = Math.Min(line.Start.X, line.End.X);
            int right = Math.Max(line.Start.X, line.End.X);
            int top = Math.Min(line.Start.Y, line.End.Y);
            int bottom = Math.Max(line.Start.Y, line.End.Y);
            int dx = right - left;
            int dy = line.End.Y - line.Start.Y;

            if (dx == 0)
            {
                for (int y = top; y <= bottom; ++y)
                {
                    Add(new Point(left, y));
                }
            }
            else if (dy == 0)
            {
                for (int x = left; x <= right; ++x)
                {
                    Add(new Point(x, top));
                }
            }

        }
    }

    class Program
    {
        private static readonly List<VectorObject> vectorObjects = new List<VectorObject>
        {
            new Rectangle(1,1,10,10),
            new Rectangle(3,3,6,6)
        };

        static void DrawPoint(Point p)
        {
            Console.Write(".");
        }

        static void Main(string[] args)
        {
            Draw();
            Draw();
        }

        static void Draw()
        {
            foreach (var vectorObject in vectorObjects)
            {
                foreach (var line in vectorObject)
                {
                    var lineToPointAdapter = new LineToPointAdapter(line);
                    foreach (var point in lineToPointAdapter)
                    {
                        DrawPoint(point);
                    }
                }
            }
        }
    }
}
