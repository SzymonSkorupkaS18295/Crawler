using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            if (string.IsNullOrEmpty(args[0]))
            {
                throw new ArgumentNullException();
            }

            var ulr = new UriBuilder(args[0]).Uri;

            if (!Uri.IsWellFormedUriString(ulr.ToString(), UriKind.Absolute))
            {
                throw new ArgumentException();
            }

            await Program.GetWebPage(ulr.ToString());
        }
        public static async Task GetWebPage(String url)
        {
            using var httpClient = new HttpClient();
            String pageSource = "";

            try
            {
                pageSource = await httpClient.GetStringAsync(url);
            }
            catch (Exception error)
            {
                Console.WriteLine("Błąd wczasie pobierania strony");
                Console.WriteLine(error);
                return;
            }


            var emailRegex = @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";





            var matchResult = Regex
                .Matches(pageSource, emailRegex)
                .Select(match => match.Value)
                .Distinct();

            if (!matchResult.Any())
            {
                Console.WriteLine("Nie znaleziono emaili");
                return;
            }

            foreach (var email in matchResult)
            {
                Console.WriteLine(email);
            }
        }
    }
}
