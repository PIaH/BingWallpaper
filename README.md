# BingWallpaper
A useful console application that sets the Bing wallpaper of the day without having tons of popups etc.
## What it does
Put this application into your autostart and it will give you another wallpaper every day.
It will also display a christian text in an overlay within the bitmap. The texts are the "Losungen" from http://www.losungen.de/.
## How to use
  - Build with msbuild (VS 2013): 
    Debug: ```msbuild BingDesktop.sln /p:Configuration=Debug```
	Release: ```msbuild BingDesktop.sln /p:Configuration=Release```

  - Run the application ```BingDesktop.exe```