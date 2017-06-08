using System;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace BeachesFunctionApp
{
    public static class ParseHamiltonBeaches
    {
        const string UPDATE = "update Reading with (serializable) set [BeachId]=@BeachId, [Open]=@Open, [DateTested]=@DateTested, [DateAdded]=@DateAdded, [Temperature]=@Temperature, [Message]=@Message where [BeachId]=@BeachId and [DateAdded]=@DateAdded";
        const string INSERT = "insert into Reading ([BeachId], [Open], [DateTested], [DateAdded], [Temperature], [Message]) values (@BeachId, @Open, @DateTested, @DateAdded, @Temperature, @Message)";

        [FunctionName("TimerTriggerCSharp")]
        public static void Run([TimerTrigger("0 0 */4 * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"Parsing Hamilton Beaches at: {DateTime.Now}");

            var readings = HamiltonBeaches.ParseBeachQualityPage().Result;
            try
            {
                var conn = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                using (var connection = new SqlConnection(conn))
                {
                    connection.Open();

                    foreach (var reading in readings)
                    {
                        log.Info($"{reading.Beach.Name} is {reading.Open}");

                        var parms = new { BeachId = reading.Beach.Id, Open = (int)reading.Open, DateTested = reading.DateTested, DateAdded = reading.DateAdded, Temperature = reading.Temperature, Message = reading.Message };
                        var rows = connection.Execute(UPDATE, parms);
                        if (rows == 0)
                        {
                            rows = connection.Execute(INSERT, parms);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                log.Error(e.ToString());
                throw;
            }
        }
    }
}