using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiskovSubstitutionDemo
{
    public class Rectangle
    {
        /// <summary>
        /// props are made virtual to be able to override them. By this way the Liskov Substitution princible will be satisfied.
        /// </summary>
        public virtual int Width { get; set; }

        public virtual int Height { get; set; }

        public Rectangle()
        {

        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $@"{ nameof(Width) }  : { Width } , {nameof(Height)} : {Height}";
        }
    }


    public class Square : Rectangle
    {
        public new int Width
        {
            set { base.Width = base.Height = value; }
        }

        public new int Height
        {
            set { base.Height = base.Width = value; }
        }

    }

    public class CorrectSquare : Rectangle
    {
        public override int Width { set { base.Width = base.Height = value; } }

        public override int Height { set { base.Height = base.Width = value; } }
    }

    class Program
    {
        static int Area(Rectangle r) => r.Width * r.Height;

        static void Main(string[] args)
        {
            var rectangle = new Rectangle(2, 3);

            Console.WriteLine($"{rectangle}, Area is {Area(rectangle)}");

            // This square is the wrong one because if we create a square instance by referencing Rectangle class it won't work.
            Square square = new Square();
            square.Width = 4;
            Console.WriteLine($"Square :  {square}, Area is {Area(square)}");

            // Below code does not work
            Rectangle wrongSquare = new Square();
            wrongSquare.Width = 4;
            Console.WriteLine($"Wrong Square :  {wrongSquare}, Area is {Area(wrongSquare)}");


            // Below square is the correct implementation
            Rectangle correctSquare = new CorrectSquare();
            correctSquare.Width = 6;
            Console.WriteLine($"Correct Square :  {correctSquare}, Area is {Area(correctSquare)}");

            Console.ReadLine();
        }
    }
}
