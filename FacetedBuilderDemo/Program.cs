using Newtonsoft.Json;
using System;

namespace FacetedBuilderDemo
{
    public class Employee
    {
        //Address
        public string StreetAddress, PostCode, City;

        //employement
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    /// <summary>
    /// This is the facade class 
    /// </summary>
    public class EmployeeBuilder
    {
        protected Employee employee = new Employee();

        // This is a facade for the job builder so that we can access to job builder methods through the employee builder
        public EmployeeJobBuilder Works => new EmployeeJobBuilder(employee);

        // This is a facade for address builder.
        public EmployeeAddressBuilder Lives { get { return new EmployeeAddressBuilder(employee); } }


        // implicit operator allows implicit type conversion from EmployeeBuilder to Employee.
        public static implicit operator Employee(EmployeeBuilder employeeBuilder)
        {
            return employeeBuilder.employee;
        }
    }

    public class EmployeeJobBuilder : EmployeeBuilder
    {
        public EmployeeJobBuilder(Employee employee)
        {
            this.employee = employee;
        }

        public EmployeeJobBuilder At(string companyName)
        {
            employee.CompanyName = companyName;
            return this;
        }

        public EmployeeJobBuilder AsA(string position)
        {
            employee.Position = position;
            return this;
        }

        public EmployeeJobBuilder Earns(int income)
        {
            employee.AnnualIncome = income;
            return this;
        }
    }

    public class EmployeeAddressBuilder : EmployeeBuilder
    {
        public EmployeeAddressBuilder(Employee employee)
        {
            this.employee = employee;
        }

        public EmployeeAddressBuilder LivesAt(string streetAddress)
        {
            employee.StreetAddress = streetAddress;
            return this;
        }

        public EmployeeAddressBuilder WithPostCode(string postCode)
        {
            employee.PostCode = postCode;
            return this;
        }

        public EmployeeAddressBuilder In(string city)
        {
            employee.City = city;
            return this;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var employeeBuilder = new EmployeeBuilder();

            // Example of the work facade is as follows:

            Employee employee = employeeBuilder.Works.At("Snacktech").AsA("Software Developer").Earns(120000);

            Console.WriteLine(employee);

            Console.ReadLine();

        }
    }
}
