using FeaturesInTheConsole.Service;

namespace FeaturesInTheConsole.Messages{
    public class PortugueseAnswers
    {
        public static string Portuguese => "Tudo bem, vamos cantar algumas músicas. \n\nPara fazer isso, preciso pedir que você siga esta etapa: " +
             "\n\nDigite /letra 'Nome do cantor'-'Nome da música'. Por favor, certifique-se de que" +
             "você seguiu corretamente as instruções.";
        public static string Lyric(string singerName, string musicName)
        {
            string url = VagalumeService.ConvertMusicInformationsToUrl(singerName, musicName);
            var response = VagalumeService.CallUrl(url).Result;
            var lyric = VagalumeService.GetLyricById(response);

            return lyric;
        }

        public static string ErrorAnswer => "Ops, ocorreu um erro. \n\nPor favor, certifique-se que você digitou no formato correto: /lyric Nome do cantor-Nome da musica." +
            "\n\nObs: Não há espaço entre o '-'";
    }
}
