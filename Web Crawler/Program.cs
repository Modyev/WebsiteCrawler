namespace Web_Crawler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Website URL: ");
            string URL = Console.ReadLine();
            Console.Write("\nHow many URLs to visit?: ");
            int urlCount = Convert.ToInt32(Console.ReadLine());
            Task.Factory.StartNew(() => Crawler.UpdateTitle());
            Crawler.Worker(URL, urlCount);
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}