using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;
using System.Text.RegularExpressions;
using Console = Colorful.Console;
using HtmlAgilityPack;

namespace Web_Crawler
{
    internal class Crawler
    {
        public static HashSet<string> visitedUrls = new HashSet<string>();
        public static List<string> AllLinks = new List<string>();
        public static int UrlCount = 0;
        public static List<string> LinksFull = new List<string>();
        public static int Error = 0;
        public static int maxURLs { get; set; }
        public static void Worker(string domain, int maxUrlcount)
        {
            maxURLs = maxUrlcount;
            if (visitedUrls.Count > maxURLs || visitedUrls.Contains(domain))
                return;

            visitedUrls.Add(domain);

            HttpRequest req = new HttpRequest();
            req.UserAgentRandomize();
            req.KeepAlive = true;
            req.IgnoreProtocolErrors = true;
            string response = "";
            try
            {
                response = req.Get(domain).ToString();
            }
            catch (Exception)
            {
                Error++;
            }
            var Links = ExtractLinks(response);

            foreach (var link in Links)
            {
                if (visitedUrls.Count < maxURLs)
                {
                    Worker(link, maxURLs);
                }
            }
        }


        public static List<string> ExtractLinks(string resp)
        {
            string hrefPattern = @"<a\s+(?:[^>]*?\s+)?href=([""'])(.*?)\1";
            MatchCollection matches = Regex.Matches(resp, hrefPattern, RegexOptions.IgnoreCase);
            List<string> Links = new List<string>();

            foreach (Match match in matches)
            {
                string link = match.Groups[2].Value;

                if (ValidateLink(link))
                {
                    UrlCount++;
                    Links.Add(link);
                    Console.WriteLine(link);
                    Export(link);
                }
            }
            return Links;
        }
        public static bool ValidateLink(string link)
        {
            if (link.StartsWith("http"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        int errors = 0;
        public static void UpdateTitle()
        {
            for (; ; )
            {
                Console.Title = $"Web Crawler | URL COUNT: {UrlCount} - URLs To Visit: {maxURLs} - Errors: {Error}";
                Thread.Sleep(2000);
            }
        }
        static string Parse(string source, string left, string right) => source.Split(new string[1]
        {
            left
        }, StringSplitOptions.None)[1].Split(new string[1]
        {
            right
        }, StringSplitOptions.None)[0];

        public static ReaderWriterLockSlim Lock = new ReaderWriterLockSlim();
        public static void Export(string content)
        {
            Lock.EnterWriteLock();
            try
            {
                using (StreamWriter streamWriter = File.AppendText("CrawledLinks.txt"))
                {
                    streamWriter.WriteLine(content);
                    streamWriter.Close();
                }
            }
            finally
            {
                Lock.ExitWriteLock();
            }

            return;
        }
    }
}
