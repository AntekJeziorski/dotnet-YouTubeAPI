# YouBookie - YouTubeAPI project

The YouBookie app is used to track the statistics of selected videos and artists from the YouTube platform. 
Using the YouTube Data API, it retrieves available data such as the number of views, 
likes and comments for videos, as well as the number of subscriptions, views and videos for creators. 
In addition, the application stores the URL of thumbnails as well as descriptions of videos and channels.
All this information is stored in a specially designed relational database. The programme allows the history 
of statistics to be visualised in graphs.

## build

To run the application, you must have the .Net framework version 4.8 installed on your system. 
To build the application, you can use Visual Studio or use the command line in the project's root directory.

To build and run the application you need to:
1. Navigate to the project solution folder.
2. Run: msbuild dotnet-YouTubeAPI.sln /p:Configuration=Release
3. Run: .\bin\Release\dotnet-YouTubeAPI.exe

## docs

The `/docs` directory contains the latest version of the project documentation generated using the doxygen system. 
The docs file is `/docs/html/index.html`.

## tests

Automatic test implemented for main classes using NUnit package.
Can be run from Visual Studio or NUnit Console.

## contrubutors

- Antoni Jeziorski, 259254@studnet.pwr.edu.pl
- Szymon Sobczak, 259275@student.pwr.edu.pl
- Jędrzej Szymczyk, 254898@student.pwr.edu.pl
