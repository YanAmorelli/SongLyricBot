using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebScraping.FeaturesInTheConsole
{
    public class VagalumeTest{
        public static async Task<string> CallUrl(string fullUrl){
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync(fullUrl);
            return await response;
        }

        public static string ConvertMusicInformationsToUrl(string singer, string music){
            var singerToUrl = singer.Replace(" ", "-").ToLower();
            var musicToUrl = music.Replace(" ", "-").ToLower();

            return $"https://www.vagalume.com.br/{singerToUrl}/{musicToUrl}.html";
        }

        public static string GetLyricById(string html){
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            var verses = document.GetElementbyId("lyrics").InnerHtml.Replace("<br>", "\n");

            return verses;
        }

        public static void Execute(){
            Console.WriteLine("Enter the name of the singer");
            string singerName = Console.ReadLine();
            Console.WriteLine("Enter the name of the song");
            string musicName = Console.ReadLine();

            string url = ConvertMusicInformationsToUrl(singerName, musicName);
            var response = CallUrl(url).Result;
            var lyric = GetLyricById(response);
            
            Console.WriteLine(lyric);
        }
    }
}
