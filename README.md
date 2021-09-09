<h1> SongLyricBot </h1>

<p align="justify">
  <img src="https://img.shields.io/static/v1?label=Telegram&message=Chatbot&color=blue&style=for-the-badge&logo=Telegram"/>
  <img src="https://img.shields.io/static/v1?label=Ngrok&message=deploy&color=blue&style=for-the-badge&logo=Ngrok"/>
  <img src="http://img.shields.io/static/v1?label=Dotnet&message=5.0&color=yellow&style=for-the-badge&logo=Dotnet"/>
  <img src="http://img.shields.io/static/v1?label=STATUS&message=Developing&color=RED&style=for-the-badge"/>
</p>


### Topics 

- [About](#about)
- [Features and new features](#features-and-new-features)
- [Configuration](#configuration)
- [References](#references)


## About
<p align="justify"> 
This project is a Telegram chatbot that responds to the lyrics of a song when the user gives the singer's name and the song's name. 
To obtain the lyrics web scraping was used. The project works with Telegram webhook. 
</p>


## Features and new features
:heavy_check_mark: Reply song lyrics to user
<br>
:hammer: Reply information about the singer (Top 10 musics, news)
<br>
:hammer: Reply the song link on youtube 


## Configuration 
In **appsettings.json**:

``` json
"BotConfiguration": {
    "BotToken": "{BotToken}",
    "HostAddress": "https://mydomain.com"
```

Where BotToken is the Token that BotFather provides you and HostAddres is the server you deploy your application to. For testing, use Ngrok. [How to deploy your application using Ngrok](https://www.youtube.com/watch?v=-Er2kaKxaBg&ab_channel=Jeben).


## References
- [Video that I present the project - In portuguese](https://youtu.be/kcux_o1UDzs)

- [Official TelegramBot library](https://core.telegram.org/bots/api)

- [Sample project with assembled project pattern](https://github.com/TelegramBots/Telegram.Bot.Examples/tree/master/Telegram.Bot.Examples.WebHook)

- [Introduction - A guide to Telegram.Bot library](https://telegrambots.github.io/book/)
