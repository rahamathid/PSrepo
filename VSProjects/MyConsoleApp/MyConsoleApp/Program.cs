using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MyConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            myfunction();
            string val = ConfigurationManager.AppSettings["key1"];
            Console.WriteLine("Key1: " +val);
            Console.ReadLine();
        }

        private static void myfunction()
        {
            List<string> strList = new List<string>();
            Console.WriteLine("This is inside myfunction");
        }
    }
}
