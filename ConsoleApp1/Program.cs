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
          
            var websiteUrl = args[0];

            HttpClient http = new HttpClient();
            var response = await http.GetAsync(websiteUrl);
            var content = await response.Content.ReadAsStringAsync();
            var regex = new Regex(@"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+");

            var matchCollection = regex.Matches(content);

            foreach(var item in matchCollection)
            {
                Console.WriteLine(item);
              
            }
            
            //Console.WriteLine(response);
        
        
        }
    }
}
