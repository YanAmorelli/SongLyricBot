using System;
using FeaturesInTheConsole.Messages;
using Telegram.Bot;

namespace FeaturesInTheConsole
{
    public static class Program
    {
        static TelegramBotClient Bot = new TelegramBotClient(Configuration.BotToken);

        [Obsolete]
        static void Main(string[] args)
        {
            Bot.StartReceiving();
            Bot.OnMessage += Bot_OnMessage;

            Console.ReadLine();
        }

        [Obsolete]
        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                if (e.Message.Text.StartsWith("/start"))
                {
                    var response = EnglishAnswers.Start;
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, response);
                }

                if (e.Message.Text.StartsWith("/e"))
                {
                    var response = EnglishAnswers.English;
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, response);
                }

                if (e.Message.Text.StartsWith("/lyric"))
                {
                    try
                    {
                        var singerName = e.Message.Text.Split('-')[0].Substring(7);
                        var musicName = e.Message.Text.Split('-')[1];
                        var lyricResponse = EnglishAnswers.Lyric(singerName, musicName);
                        Bot.SendTextMessageAsync(e.Message.Chat.Id, lyricResponse);
                    }
                    catch (Exception)
                    {
                        var error = EnglishAnswers.ErrorAnswer;
                        Bot.SendTextMessageAsync(e.Message.Chat.Id, error);
                    }
                }

                if (e.Message.Text.StartsWith("/p"))
                {
                    var response = PortugueseAnswers.Portuguese;
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, response);
                }

                if (e.Message.Text.StartsWith("/letra"))
                {
                    try
                    {
                        var singerName = e.Message.Text.Split('-')[0].Substring(7);
                        var musicName = e.Message.Text.Split('-')[1];
                        var lyricResponse = PortugueseAnswers.Lyric(singerName, musicName);
                        Bot.SendTextMessageAsync(e.Message.Chat.Id, lyricResponse);
                    }
                    catch (Exception)
                    {
                        var error = PortugueseAnswers.ErrorAnswer;
                        Bot.SendTextMessageAsync(e.Message.Chat.Id, error);
                    }
                }
            }
        }
    }
}
