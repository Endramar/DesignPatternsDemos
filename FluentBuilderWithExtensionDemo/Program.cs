using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentBuilderWithExtensionDemo
{
    /// <summary>
    /// A simple employee model
    /// </summary>
    public class Employee
    {
        public string Name;
        public string Position;

        public override string ToString()
        {
            return $"{nameof(Name)} is {Name} and {nameof(Position)} is {Position}";
        }
    }

    /// <summary>
    /// We are creating the same logic with fluent inheritance with extension methods. 
    /// </summary>
    public class EmployeeBuilder
    {
        public List<Action<Employee>> Actions = new List<Action<Employee>>();

        public EmployeeBuilder Called(string name)
        {
            Actions.Add(e => { e.Name = name; });

            return this;
        }
    }

    /// <summary>
    /// Below is the extension method to create WorksAsA method without altering the EmployeeBuilder class itself (The Open-Closed princible)
    /// </summary>
    public static  class EmployeeBuilderExtensions
    {
        public static EmployeeBuilder WorksAsA(this EmployeeBuilder employeeBuilder, string position)
        {
            employeeBuilder.Actions.Add(e => { e.Position = position; });
            return employeeBuilder;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var eb = new EmployeeBuilder();

            var employee = eb.Called("Ali").WorksAsA("Engineer");

            Console.WriteLine(employee);
            Console.ReadLine();
        }
    }
}
