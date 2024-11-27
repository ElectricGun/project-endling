using System;
using System.IO;
using Godot;
using Godot.Collections;
using utils;
using static DictionaryKeys;

public class SaveUtils {

	// add 2 to revision when you are changing the data schema of save files
	public static int SaveFileRevision {get; private set;} = 2;

	protected static string GetUniqueName(string name, string directory) {
		
		string OutputName = name;

		while (true)
		if (!DirAccess.DirExistsAbsolute(directory.PathJoin(OutputName))) {
			return OutputName;
		} else {
			OutputName += "(1)";
		}
	}
	public static Dictionary GenerateObjectDataTemplateDict() {
		Dictionary ObjectData = new()
		{
			{ KeyIsSpawned, false },
			{ KeyNodePath , "" },
			{ KeyPositionX, 0 },
			{ KeyPositionY, 0 },
			{ KeyNodeType, typeof(LevelObject).ToString()}
		};

		return ObjectData;
	}

	public static Dictionary GeneratePlayerDataTemplateDict() {
		Dictionary PlayerData = new()
		{
			{ KeyPositionX, 0 },
			{ KeyPositionY, 0 },
			{ KeyVelocityX, 0 },
			{ KeyVelocityY, 0 },
			{ KeyCheckpoint, 0 },
			{ KeyPlayerAlreadySpawned, false}
		};

		return PlayerData;
	}

	public static Dictionary GenerateLevelDataTemplateDict() {
		// create starting level with empty object data
		Dictionary WorldData = new()
		{
			{ KeyPlayerData, GeneratePlayerDataTemplateDict()},
			{ KeyLevelObjects, new Godot.Collections.Array() }
		};
		return WorldData;
	}

	public static Dictionary GenerateSaveTemplateDict(string saveName) {

		// add starting level to levels
		Dictionary Levels = new()
		{
			 {"0", GenerateLevelDataTemplateDict()}
		};

		// master data
        Dictionary Data = new()
        {
            { KeyFormatRevision, SaveFileRevision },
            { KeySaveName, saveName },
            { KeyCurrentLevel, 0 },
            { KeySavedLevels, Levels }
        };
		
		return Data;
	}

	public static void NewSave(string directory) {
		NewSave("unnamed_game_save", directory);
	}
	public static void NewSave(string name, string directory) {

		name = GetUniqueName(name, directory);
		string NewDirectory = directory.PathJoin(name);
		DirAccess.MakeDirRecursiveAbsolute(NewDirectory);     

		// generate json
        string JSON = Json.Stringify(GenerateSaveTemplateDict(name), indent: "	");
        
		// initialise save file
		Godot.FileAccess SaveFile = Godot.FileAccess.Open(NewDirectory.PathJoin("save.json"), Godot.FileAccess.ModeFlags.WriteRead);
		SaveFile.StoreString(JSON);
		SaveFile.Close();

		GD.Print("[SaveUtils.NewSave] Save successfully created at : " + ProjectSettings.GlobalizePath(NewDirectory));
	}

	public static void OverwriteSave(string saveDirectory, Dictionary newSave) {
		
		// save sessiondata
		if (!newSave.ContainsKey(KeyUnlockedWords)) {
			newSave.Add(KeyUnlockedWords, new Godot.Collections.Array());
		}
		
		newSave[KeyUnlockedWords] = SessionData.UnlockedWords;

        string JSON = Json.Stringify(newSave, indent: "	");

		// initialise save file
		Godot.FileAccess SaveFile = Godot.FileAccess.Open(saveDirectory.PathJoin("save.json"), Godot.FileAccess.ModeFlags.WriteRead);
		SaveFile.StoreString(JSON);
		SaveFile.Close();

		GD.Print("[SaveUtils.OverwriteSave] saved to " + saveDirectory);	
	}

	public static Dictionary LoadSave(string LinkToSaveDirectory) {
	    if (!Directory.Exists(LinkToSaveDirectory)) {
            throw new Exception ("[SaveUtils.LoadSave] Cannot load save at " + LinkToSaveDirectory + " Does not exist!");
        }
		Dictionary Out = (Dictionary) Json.ParseString(File.ReadAllText(LinkToSaveDirectory.PathJoin("save.json")));
		SessionData.LastLoadedSaveDirectory = LinkToSaveDirectory;
		if (!Out.ContainsKey(KeyUnlockedWords)) {
			Out.Add(KeyUnlockedWords, new Godot.Collections.Array());
		}
		SessionData.UnlockedWords = (Godot.Collections.Array) Out[KeyUnlockedWords];

        return Out;
	}


    public static Dictionary LoadSave(string name, string directory) {
        string SavePath = directory.PathJoin(name);
		return LoadSave(SavePath);
    }


}
