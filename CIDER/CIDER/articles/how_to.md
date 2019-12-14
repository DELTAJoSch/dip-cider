#Getting Started

##Basics

This software works on Windows PC`s with .Net Framework 4.7.2 installed. If you are running Windows 10 with the latest updates installed this should work out of the box for you. If you are not running Windows 10 head to [this link](https://dotnet.microsoft.com/download/dotnet-framework/net472) and download and install it manually.

##Installation

###Installer
Download the installer and execute it.

###Build from Source
If you want to build this project from source download or clone the repository to your computer. After downloading, open the .sln file in visual studio. The project should be loaded. Set the build settings to "Release" and press the start button - afterwards, copy the contents of the Release folder to a location of your choosing.

##Setup
After successfully installing the application, launch the application. You will be presented with a license dialog. Accept the licenses and click ok. You will now notice that the map views are greyed out. Please head to the [Bing Maps Portal](https://www.bingmapsportal.com/) and create an account. Afterwards click "My Keys", the create a new key. Set the application name to CIDER, and the key type to Windows App. Copy the key into a new file at a location of your choosing. The file ending should be .key. Add the key to the application. Head to the about view and press "Add new key". Select the file and press ok. You should be able to use the map views now.

##Load a track
![Load View](../images/load.png)

1. Head to the load view
2. Press "..."
3. If you see a green tick mark, press "Load"

You successfully loaded the track.