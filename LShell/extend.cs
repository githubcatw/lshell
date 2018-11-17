using System;  
using System.Text;  
using System.IO;  
  
namespace ExtensionMethod1
{
    public static class XX
    {
        public static void NewMethod(this Directory ob)
        {
            Console.WriteLine("Hello I m extended method");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            System.IO.Directory.NewMethod();
        }
    }
}