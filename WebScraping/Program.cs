using WebScraping.FeaturesInTheConsole;
using System;
using System.Collections.Generic;

namespace FeaturesInTheConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var classe = new InvokeClass(new Dictionary<string, Action>() {
                {"First test: WikipediaTest", WikipediaTest.Execute},
                {"Second test: VagalumeTest", VagalumeTest.Execute},
            });

            classe.SelectAndExecute();
        }
    }
}
