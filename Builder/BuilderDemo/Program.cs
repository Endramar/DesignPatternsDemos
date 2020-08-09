using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Perpose of the demo project is to create a HtmlBuilder to build html strings easily.
/// </summary>
namespace BuilderDemo
{

    public class HtmlElement
    {
        public string Text;
        public string Name;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int IndentSize = 2;

        private readonly string thisIsAConflickString;

        public HtmlElement()
        {

        }

        public HtmlElement(string text, string name)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var indentString = new string(' ', indent * IndentSize);
            sb.AppendLine($"{indentString}<{Name}>");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', IndentSize * (indent + 1)));
                sb.AppendLine(Text);
            }

            foreach (var htmlElement in Elements)
            {
                sb.Append(htmlElement.ToStringImpl(indent + 1));
            }

            sb.AppendLine($"{indentString}</{Name}>");

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }

    }

    public class HtmlBuilder
    {
        private readonly HtmlElement rootElement = new HtmlElement();
        private readonly string rootName;

        public HtmlBuilder(string rootName)
        {
            this.rootName = rootName;
            this.rootElement.Name = rootName;
        }

        public HtmlBuilder AddChild(string name, string text)
        {
            var childElement = new HtmlElement(text, name);
            rootElement.Elements.Add(childElement);

            /// Return the reference of the builder itself so that we can call AddChild method as many times as needed without usage of the instance
            return this;
        }

        public override string ToString()
        {
            return rootElement.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample html string by String Builder
            var listWords = new string[] { "First Element", "Second Element", " Third Element" };
            var sb = new StringBuilder();
            sb.Append("<ul>");
            foreach (var word in listWords)
            {
                sb.AppendFormat("<li>{0}</li>", word);
            }
            sb.Append("</ul>");
            Console.WriteLine(sb.ToString());

            // below code is the same one with custom HtmlBuilder

            var htmlBuilder = new HtmlBuilder("ul");

            htmlBuilder.AddChild("li", "first test word");
            htmlBuilder.AddChild("li", "second test word");

            Console.WriteLine(htmlBuilder);

            // in order to make the builder use fluent interface we need to return its reference from AddChild method
            // By this way we can call the AddChild method like below


            htmlBuilder = new HtmlBuilder("ul")
           .AddChild("li", "first test word")
           .AddChild("li", "second test word")
           .AddChild("li", "third test word");

            Console.WriteLine(htmlBuilder);

            Console.ReadLine();

        }
    }
}
