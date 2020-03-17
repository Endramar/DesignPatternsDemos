using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentBuilderWithInheritanceDemo
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

        public class Builder : EmployeeJobBuilder<Builder>
        {

        }

        public static Builder New => new Builder();
       
    }

    /// <summary>
    /// A simple fluent employee info builder which sets the name of the emloyee and build a new one
    /// </summary>
    public class WrongEmployeeInfoBuilder
    {
        public Employee Employee = new Employee();

        public WrongEmployeeInfoBuilder Called(string name)
        {
            Employee.Name = name;
            return this;
        }
    }

    /// <summary>
    /// Lets create a job builder which inherits from info builder and wants to use all functionalities of both job and info builder
    /// </summary>
    public class WrongEmployeeJobBuilder : WrongEmployeeInfoBuilder
    {
        public WrongEmployeeJobBuilder WorkAs(string positionName)
        {
            Employee.Position = positionName;
            return this;
        }
    }


    /// <summary>
    /// Lets create the correct generic version of the builders so we can do what we want.
    /// </summary>

    public abstract class EmployeeBuilder
    {
        public Employee Employee = new Employee();

        public Employee Build()
        {
            return Employee;
        }
    }

    /// <summary>
    /// create the builders with generic recursive approach. Restrict the T type with the builder itself
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EmployeeInfoBuilder<T> : EmployeeBuilder where T : EmployeeInfoBuilder<T>
    {
        public T Called(string name)
        {
            Employee.Name = name;
            return (T)this;
        }
    }

    public class EmployeeJobBuilder<T> : EmployeeInfoBuilder<EmployeeJobBuilder<T>> where T : EmployeeJobBuilder<T>
    {
        public T WorkedAs(string position)
        {
            Employee.Position = position;
            return (T)this;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            // we can seperately call the builders with the wrong approach like below examples.
            new WrongEmployeeInfoBuilder().Called("Ali");
            new WrongEmployeeJobBuilder().WorkAs("Software Engineer");

            // But we cannot create a single job builder which creates both info and job information, Therefore the code below won't work
            var builder = new WrongEmployeeJobBuilder();
            builder.Called("Ali"); //.WorkedAs(); There are no WorkedAs method because the Called() method returns EmployeeInfoBuilder and that builder does not know the method inside EmployeeJobBuilder

            /// We created an inherited builder pattern with both info and job builders.
            var employee = Employee.New.Called("Ali").WorkedAs("Engineer").Build();

            Console.WriteLine(employee);
            Console.ReadLine();
        }
    }
}
