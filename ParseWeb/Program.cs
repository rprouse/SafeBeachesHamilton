using System;

namespace ParseWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            var readings = HamiltonBeaches.ParseBeachQualityPage().Result;

            foreach(var reading in readings)
            {
                switch (reading.Open)
                {
                    case OpenStatus.Untested:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(reading.Beach.Name);
                        Console.WriteLine($"\t{reading.Message}");
                        break;
                    case OpenStatus.Closed:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(reading.Beach.Name);
                        Console.WriteLine($"\t{reading.Message}");
                        break;
                    case OpenStatus.Open:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine(reading.Beach.Name);
                        Console.WriteLine($"\tTested: {reading.DateTested.Value.ToString("d")}");
                        Console.WriteLine($"\tTemperature: {reading.Temperature}");
                        break;
                    default:
                        break;
                }
                Console.WriteLine();
            }

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