using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PropertyGuruScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your url: ");
            var input = Console.ReadLine();
            if (input == "")
            {
                Console.WriteLine("Please enter your url! Re-launch to add.");
                Console.WriteLine("This is made by RTG, all rights reserved!");
                Console.ReadLine();
            }
            else
            {
                GetHTMLAsync(input);
                Console.ReadLine();
            }
        }

        private static async void GetHTMLAsync(string link)
        {
            var url = link;
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var Products = htmlDocument.DocumentNode.Descendants("ul")
                .Where(node => node.GetAttributeValue("id", "")
                .Equals("ListViewInner")).ToList();

            var ProductListItems = Products[0].Descendants("li")
                .Where(node => node.GetAttributeValue("id", "")
                .Contains("item")).ToList();

            foreach (var ProductListItem in ProductListItems)
            {
                Console.WriteLine(ProductListItem.GetAttributeValue("listingid", ""));
                Console.WriteLine(ProductListItem.Descendants("h3")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("lvtitle")).FirstOrDefault().InnerText.Trim('\r', '\t', '\n')
                    );
                Console.WriteLine(ProductListItem.Descendants("li")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("lvprice prc")).FirstOrDefault().InnerText.Trim('\r', '\t', '\n')
                    );

                Console.WriteLine();
            }

            Console.WriteLine("This is made by RTG, all rights reserved!");
        }
    }
}
