using System;
using System.IO;
using System.Linq;
using HtmlAgilityPack;

namespace ScrapeHtmlAgilityPack
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load("http://www.c-sharpcorner.com");
            HtmlNode[] nodes = document.DocumentNode.SelectNodes("//a[contains(@class, 'title')]").ToArray();
            var csvFile = new StreamWriter("C:/Users/karol/OneDrive/Desktop/output/Sample.csv");
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
