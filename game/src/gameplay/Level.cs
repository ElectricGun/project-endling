using System;
using Godot;
using Godot.Collections;
using utils;
using static DictionaryKeys;
[GlobalClass]
public partial class Level : Menu
{
	[Export] public Node2D InitialSpawnPoint;
	[Export] public string LevelID {get; set;} = "DEFAULT";
	[Export] public Node SavedObjectTree;
	[Export] public Node StaticObjectTree;
	[Export] public Node HiddenObjectTree;
	[Export] public PlayerCharacter PlayerCharacter;

	public PauseMenu PauseMenu;

	protected bool IsImportingData = false;
	protected Dictionary SaveDataToImport;

	public override void _Ready()
	{
		if (!Engine.IsEditorHint()) { 

			base._Ready();

			PauseMenu = (PauseMenu) ScenesPacked.PAUSE_MENU.Instantiate();
			CanvasLayer.AddChild(PauseMenu);

			PauseMenu.QuitButton.Connect(BaseButton.SignalName.Pressed, Callable.From(OnQuitButtonPressed));

			try {
				if (IsImportingData) ImportData(SaveDataToImport);
			} catch (Exception e) {
				GD.Print("[Level._Ready] Error importing save data: \" " + e + " \"");
			}

			//GD.Print(ExportData());

			if (HiddenObjectTree != null) {
				foreach (Node node in HiddenObjectTree.GetChildren()) {
					if (node is CanvasItem canvasItem) {
						canvasItem.Visible = false;
					}
				}
			}
		}
	}

	protected void OnQuitButtonPressed() {
		Dictionary SaveTo = SaveUtils.LoadSave(SessionData.LastLoadedSaveDirectory);
		Dictionary SavedLevels = (Dictionary)SaveTo[KeySavedLevels];

		if (SavedLevels.ContainsKey(LevelID)) {
			SavedLevels[LevelID] = ExportData();
		} else {
			SavedLevels.Add(LevelID, ExportData());
		}

		SaveUtils.OverwriteSave(SessionData.LastLoadedSaveDirectory, SaveTo);
		Transition(ScenesPacked.MAIN_MENU.Instantiate());
	}

	public void QueueImportData(Dictionary saveData) {
		IsImportingData = true;
		SaveDataToImport = saveData;
	}

	protected void ImportData(Dictionary saveData) {
		Dictionary LevelsDict = (Dictionary)saveData[KeySavedLevels];

		if (LevelsDict.ContainsKey((string) saveData[KeyCurrentLevel])) {

			Dictionary LevelData = (Dictionary) LevelsDict[(string) saveData[KeyCurrentLevel]];
			Array<Dictionary> LevelObjects = (Array<Dictionary>) LevelData[KeyLevelObjects];

			// load player data
			if (LevelData.ContainsKey(KeyPlayerData)) {
				Dictionary PlayerData = (Dictionary) LevelData[KeyPlayerData];
				PlayerCharacter.ImportData(PlayerData);
			} else {
				if (InitialSpawnPoint != null) PlayerCharacter.GlobalPosition = InitialSpawnPoint.GlobalPosition;
			}

			foreach (Dictionary LevelObjectData in LevelObjects) {
				string NodePath = (string) LevelObjectData[KeyNodePath];

				// import data to objects that are placed by default, otherwise spawn the objects and import data
				if (!string.IsNullOrEmpty(NodePath) && !(bool) LevelObjectData[KeyIsSpawned]) {
					try {
						((Saveable) GetNode(NodePath)).ImportData(LevelObjectData);
					} catch (Exception e) {
						GD.Print("[Level.ImportData] Error getting node " + NodePath);
						GD.Print(e);
					}
					GD.Print("[Level.ImportData] Imported object " + NodePath);

				} else {
					Node Object = (Node) Activator.CreateInstance(Type.GetType((string) LevelObjectData[KeyNodeType]));
					((Saveable)Object).ImportData(LevelObjectData);
					SavedObjectTree.AddChild(Object);
					GD.Print("[Level.ImportData] Created object " + Object.GetPath());

				}
			}
			GD.Print("[Level.ImportData] Successfully imported level");
		}
	}

	public Dictionary ExportData() {
		Dictionary Data = SaveUtils.GenerateLevelDataTemplateDict();
		Array<Node> SavedObjects = SavedObjectTree.GetChildren();

		Data[KeyPlayerData] = PlayerCharacter.ExportData();
		
		foreach(Node mutableObject in SavedObjects) {
			
			if (mutableObject is Saveable levelObject && levelObject.GetIsSaved()) {
				((Godot.Collections.Array)Data[KeyLevelObjects]).Add(levelObject.ExportData());
			}
		}

		GD.Print("[Level.ExportData] Successfully exported level");
		return Data;
	}

	public override string[] _GetConfigurationWarnings() {
		var warnings = new System.Collections.Generic.List<string>();

		if (LevelID.Equals("DEFAULT")) {
			warnings.Add("LevelID is not set!");
		}

	/*
		if (GetTree().CurrentScene is not Level) {
		warnings.Add("Root node is not of instance Level!");
		}
		*/

		return warnings.Count > 0 ? warnings.ToArray() : System.Array.Empty<string>();
	}
}
