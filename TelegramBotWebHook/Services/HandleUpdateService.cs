using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBotWebHook.Services
{
    public class HandleUpdateService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ILogger<HandleUpdateService> _logger;

        public HandleUpdateService(ITelegramBotClient botClient, ILogger<HandleUpdateService> logger)
        {
            _botClient = botClient;
            _logger = logger;
        }

        public async Task ReplyLyricAsync(Update update)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageReceived(update.Message),
                UpdateType.EditedMessage => BotOnMessageReceived(update.Message),
                _ => UnknownUpdateHandlerAsync(update)
            };

            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(exception);
            }
        }

        private async Task BotOnMessageReceived(Message message)
        {
            _logger.LogInformation($"Receive message type: {message.Type}");
            if (message.Type != MessageType.Text)
                return;

            var action = message.Text.Split(' ').First() switch
            {
                "/portuguese" => SendResponseInPortuguese(_botClient, message),
                "/english" => SendResponseInEnglish(_botClient, message),
                "/lyric" => SendLyricInEnglish(_botClient, message),
                "/letra" => SendLyricInPortuguese(_botClient, message),
                _ => Usage(_botClient, message)
            };
            var sentMessage = await action;
            _logger.LogInformation($"The message was sent with id: {sentMessage.MessageId}");

            static async Task<Message> Usage(ITelegramBotClient bot, Message message)
            {
                const string usage = "Usage:\n" +
                                     "/portuguese - set bot language to portuguese\n" +
                                      "/english - set bot language to english";

                return await bot.SendTextMessageAsync(chatId: message.Chat.Id,
                                                      text: usage);
            }

            static async Task<Message> SendResponseInEnglish(ITelegramBotClient bot, Message message)
            {
                await bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                await Task.Delay(500);

                var english = TextResponsesService.English;
                return await bot.SendTextMessageAsync(message.Chat.Id, english);
            }

            static async Task<Message> SendResponseInPortuguese(ITelegramBotClient bot, Message message)
            {
                await bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                await Task.Delay(500);

                var english = TextResponsesService.Portuguese;
                return await bot.SendTextMessageAsync(message.Chat.Id, english);
            }
        }

        static async Task<Message> SendLyricInEnglish(ITelegramBotClient bot, Message message)
        {
            await bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            await Task.Delay(500);

            var singerName = message.Text.Split('-')[0].Substring(7);
            var musicName = message.Text.Split('-')[1];
            var lyricResponse = TextResponsesService.LyricResponseInEnglish(singerName, musicName);
            return await bot.SendTextMessageAsync(message.Chat.Id, lyricResponse);
        }

        static async Task<Message> SendLyricInPortuguese(ITelegramBotClient bot, Message message)
        {
            await bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            await Task.Delay(500);

            var singerName = message.Text.Split('-')[0].Substring(7);
            var musicName = message.Text.Split('-')[1];
            var lyricResponse = TextResponsesService.LyricResponseInPortuguese(singerName, musicName);
            return await bot.SendTextMessageAsync(message.Chat.Id, lyricResponse);
        }

        private Task UnknownUpdateHandlerAsync(Update update)
        {
            _logger.LogInformation($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }

        public Task HandleErrorAsync(Exception exception)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            _logger.LogInformation(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}