using System;
using System.Data.SqlClient;
using Dapper;

namespace ParseWeb
{
    class Program
    {
        const string UPDATE = "update Reading with (serializable) set [BeachId]=@BeachId, [Open]=@Open, [DateTested]=@DateTested, [DateAdded]=@DateAdded, [Temperature]=@Temperature, [Message]=@Message where [BeachId]=@BeachId and [DateAdded]=@DateAdded";
        const string INSERT = "insert into Reading ([BeachId], [Open], [DateTested], [DateAdded], [Temperature], [Message]) values (@BeachId, @Open, @DateTested, @DateAdded, @Temperature, @Message)";

        static void Main(string[] args)
        {
            var readings = HamiltonBeaches.ParseBeachQualityPage().Result;
            try
            {
                using (SqlConnection connection = new SqlConnection($"Server=tcp:beaches.database.windows.net,1433;Initial Catalog=SafeBeaches;Persist Security Info=False;User ID={args[0]};Password={args[1]};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                {
                    connection.Open();

                    foreach (var reading in readings)
                    {
                        Console.WriteLine($"{reading.Beach.Name} is {reading.Open}");

                        var parms = new { BeachId = reading.Beach.Id, Open = (int)reading.Open, DateTested = reading.DateTested, DateAdded = reading.DateAdded, Temperature = reading.Temperature, Message = reading.Message };
                        var rows = connection.Execute(UPDATE, parms);
                        if (rows == 0)
                        {
                            rows = connection.Execute(INSERT, parms);
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            ConfirmExit();
        }

        private static void ConfirmExit()
        {
#if DEBUG
            Console.WriteLine();
            Console.WriteLine("*** Press ENTER to Exit ***");
            Console.ReadLine();
#endif
        }
    }
}