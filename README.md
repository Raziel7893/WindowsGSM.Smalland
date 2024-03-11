# WindowsGSM.Smalland
🧩WindowsGSM plugin that provides Smalland Dedicated server

## PLEASE ⭐STAR⭐ THE REPO IF YOU LIKE IT! THANKS!

### WindowsGSM Installation: 
1. Download  WindowsGSM https://windowsgsm.com/ 
2. Create a Folder at a Location you wan't all Server to be Installed and Run.
3. Drag WindowsGSM.Exe into previously created folder and execute it.

### Plugin Installation:
1. Download [latest](https://https://github.com/Raziel7893/WindowsGSM.Smalland/releases/latest) release
2. Either Extract then Move the folder **Smalland.cs** to **WindowsGSM/plugins** 
    1. Press on the Puzzle Icon in the left bottom side and press **[RELOAD PLUGINS]** or restart WindowsGSM
3. Or Press on the Puzzle Icon in the left bottom side and press **[IMPORT PLUGIN]** and choose the downloaded .zip

### Official Documentation
🗃️ Didn't find any documentation yet. Please Let me know if you came accros one

### The Game
🕹️ https://store.steampowered.com/app/768200/Smalland_Survive_the_Wilds/

### Dedicated server info
🖥️ https://steamdb.info/app/808040/info/

### Port Forwarding
- 7777 UDP - Default
- 7777 TCP - if you are using 8211 it automatically using 8211 +1 = 8212 for QueryPort so you have to port forward this

### Files To Backup
- Save Gane (You could only save serverfiles/SMALLAND/Saved , but that includes many big logs)
  - WindowsGSM\servers\%ID%\serverfiles/SMALLAND/Saved/SaveGames
  - WindowsGSM\servers\%ID%\serverfiles/SMALLAND/Saved/Config/WindowsServer", backupName="Smalland/Smalland/Saved/Config/WindowsServer
- WindowsGSM Config
  - WindowsGSM\servers\%ID%\configs

### Available Params
All these params are automatically set by WGSM
- -port=8211                    can be change and working (Change via WGSM settings)
- -servername=""                can override via Server Start Param box (WGSM Edit button)
- -serverdescription=""         can override via Server Start Param box (WGSM Edit button)
- #-NOSTEAM                      Suggested StartParameters from the default start script, not sure what it does, changes nothing, so it is skipped by default. Maybe there is an update on release for it (cann be added in Additional Parameters)
- -log                          creates logfiles in serverId\serverfiles\SMALLAND\Saved\Logs

### Config Guide
I Created A configFile to set world-parameters(Including Password) to not clutter the AdditionalParameter Box and dueto strange format of the Start Parameters
Go to serverId\serverfiles\smalland_conf.json and edit it with your preffered text Editior. 
- "Password": "",                       Locks Server behind a password promt
- "FriendlyFire": false,                Friendly fire between players
- "PeacefullMode": false,               
- "KeepInventory": false,               Keep inventory on death
- "NoDeterioration": false,             Disable building wather deterioration
- "Private": false,                     Private server is hidden on the server browser (Not possible yet, as there is no way to connect without the ServerBrowser)
- "LengthOfDaySeconds": 1800,           Length of day in seconds (default is 1800 which is 30 minutess)
- "LengthOfSeasonSeconds": 10800,       Length of season in seconds (default is 10800 which is 3 hours)
- "CreatureHealthModifier": 100,        Creature health modifier (20-300 default is 100)
- "CreatureDamageModifier": 100,        Creature damage modifier (20-300 default is 100)
- "NourishmentLossModifier": 100,       Nourishment loss modifier (0-100 default is 100)
- "FalldamageModifier": 100,            Fall damage modifier (50-100 default is 100)

The last 4 Options are copied from the shipped Start-Script(serverfiles/start-server.bat) on install. They are apperantly mandatory, so the should not be changed
- "DeploymentId": "50f2b148496e4cbbbdeefbecc2ccd6a3",
- "ClientId": "xyza78918KT08TkA6emolUay8yhvAAy2",
- "ClientSecret": "aN2GtVw7aHb6hx66HwohNM+qktFaO3vtrLSbGdTzZWk",
- "PrivateKey": ""

### Connecting on a locked server (with password)
For now only Connecting via a the ingame serverbrowser is possible. It is recommended to note down the 5 Options after password and filter your servers accordingly (as there is no name filter either)
- On the left of the join window be sure to click public (not official)
- On the lower part choose your options to filter down the list of servers
- Be sure to favour your server, so they should show up under Favorites on the next time

### Other notes
- The game is currently in Early Access Stage WGSM and this plugin is not taking liability if something happens to your server, the app is only for managing your server easily

### How can you play with your friends without port forwarding?
- Use [zerotier](https://www.zerotier.com/) folow the basic guide and create network
- Download the client app and join to your network
- Create static IP address for your host machine
- Edit WGSM IP Address to your recently created static IP address
- Give your network ID to your friends
- After they've joined to your network
- They can connect using the IP you've created eg: 10.123.17.1:7777
- Enjoy

### Support
[WGSM](https://discord.com/channels/590590698907107340/645730252672335893)

### Give Love!
[Buy me a coffee](https://ko-fi.com/raziel7893)

[Paypal](https://paypal.me/raziel7893)

### License
This project is licensed under the MIT License - see the <a href="https://github.com/ohmcodes/WindowsGSM.Palworld/blob/main/LICENSE">LICENSE.md</a> file for details

### Thanks
Thanks to ohmcodes for the Enshrouded and Palworld Plugins which i used for guidance to create this one
