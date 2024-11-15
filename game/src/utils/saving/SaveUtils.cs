using System;
using System.IO;
using Godot;
using Godot.Collections;

public class SaveUtils {

	// add 1 to revision when you are changing the data schema of save files
	public static int SaveFileRevision {get; private set;} = 0;

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

		Data.Add("revision", SaveFileRevision);
		Data.Add("levelName", name);
        Data.Add("level", 0);
        Data.Add("checkpoint", 0);

        string JSON = Json.Stringify(Data);
        
		// initialise save file
		Godot.FileAccess SaveFile = Godot.FileAccess.Open(NewDirectory.PathJoin("save.json"), Godot.FileAccess.ModeFlags.WriteRead);
		SaveFile.StoreString(JSON);
		SaveFile.Close();


		GD.Print("[SaveUtils.NewSave] Save successfully created at : " + ProjectSettings.GlobalizePath(NewDirectory));
	}

	public static string LoadSave(string LinkToSaveDirectory) {
	    if (!Directory.Exists(LinkToSaveDirectory)) {
            throw new Exception ("[SaveUtils.LoadSave] Cannot load save at " + LinkToSaveDirectory + " Does not exist!");
        }

        return File.ReadAllText(LinkToSaveDirectory.PathJoin("save.json"));
	}


    public static string LoadSave(string name, string directory) {

        string SavePath = directory.PathJoin(name);
		return LoadSave(SavePath);

    }
}
