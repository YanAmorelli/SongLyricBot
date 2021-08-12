using FeaturesInTheConsole.Service;

namespace FeaturesInTheConsole.Messages{
    public class EnglishAnswers{
        public static string Start => "Hi there, my name is SongLyric. I am a bot and my mission is give lyrics of musics to you. " +
            "\n\nBut first of all, which language do you prefer? Portuguese or English? \n\nFor Portuguese type /p \nFor english type /e.";

        public static string English => "Alright, let's sing some songs. To do this, I need to ask you to follow this step: " +
            "\n Type /lyric 'Name of the singer'-'Name of the music'. Please, make sure that " +
            "you correctly followed the instructions.";
        public static string Lyric(string singerName, string musicName)
        {
            string url = VagalumeService.ConvertMusicInformationsToUrl(singerName, musicName);
            var response = VagalumeService.CallUrl(url).Result;
            var lyric = VagalumeService.GetLyricById(response);

            return lyric;
        }

        public static string ErrorAnswer => "Oops, an error occurred. \n\nPlease make sure you type in the correct format: /lyric Name of singer-Name of song. " +
            "\n\nNote: There is no space between the '-'";

    }
}
 