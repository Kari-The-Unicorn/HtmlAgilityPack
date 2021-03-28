using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using HtmlAgilityPack;
using ScrapeHtmlAgilityPack.Helpers;

namespace ScrapeHtmlAgilityPack
{
    public class Program
    {
        public static string URL = "http://www.c-sharpcorner.com";

        public static void Main(string[] args)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(URL);
            HtmlNode[] events = document.DocumentNode.SelectNodes("//div[contains(@class,'Events')]//li").ToArray();
            string eventTitle = string.Empty;
            string registrLink = string.Empty;

            foreach (var evnt in events)
            {
                HtmlNode eventsDetails = evnt.SelectSingleNode("//div[contains(@class, 'eventsWrap')]");
                eventTitle = eventsDetails.SelectSingleNode("//a[contains(@class,'title')]").InnerText;
                registrLink = eventsDetails.SelectSingleNode("//a[contains(@class,'rgstr')]")
                    .GetAttributeValue("href", null);
                WriteToCsv(eventTitle, registrLink);
            }
        }

        private static void WriteToCsv(string subject, string registerDetails)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "output/Sample.csv";
            using (var writer = new StreamWriter(path))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.GetCultureInfo("en-GB")))
            {
                var records = new List<Header>();
                records.Add(new Header
                {
                    Subject = subject,
                    RegisterLink = registerDetails,
                });
                csvWriter.WriteRecords(records);
            }
        }
    }
}
