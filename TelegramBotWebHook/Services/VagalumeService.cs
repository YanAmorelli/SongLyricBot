using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;

namespace TelegramBotWebHook.Services
{
    public class VagalumeService
    {
        public static async Task<string> CallUrl(string fullUrl)
        {
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync(fullUrl);
            return await response;
        }

        public static string ConvertMusicInformationsToUrl(string singer, string music)
        {
            var singerToUrl = singer.Replace(" ", "-").ToLower();
            var musicToUrl = music.Replace(" ", "-").ToLower();

            return $"https://www.vagalume.com.br/{singerToUrl}/{musicToUrl}.html";
        }

        public static string GetLyricById(string html)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            var verses = document.GetElementbyId("lyrics").InnerHtml.Replace("<br>", "\n");

            return verses;
        }
    }
}
