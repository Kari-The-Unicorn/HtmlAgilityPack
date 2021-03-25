using System;
using System.IO;
using System.Linq;
using HtmlAgilityPack;

namespace ScrapeHtmlAgilityPack
{
    public class Program
    {
        public static string URL = "http://www.c-sharpcorner.com";

        static void Main(string[] args)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(URL);
            HtmlNode[] nodes = document.DocumentNode.SelectNodes("//a[contains(@class, 'title')]").ToArray();
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "output/Sample.csv";
            var csvFile = new StreamWriter(path);
            csvFile.Write("column" + ",");
            foreach (HtmlNode item in nodes)
            {
                csvFile.Write(Environment.NewLine);
                var data = item.InnerText;
                csvFile.Write(data.Trim() + ",");
            }
        }
    }
}
