using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WindowsGSM.Functions;
using WindowsGSM.GameServer.Query;
using WindowsGSM.GameServer.Engine;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace WindowsGSM.Plugins
{
    public class Smalland : SteamCMDAgent
    {
        // - Plugin Details
        public Plugin Plugin = new Plugin
        {
            name = "WindowsGSM.Smalland", // WindowsGSM.XXXX
            author = "raziel7893",
            description = "WindowsGSM plugin for supporting Smalland Dedicated Server",
            version = "1.0.0",
            url = "https://github.com/Raziel7893/WindowsGSM.Smalland", // Github repository link (Best practice) TODO
            color = "#34FFeb" // Color Hex
        };

        // - Settings properties for SteamCMD installer
        public override bool loginAnonymous => true;
        public override string AppId => "808040"; // Game server appId Steam

        // - Standard Constructor and properties
        public Smalland(ServerConfig serverData) : base(serverData) => base.serverData = _serverData = serverData;
        private readonly ServerConfig _serverData;


        // - Game server Fixed variables
        //public override string StartPath => "SMALLANDServer.exe"; // Game server start path
        public override string StartPath => "SMALLAND\\Binaries\\Win64\\SMALLANDServer-Win64-Shipping.exe";
        public string FullName = "Smalland Dedicated Server"; // Game server FullName
        public string ConfigFile = "smalland_conf.json"; // Game server FullName
        public string StartScript = "start-server.bat";
        public bool AllowsEmbedConsole = true;  // Does this server support output redirect?
        public int PortIncrements = 1; // This tells WindowsGSM how many ports should skip after installation

        // - Game server default values
        public string Port = "7777"; // Default port

        public string Additional = "-log"; // Additional server start parameter

        // TODO: Following options are not supported yet, as ther is no documentation of available options
        public string Maxplayers = "16"; // Default maxplayers        
        public string QueryPort = "7778"; // Default query port. This is the port specified in the Server Manager in the client UI to establish a server connection.
        // TODO: Unsupported option
        public string Defaultmap = "Dedicated"; // Default map name
        // TODO: Undisclosed method
        public object QueryMethod = new A2S(); // Query method should be use on current server type. Accepted value: null or new A2S() or new FIVEM() or new UT3()


        public class SmallandConfig
        {
            public string Password = "";
            public bool FriendlyFire = false;
            public bool PeacefullMode = false;
            public bool KeepInventory = false;
            public bool NoDeterioration= false;
            public bool Private = false;
            public int LengthOfDaySeconds = 1800;
            public int LengthOfSeasonSeconds = 10800;
            public int CreatureHealthModifier = 100;
            public int CreatureDamageModifier = 100;
            public int NourishmentLossModifier = 100;
            public int FalldamageModifier = 100;


            public string DeploymentId= "";
            public string ClientId = "";
            public string ClientSecret = "";
            public string PrivateKey = "";
        }

        // - Create a default cfg for the game server after installation
        public async void CreateServerCFG()
        {
            //will be passed as start argument from file
            var config = new SmallandConfig()
            {
                Password = "",
                FriendlyFire = false,
                PeacefullMode = false,
                KeepInventory = false,
                NoDeterioration = false,
                Private = false,
                LengthOfDaySeconds = 1800,
                LengthOfSeasonSeconds = 10800,
                CreatureHealthModifier = 100,
                CreatureDamageModifier = 100,
                NourishmentLossModifier = 100,
                FalldamageModifier = 100
            };
            ReadEngineKeys(config);
            // Convert the object to JSON format
            string jsonContent = JsonConvert.SerializeObject(config, Formatting.Indented);

            // Specify the file path
            string filePath = Functions.ServerPath.GetServersServerFiles(_serverData.ServerID, ConfigFile);

            // Write the JSON content to the file
            File.WriteAllText(filePath, jsonContent);
        }

        private void ReadEngineKeys(SmallandConfig config)
        {
            string shipScript = Functions.ServerPath.GetServersServerFiles(_serverData.ServerID, StartScript);
            if (!File.Exists(shipScript))
            {
                Error = "shipped start-server.bat not found, cant read Engine:[EpicOnlineServices] values, trying without";
            }
            StreamReader sr = new StreamReader(shipScript);
            var line = sr.ReadLine();
            while (line != null)
            {
                if (line.Contains("set DEPLOYMENTID"))
                    config.DeploymentId = line.Split('=')[1];
                if (line.Contains("set CLIENTID"))
                    config.ClientId = line.Split('=')[1];
                if (line.Contains("set CLIENTSECRET"))
                    config.ClientSecret = line.Split('=')[1];
                if (line.Contains("set PRIVATEKEY"))
                    config.PrivateKey = line.Split('=')[1];

                line = sr.ReadLine();
            }
        }

        // - Start server function, return its Process to WindowsGSM
        public async Task<Process> Start()
        {
            string shipExePath = Functions.ServerPath.GetServersServerFiles(_serverData.ServerID, StartPath);
            if (!File.Exists(shipExePath))
            {
                Error = $"{Path.GetFileName(shipExePath)} not found ({shipExePath})";
                return null;
            }

            string configPath = Functions.ServerPath.GetServersServerFiles(_serverData.ServerID, ConfigFile);
            if (!File.Exists(configPath))
            {
                Error = $"{configPath} not found";
                return null;
            }

            StreamReader configReader = new StreamReader(configPath);
            var config = JsonConvert.DeserializeObject<SmallandConfig>(configReader.ReadToEnd());
            //Try gather a password from the gui

            StringBuilder sb = new StringBuilder();
            sb.Append($"/Game/Maps/WorldGame/WorldGame_Smalland?SERVERNAME=\"{_serverData.ServerName}\"?WORLDNAME=\"{_serverData.ServerMap}\"");
            if (!string.IsNullOrWhiteSpace(config.Password)) sb.Append($"?PASSWORD=\"{config.Password}\"");
            if (config.FriendlyFire) sb.Append("?FRIENDLYFIRE");
            if (config.PeacefullMode) sb.Append("?PEACEFULMODE");
            if (config.KeepInventory) sb.Append("?KEEPINVENTORY");
            if (config.NoDeterioration) sb.Append("?NODETERIORATION");
            if (config.Private) sb.Append("?PRIVATE");
            sb.Append($"?lengthofdayseconds={config.LengthOfDaySeconds}");
            sb.Append($"?lengthofseasonseconds={config.LengthOfSeasonSeconds}");
            sb.Append($"?creaturehealthmodifier={config.CreatureHealthModifier}");
            sb.Append($"?creaturedamagemodifier={config.CreatureDamageModifier}");
            sb.Append($"?nourishmentlossmodifier={config.NourishmentLossModifier}");
            sb.Append($"?falldamagemodifier={config.FalldamageModifier}");
            
            if(!string.IsNullOrEmpty(config.DeploymentId) || !string.IsNullOrEmpty(config.ClientId) || !string.IsNullOrEmpty(config.ClientSecret))
            {
                ReadEngineKeys(config);
            }

            sb.Append($" -ini:Engine:[EpicOnlineServices]:DeploymentId={config.DeploymentId}");
            sb.Append($" -ini:Engine:[EpicOnlineServices]:DedicatedServerClientId={config.ClientId}");
            sb.Append($" -ini:Engine:[EpicOnlineServices]:DedicatedServerClientSecret={config.ClientSecret}");
            if (!string.IsNullOrWhiteSpace(config.PrivateKey)) sb.Append($" -ini:Engine:[EpicOnlineServices]:DedicatedServerPrivateKey={config.PrivateKey}");

            sb.Append($" -port={_serverData.ServerPort} {Additional}");

            // Prepare Process
            var p = new Process
            {
                StartInfo =
                {
                    CreateNoWindow = true,  //wird komplett ignoriert?!?
                    WorkingDirectory = ServerPath.GetServersServerFiles(_serverData.ServerID),
                    FileName = shipExePath,
                    Arguments = sb.ToString(),
                    WindowStyle = ProcessWindowStyle.Hidden,  //wird komplett ignoriert?!?
                    UseShellExecute = false
                },
                EnableRaisingEvents = true
            };

            // Set up Redirect Input and Output to WindowsGSM Console if EmbedConsole is on
            if (AllowsEmbedConsole)
            {
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                var serverConsole = new ServerConsole(_serverData.ServerID);
                p.OutputDataReceived += serverConsole.AddOutput;
                p.ErrorDataReceived += serverConsole.AddOutput;
            }

            // Start Process
            try
            {
                p.Start();
                if (AllowsEmbedConsole)
                {
                    p.BeginOutputReadLine();
                    p.BeginErrorReadLine();
                }
                return p;
            }
            catch (Exception e)
            {
                Error = e.Message;
                return null; // return null if fail to start
            }
        }

        // - Stop server function
        public async Task Stop(Process p)
        {
            await Task.Run(() =>
            {
                Functions.ServerConsole.SetMainWindow(p.MainWindowHandle);
                Functions.ServerConsole.SendWaitToMainWindow("^c");
                p.WaitForExit(2000);
            });
        }
    }
}
