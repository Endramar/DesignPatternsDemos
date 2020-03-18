using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregationDemo
{
    public class Document
    {

    }

    /// <summary>
    /// An interface for new printer machine which can either print, scan or fax the documents.
    /// But this interface contains too many methods for an old fashioned printer which only capable of printing a document.
    /// Therefore, this interface breaks the INTERFACE SEGREGATION PRINCIBLE
    /// </summary>
    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
    }

    /// <summary>
    /// This printer can do all three jobs
    /// </summary>
    public class MultiFunctionPrinter : IMachine
    {
        public void Fax(Document d)
        {
            //
        }

        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }
    }

    /// <summary>
    /// This printer can only print a document so the other two methods are redundant 
    /// This is a vialotion for the princible
    /// </summary>
    public class OldFashionedPrinter : IMachine
    {
        public void Print(Document d)
        {
            // Thi
        }

        public void Fax(Document d)
        {
            throw new NotImplementedException();
        }


        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Below interfaces created to satisfy the princible
    /// </summary>
    public interface IPrinter
    {
        void Print(Document d);    
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public interface IMultiFunctionMachine : IPrinter, IScanner
    {

    }

    /// <summary>
    /// This class has created by using Decorator Pattern !!!!!
    /// </summary>
    public class NewPrinterWithDecorator : IMultiFunctionMachine
    {
        private readonly IPrinter printer;
        private readonly IScanner scanner;

        public NewPrinterWithDecorator(IPrinter printer, IScanner scanner)
        {
            this.printer = printer ?? throw new ArgumentNullException(nameof(printer));
            this.scanner = scanner ?? throw new ArgumentNullException(nameof(scanner));
        }

        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void Scan(Document d)
        {
            scanner.Scan(d);
        }
    }

    /// <summary>
    /// Without decorator pattern. Direct implementation of the interfaces
    /// </summary>
    public class NewPrinterWithoutDecorator : IScanner, IPrinter /// Or IMultiFunctionMachine
    {
        public void Print(Document d)
        {
           ///
        }

        public void Scan(Document d)
        {
            ///
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
