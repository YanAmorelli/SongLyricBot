namespace TelegramBotWebHook.Services
{
    public class TextResponsesService
    {
        public static string English => "Alright, let's sing some songs. \n\nTo do this, I need to ask you to follow this step: " +
            "\n\n Type /lyric 'Name of the singer'-'Name of the music'. Please, make sure that " +
            "you correctly followed the instructions.";
        
        public static string Portuguese => "Tudo bem, vamos cantar algumas músicas. \n\nPara fazer isso, preciso pedir que você siga esta etapa: " +
             "\n\nDigite /letra 'Nome do cantor'-'Nome da música'. Por favor, certifique-se de que" +
             "você seguiu corretamente as instruções.";

        public static string Error => "Oops, an error occurred. \n\nPlease make sure you type in the correct format: /lyric Name of singer-Name of song. " +
                    "\n\nNote: There is no space between the '-'";

        public static string  ErrorInPortuguese => "Ops, ocorreu um erro. \n\nPor favor, certifique-se que você digitou no formato correto: " +
            "/letra Nome do cantor-Nome da musica. \n\nObs: Não há espaço entre o '-'";

        private static string GetLyric(string singerName, string musicName)
        {
            string url = VagalumeService.ConvertMusicInformationsToUrl(singerName, musicName);
            var response = VagalumeService.CallUrl(url).Result;
            var lyric = VagalumeService.GetLyricById(response);
            return lyric;
        }
        
        public static string LyricResponseInEnglish(string singerName, string musicName)
        {
            try
            {
                var lyric = GetLyric(singerName, musicName);
                return lyric;

            }
            catch (System.Exception)
            {
                var error = Error;
                return error;
            }
        }

        public static string LyricResponseInPortuguese(string singerName, string musicName)
        {
            try
            {
                var lyric = GetLyric(singerName, musicName);
                return lyric;

            }
            catch (System.Exception)
            {
                var error = ErrorInPortuguese;
                return error;
            }
        }
    }

}

