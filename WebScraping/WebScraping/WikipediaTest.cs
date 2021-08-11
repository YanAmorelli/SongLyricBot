using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebScraping.FeaturesInTheConsole
{
    public class WikipediaTest
    {
        public static async Task<string> CallUrl(string fullUrl)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            client.DefaultRequestHeaders.Accept.Clear();
            var response = client.GetStringAsync(fullUrl);
            return await response;
        }

        public static List<string> ParseHtml(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var programmerLinks = htmlDoc.DocumentNode.Descendants("li")
                    .Where(node => !node.GetAttributeValue("class", "").Contains("tocsection")).ToList();

            List<string> wikiLink = new List<string>();

            foreach (var link in programmerLinks)
            {
                if (link.FirstChild.Attributes.Count > 0)
                    wikiLink.Add("https://en.wikipedia.org/" + link.FirstChild.Attributes[0].Value);
            }

            return wikiLink;
        }

        public static void Execute()
        {
            string url = "https://en.wikipedia.org/wiki/List_of_programmers";
            var response = CallUrl(url).Result;
            var linklist = ParseHtml(response);

            foreach (var link in linklist)
            {
                Console.WriteLine(link);
            }
        }
    }
}
