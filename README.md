# BingWallpaper

![BingWallpaper Logo](https://raw.githubusercontent.com/piah/BingWallpaper/master/BingWallpaper.Docs/logo.jpg)

A simple but useful  application that sets the Bing wallpaper of the day without having tons of popups etc.
There is no need to use the existing BingDesktop application from Microsoft that interrupts you with popups while working.
## What it does
Put this application into your autostart and it will give you another wallpaper every day.
It will also display a christian text in an overlay within the bitmap. The texts are taken from the german "Daily Watchwords" (https://de.wikipedia.org/wiki/Herrnhuter_Losungen) that can be found at http://www.losungen.de/.

The current support language of the texts is German.

## Build
Build with msbuild (requires Microsoft Visual Studio 2013+): 
- Debug: ```msbuild BingWallpaper.sln /p:Configuration=Debug```
- Release: ```msbuild BingWallpaper.sln /p:Configuration=Release```

## Run
### Windows
- Requires .Net-Framework 4.6 (https://www.microsoft.com/de-de/download/details.aspx?id=49982)
- Run the application (WPF UI) ```BingWallpaper.Desktop.exe```
- Run the application (Console UI) ```BingWallpaper.Console.exe```

### Mac OS X / Linux
- Requires Mono 4.6 (http://www.mono-project.com/download/#download-mac)
- Run in the Mono-Commandline: ```mono BingWallpaper.Console.exe```