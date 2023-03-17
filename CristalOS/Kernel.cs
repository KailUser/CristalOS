using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace CristalOS
{
    public class Kernel : Sys.Kernel
    {

        protected override void BeforeRun()
        {
            Console.Clear();
            Console.WriteLine("Cristal booted successfully.");
        }

        protected override void Run()
        {
            Console.Write("CristalOS >> ");
            string option = Console.ReadLine();
        }
    }
}
