using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParseWeb
{
    public enum Status
    {
        Untested,
        Closed,
        Open
    }

    public class HamiltonBeaches
    {
        const string WATER_QUALITY_URI = "https://www.hamilton.ca/parks-recreation/parks-trails-and-beaches/beach-water-quality-in-hamilton";

        HttpClient _client;
        HttpClientHandler _handler;

        public async Task ParseBeachQualityPage()
        {
            _handler = new HttpClientHandler();
            _client = new HttpClient(_handler);


            string html = await _client.GetStringAsync(WATER_QUALITY_URI);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var cells = doc.DocumentNode
                           .Descendants("div")
                           .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.StartsWith("coh-column half"))
                           .ToList();

            for(int i = 0; i <= 14 && i < cells.Count - 1; i += 2)
            {
                try
                {
                    var first = cells[i];
                    var second = cells[i + 1];

                    var name = first.Descendants("strong").FirstOrDefault()?.InnerText.Trim();
                    var statusMsg = second.Descendants("strong").FirstOrDefault()?.InnerText.Trim();
                    var img = second.Descendants("img")?.FirstOrDefault()?.GetAttributeValue("alt", "");

                    Status status = img == null ? Status.Untested :
                                                  img.StartsWith("swim", StringComparison.OrdinalIgnoreCase) ?
                                                  Status.Open : Status.Closed;

                    var info = second.Descendants("p")
                                    ?.FirstOrDefault()
                                    ?.ChildNodes
                                     .Where(n => n.Name == "#text")
                                     .Select(n => n.InnerText.Trim())
                                     .ToArray();

                    Console.WriteLine($"{name}: {status}");

                    if (info != null && info.Length == 2)
                    {
                        if(info[0].StartsWith("Tested: "))
                        {
                            var dateStr = info[0].Split(new[] { ':' })[1].Trim();
                            if (DateTime.TryParse(dateStr, out DateTime result))
                            {
                                Console.WriteLine($"  Date: {result.ToString("d")}");
                            }
                        }
                        if(info[1].StartsWith("Water temperature: "))
                        {
                            var tempStr = info[1].Split(new[] { ':' })[1].Trim().Trim(new[] { '°', 'c', 'C' });
                            if (int.TryParse(tempStr, out int result))
                            {
                                Console.WriteLine($"  Temp: {result}");
                            }
                        }
                    }
                    Console.WriteLine();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
