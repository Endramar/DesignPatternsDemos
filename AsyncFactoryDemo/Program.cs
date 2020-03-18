using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncFactoryDemo
{
    /// <summary>
    /// a dummy class which needs a async factory method. Async creation cannot be done via constructor.
    /// </summary>
    public class Foo
    {
        /// <summary>
        /// make the constructor private so that noone can create an instance by using it.
        /// </summary>
        private Foo()
        {
        }

        private async Task<Foo> InitAsync()
        {
            // a sample code which demonsrates the async job
            await Task.Delay(100);
            // use fluent api to return the object itself
            return this;
        }


        public static async Task<Foo> CreateAsync()
        {
            var foo = new Foo();
            return await foo.InitAsync();
        }

    }

    class Program
    {
        static async Task Main(string[] args)
        {
            var foo = await Foo.CreateAsync();
        }
    }
}
