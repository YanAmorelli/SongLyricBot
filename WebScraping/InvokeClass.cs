using System;
using System.Collections.Generic;
using System.Linq;

namespace FeaturesInTheConsole
{
    public class InvokeClass
    {
        Dictionary<string, Action> Classes;

        public InvokeClass(Dictionary<string, Action> classes)
        {
            Classes = classes;
        }

        public void SelectAndExecute()
        {
            int i = 1;

            foreach (var classe in Classes)
            {
                Console.WriteLine("{0}) {1}", i, classe.Key);
                i++;
            }

            Console.Write("Enter the number (or empty for the last class): ");

            int.TryParse(Console.ReadLine(), out int num);
            bool validNum = num > 0 && num <= Classes.Count;
            num = validNum ? num - 1 : Classes.Count - 1;

            string className = Classes.ElementAt(num).Key;

            Console.Write("\nExecuting class");
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(className);
            Console.ResetColor();

            Console.WriteLine(String.Concat(
                Enumerable.Repeat("=", className.Length + 21)) + "\n");

            Action executar = Classes.ElementAt(num).Value;
            try
            {
                executar();
            }
            catch (Exception e)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Ocorreu um erro: {0}", e.Message);
                Console.ResetColor();

                Console.WriteLine(e.StackTrace);
            }
        }
    }
}