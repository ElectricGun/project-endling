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
			{ KeyUnlockedWords, new Godot.Collections.Array()},
			{ KeyFormatRevision, SaveFileRevision },
			{ KeySaveName, saveName },
			{ KeyCurrentLevel, 0 },
			{ KeySavedLevels, Levels }
		};
		
		return Data;
	}

	private static void CreateJSONSaveFile(string directory, string filename, Dictionary data) {


		// generate json
		string JSON = Json.Stringify(data, indent: "	");
		
		// initialise save file
		Godot.FileAccess SaveFile = Godot.FileAccess.Open(directory.PathJoin(filename), Godot.FileAccess.ModeFlags.WriteRead);
		SaveFile.StoreString(JSON);
		SaveFile.Close();

		GD.Print("[SaveUtils.NewSave] Save successfully created at : " + ProjectSettings.GlobalizePath(directory));
	}


	private static void OverwriteJson(string directory, string filename, Dictionary newData) {
		string JSON = Json.Stringify(newData, indent: "	");

		// initialise save file
		Godot.FileAccess SaveFile = Godot.FileAccess.Open(directory.PathJoin(filename), Godot.FileAccess.ModeFlags.WriteRead);
		SaveFile.StoreString(JSON);
		SaveFile.Close();

		GD.Print("[SaveUtils.OverwriteJson] saved to " + directory);	
	}

	public static void NewSave(string directory) {
		NewSave("unnamed_game_save", directory);
	}

	public static void NewSave(string name, string directory) {

		name = GetUniqueName(name, directory);
		string NewDirectory = directory.PathJoin(name);
		DirAccess.MakeDirRecursiveAbsolute(NewDirectory);     

		CreateJSONSaveFile(NewDirectory, "save.json", GenerateSaveTemplateDict(name));
	}

	public static void OverwriteSave(string saveDirectory, Dictionary newSave) {
		
		// save sessiondata
		if (!newSave.ContainsKey(KeyUnlockedWords)) {
			newSave.Add(KeyUnlockedWords, new Godot.Collections.Array());
		}
		
		newSave[KeyUnlockedWords] = SessionData.UnlockedWords;

		string JSON = Json.Stringify(newSave, indent: "	");

		// initialise save file
		// Godot.FileAccess SaveFile = Godot.FileAccess.Open(saveDirectory.PathJoin("save.json"), Godot.FileAccess.ModeFlags.WriteRead);
		// SaveFile.StoreString(JSON);
		// SaveFile.Close();

		OverwriteJson(saveDirectory, "save.json", newSave);

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
		//SessionData.UnlockedWords = (Godot.Collections.Array) Out[KeyUnlockedWords];
		return Out;
	}

	public static Dictionary FetchSettings() {

		Dictionary SettingsDict = new() {
			{KeyIsFullscreen, SessionData.IsFullscreen}
		};
		return SettingsDict;
	}
	public static Dictionary LoadSave(string name, string directory) {
		string SavePath = directory.PathJoin(name);
		return LoadSave(SavePath);
	}

	public static void SaveSettingsFile(string directory) {
		CreateJSONSaveFile(directory, "settings.json", FetchSettings());
	}

	public static Dictionary LoadSettings(string directory) {
		return (Dictionary) Json.ParseString(File.ReadAllText(directory.PathJoin("settings.json")));

	}
}
