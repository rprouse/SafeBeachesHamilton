using System;

namespace ParseWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new HamiltonBeaches();
            parser.ParseBeachQualityPage().Wait();

            ConfirmExit();
            Console.ResetColor();
        }

        private static void ConfirmExit()
        {
#if DEBUG
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("*** Press ENTER to Exit ***");
            Console.ReadLine();
#endif
        }
    }
}