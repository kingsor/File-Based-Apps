Console.WriteLine("Show Folders script");

Console.WriteLine($"Current Directory: {Environment.CurrentDirectory}");
Console.WriteLine($"Base Directory: {AppContext.BaseDirectory}");
Console.WriteLine($"Entry Point File Path: {AppContext.GetData("EntryPointFilePath")}");
Console.WriteLine($"Entry Point File Directory: {AppContext.GetData("EntryPointFileDirectoryPath")}");

