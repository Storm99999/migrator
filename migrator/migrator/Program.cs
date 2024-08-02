using System.Diagnostics;

Console.Title = "$migrator - session id: 1";

Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.WriteLine(@"
                             $$\                                                                          
                           $$$$$$\                                                                        
            $$$$$$\$$$$\  $$  __$$\ $$$$$$$\   $$$$$$\  $$\   $$\  $$$$$$\   $$$$$$\  $$$$$$$\   $$$$$$\  
            $$  _$$  _$$\ $$ /  \__|$$  __$$\ $$  __$$\ $$ |  $$ |$$  __$$\  \____$$\ $$  __$$\ $$  __$$\ 
            $$ / $$ / $$ |\$$$$$$\  $$ |  $$ |$$$$$$$$ |$$ |  $$ |$$ /  $$ | $$$$$$$ |$$ |  $$ |$$ /  $$ |
            $$ | $$ | $$ | \___ $$\ $$ |  $$ |$$   ____|$$ |  $$ |$$ |  $$ |$$  __$$ |$$ |  $$ |$$ |  $$ |
            $$ | $$ | $$ |$$\  \$$ |$$ |  $$ |\$$$$$$$\ \$$$$$$$ |\$$$$$$$ |\$$$$$$$ |$$ |  $$ |\$$$$$$$ |
            \__| \__| \__|\$$$$$$  |\__|  \__| \_______| \____$$ | \____$$ | \_______|\__|  \__| \____$$ |
                           \_$$  _/                     $$\   $$ |$$\   $$ |                    $$\   $$ |
                             \ _/                       \$$$$$$  |\$$$$$$  |                    \$$$$$$  |
                                                         \______/  \______/                      \______/ 
");

Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("            [$migrator]: Migrating from Bloxstrap to DEFAULT ClientSettings in 1100 ms");
Thread.Sleep(1100);
Console.WriteLine("            [$migrator]: Delete all roblox versions in this folder! Then press enter.");
Process.Start("explorer.exe", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\Versions\\");
Console.ReadLine();
var fflags = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Bloxstrap\\Modifications\\ClientSettings\\ClientAppSettings.json");
Console.WriteLine("            [$migrator]: You can now reinstall Roblox and delete bloxstrap. Once reinstalled, press the enter key.");
Console.ReadLine();
Console.WriteLine("            [$migrator]: Migration will now begin.");

var lad = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\Versions\\";
Console.WriteLine("            [$migrator - 1/2]: LocalAppData was found");
string actuaalfolder = string.Empty;
foreach (var flds in Directory.GetDirectories(lad))
{
    // most likely wont happen, but just to make sure. 
    var i = new DirectoryInfo(flds);
    if (i.GetFiles().Length == 0 || i.GetDirectories().Length == 0)
    {
        Console.WriteLine("            [$migrator]: Corrupted roblox version found, ignore - " + i.FullName);
    }

    if (i.GetFiles().Length != 0)
    {
        // Checks for builtinplugins, it's a folder, when that roblox ver is outdated
        foreach (var fe in Directory.GetFiles(i.FullName))
        {
            var fileInfo1 = new FileInfo(fe);
            if (Directory.Exists(i.FullName + "BuiltInPlugins"))
            {
                Console.WriteLine("            [$migrator]: Old roblox version found, ignore - " + i.FullName);
            }
        }
    }

    if (File.Exists(i.FullName + "\\RobloxPlayerBeta.exe"))
    {
        Console.WriteLine("            [$migrator - 2/2]: Found latest! migrating to - " + i.FullName);
        actuaalfolder = i.FullName;
    }
}

Directory.CreateDirectory(actuaalfolder + "\\ClientSettings\\");
File.WriteAllText(actuaalfolder + "\\ClientSettings\\ClientAppSettings.json", fflags);
Console.WriteLine("            [$migrator]: Successfully migrated to DEFAULT roblox client. Launch roblox.");
Console.ReadLine();


