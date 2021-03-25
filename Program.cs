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

        static void Main(string[] args)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(URL);
            HtmlNode eventsMain = document.DocumentNode.SelectNodes("//div[contains(@class,'Events')]").First();
            HtmlNode[] events = eventsMain.SelectNodes(".//li").ToArray();
            foreach (var evnt in events)
            {
                HtmlNode[] dateTimeDetails = evnt.SelectNodes("//span[contains(@class, 'timeEvent')]").ToArray();
                foreach (var info in dateTimeDetails)
                {
                    string month = info.SelectSingleNode("//span[contains(@class, 'LocalMonth1')]").InnerText;
                    string day = info.SelectSingleNode("//span[contains(@class, 'LocalDay1')]").InnerText;
                    string time = info.SelectSingleNode("//span[contains(@class, 'LocalTime1')]").InnerText;
                    string dateTime = day + " " + month + " " + time;
                    


                }
            }
        }
        private void WriteToCsv(string dateTime, string subject, string registerDetails)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "output/Sample.csv";
            using (var writer = new StreamWriter(path))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.GetCultureInfo("en-GB")))
            {
                var records = new List<Header>();
                records.Add(new Header
                {
                    DateTime = dateTime,
                    Subject = subject,
                    RegisterDetails = registerDetails,
                });
                csvWriter.WriteRecords(records);
            }
        }
    }
}
