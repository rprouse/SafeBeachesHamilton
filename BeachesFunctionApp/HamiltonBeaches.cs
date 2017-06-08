using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BeachesFunctionApp
{
    public class HamiltonBeaches
    {
        const string WATER_QUALITY_URI = "https://www.hamilton.ca/parks-recreation/parks-trails-and-beaches/beach-water-quality-in-hamilton";

        public static async Task<IEnumerable<Reading>> ParseBeachQualityPage()
        {
            var readings = new List<Reading>();

            HtmlDocument doc = await GetHtmlDocument();

            List<HtmlNode> cells = GetWaterQualityDivs(doc);

            for (int i = 0; i <= 14 && i < cells.Count - 1; i += 2)
            {
                var reading = ParseReading(cells[i], cells[i + 1]);
                if (reading != null) readings.Add(reading);
            }
            return readings;
        }

        private static async Task<HtmlDocument> GetHtmlDocument()
        {
            var client = new HttpClient();

            string html = await client.GetStringAsync(WATER_QUALITY_URI);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc;
        }

        private static List<HtmlNode> GetWaterQualityDivs(HtmlDocument doc) =>
            doc.DocumentNode
               .Descendants("div")
               .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.StartsWith("coh-column half"))
               .ToList();

        private static Reading ParseReading(HtmlNode leftDiv, HtmlNode rightDiv)
        {
            try
            {
                Beach beach = ParseLeftDiv(leftDiv);
                if (beach == null)
                    return null;

                return ParseRightDiv(rightDiv, beach);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static Beach ParseLeftDiv(HtmlNode leftDiv)
        {
            var name = leftDiv.Descendants("strong").FirstOrDefault()?.InnerText.Trim();
            var beach = Beach.Beaches.FirstOrDefault(b => b.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return beach;
        }

        private static Reading ParseRightDiv(HtmlNode rightDiv, Beach beach)
        {
            var reading = new Reading { Beach = beach, DateAdded = DateTime.Now.Date };


            var img = rightDiv.Descendants("img")?.FirstOrDefault()?.GetAttributeValue("alt", "");

            reading.Open = img == null ? OpenStatus.Untested :
                                         img.StartsWith("swim", StringComparison.OrdinalIgnoreCase) ?
                                         OpenStatus.Open : OpenStatus.Closed;



            var info = rightDiv.Descendants("p")
                            ?.FirstOrDefault()
                            ?.ChildNodes
                             .Where(n => n.Name == "#text")
                             .Select(n => n.InnerText.Trim())
                             .ToArray();

            if (info != null && info.Length == 2)
            {
                if (info[0].StartsWith("Tested: "))
                {
                    var dateStr = info[0].Split(new[] { ':' })[1].Trim();
                    if (DateTime.TryParse(dateStr, out DateTime result))
                    {
                        reading.DateTested = result.Date;
                    }
                }
                if (info[1].StartsWith("Water temperature: "))
                {
                    var tempStr = info[1].Split(new[] { ':' })[1].Trim().Trim(new[] { '°', 'c', 'C' });
                    if (int.TryParse(tempStr, out int result))
                    {
                        reading.Temperature = result;
                    }
                }
            }
            reading.Message = rightDiv.Descendants("strong").FirstOrDefault()?.InnerText.Trim();

            if (info != null && info.Length != 2)
            {
                foreach (var text in info)
                {
                    reading.Message += $" {text}";
                }
            }
            return reading;
        }
    }
}
