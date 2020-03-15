using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DependencyInversionDemo
{
    public enum Relation
    {
        Parent, Child, Sibling
    }

    public class Person
    {
        public string Name { get; set; }

        public Person(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }

    public class RelationShips
    {
        private readonly List<(Person, Relation, Person)> Relations = new List<(Person, Relation, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            Relations.Add((parent, Relation.Parent, child));
            Relations.Add((child, Relation.Child, parent));
        }

        public List<(Person, Relation, Person)> AllRelations => Relations;
    }


    /// <summary>
    /// a class for searching jobs  --- violates the dependency inversion princible because this class directly access the root element of RelationShips class
    /// Because of this the developer cannot change RelationShips class in the future without modifying the dependent classes. 
    /// For example developer would want to change the data storage method from Tupples to Dictionary or use Database. 
    /// </summary>
    public class Research
    {
        public Research(RelationShips relationShips)
        {
            var johnsChildren = relationShips.AllRelations.Where(x => x.Item1.Name == "John").Select(y => y.Item3);

            WriteLine("This is the wrong way!!!");
            foreach (var c in johnsChildren)
            {
                WriteLine($"John has a child named {c.Name}");
            }
        }
    }


    /// <summary>
    /// Below has the correct implementation
    /// First we create and interface to manage search job
    /// </summary>

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindChildrenOf(Person p);
    }


    public class CorrectRelationShips : IRelationshipBrowser
    {
        private readonly List<(Person, Relation, Person)> Relations = new List<(Person, Relation, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            Relations.Add((parent, Relation.Parent, child));
            Relations.Add((child, Relation.Child, parent));
        }

        public IEnumerable<Person> FindChildrenOf(Person p)
        {
            return Relations.Where(x => x.Item1.Name == p.Name).Select(y => y.Item3);
        }

        /// <summary>
        /// This is a wrong implemention and should be removed.
        /// </summary>
        public List<(Person, Relation, Person)> AllRelations => Relations;
    }


    /// <summary>
    /// This class does not depend on the implementation of the RelationShips class itself. Therefore RelationShips class can be modified as many as needed.
    /// </summary>
    public class CorrectResearch
    {
        public CorrectResearch(IRelationshipBrowser relationshipBrowser)
        {
            var children = relationshipBrowser.FindChildrenOf(new Person("John"));

            WriteLine(" ------- ----- -----");
            WriteLine("This is the correct way!!!");
            foreach (var c in children)
            {
                WriteLine($"John has a child named {c.Name}");
            }
        }
    }


    public class Program
    {
        static void Main(string[] args)
        {
            var relationships = new RelationShips();
            relationships.AddParentAndChild(new Person("John"), new Person("David"));
            relationships.AddParentAndChild(new Person("John"), new Person("Ali"));

            new Research(relationships);


            var correctRelationShips = new CorrectRelationShips();
            correctRelationShips.AddParentAndChild(new Person("John"), new Person("David"));
            correctRelationShips.AddParentAndChild(new Person("John"), new Person("Ali"));

            new CorrectResearch(correctRelationShips);

            ReadLine();
        }
    }
}
