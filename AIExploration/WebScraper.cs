//----------------------------------------------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------------------------
// WebScraper is a class that can navigate to an URL and scrape the URL information into a text file.
//  It stores no markup -- just a pure scraper.
//  It can handle paginated data.  A page like reddit.com can be scraped for (n) pages ( 0 == infinity? )
//
//  Ideas from : http://stackoverflow.com/questions/4377355/i-need-a-powerful-web-scraper-library
//----------------------------------------------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------------------------

/* Questions:
 * Should I use C# APIs, or automate IE and save the recieved HTML somehow?
 * 
 * Test case -- reddit.com front page for 5 pages.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace AIExploration
{
    class WebScraper
    {
        public void ScrapeURLToFile(string URL, string File)
        {
            ParameterizedThreadStart startBrowser = new ParameterizedThreadStart(ThreadedScrapeUrlToFile);
            string[] myParams = {URL, File};
            Thread browserThread = new Thread(startBrowser);
            browserThread.SetApartmentState(ApartmentState.STA); // set to single threaded as Browser object requires it.
            browserThread.Start(myParams);
        }

        public void ThreadedScrapeUrlToFile( object Args )
        {
            // Let's try automating IE.
            WebBrowser myBrowser = new WebBrowser();
            myBrowser.DocumentCompleted += myBrowser_DocumentCompleted;
            myBrowser.Navigate("http://www.reddit.com");
            myBrowser.Visible = true;
            Application.Run();
        }

        void myBrowser_DocumentCompleted( object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var br = sender as WebBrowser;
            if (br.Url == e.Url)
            {
                Console.WriteLine("Navigated to {0}", e.Url);
                Application.ExitThread();
            }
        }

        public void TestReddit()
        {
            
        }

    }
}
