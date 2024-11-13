using System;
using System.IO;
using Godot;
using Godot.Collections;

public class SaveUtils {

	protected static string GetUniqueName(string name, string directory) {
		
		string OutputName = name;

		while (true)
		if (!DirAccess.DirExistsAbsolute(directory.PathJoin(OutputName))) {
			return OutputName;
		} else {
			OutputName += "(1)";
		}
	}
	public static void NewSave(string directory) {
		NewSave("unnamed_game_save", directory);
	}
	public static void NewSave(string name, string directory) {

		name = GetUniqueName(name, directory);
		string NewDirectory = directory.PathJoin(name);
		DirAccess.MakeDirRecursiveAbsolute(NewDirectory);     

        Dictionary Data = new();

        Data.Add("level", 0);
        Data.Add("checkpoint", 0);

        string JSON = Json.Stringify(Data);
        
		// initialise save file
		Godot.FileAccess SaveFile = Godot.FileAccess.Open(NewDirectory.PathJoin("save.json"), Godot.FileAccess.ModeFlags.WriteRead);
        SaveFile.StoreString(JSON);

		GD.Print("[SaveUtils.NewSave] Save successfully created at : " + ProjectSettings.GlobalizePath(NewDirectory));
	}

    public static string LoadSave(string name, string directory) {

        string SavePath = directory.PathJoin(name);
        if (!Directory.Exists(SavePath)) {
            throw new Exception ("[SaveUtils.LoadSave] Cannot load save. Save does not exist!");
        }

        return File.ReadAllText(SavePath.PathJoin("save.json"));
    }
}
