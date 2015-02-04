using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIExploration
{
    class Program
    {
        static void Main(string[] args)
        {
            WebScraper myScraper = new WebScraper();
            myScraper.ScrapeURLToFile("", "");
        }
    }
}
