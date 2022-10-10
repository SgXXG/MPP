using System;
using System.Linq;
using System.Reflection;


namespace MPP_Lab3_1
{
    static class Program
    {
        static void ListTypesInAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(t => t.IsPublic).OrderBy(t => t.Namespace + t.Name);
            foreach (var type in types)
            {
                Console.WriteLine(type.FullName);
            }
        }

        static void Main(string[] args)
        {
            var assembly = Assembly.LoadFrom("D:\\!Studing\\3 Course\\5 Sem\\СПП\\MPP_Lab1\\MPP_Lab1\\bin\\Debug\\net6.0\\MPP_Lab1_1.dll");
            ListTypesInAssembly(assembly);
            Console.ReadKey();
        }
    }
}