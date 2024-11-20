using System;
using Godot;
using Godot.Collections;
using utils;
using static DictionaryKeys;
[GlobalClass]
public partial class Level : Menu
{
	[Export] public string LevelID = "-999";
	[Export] public Node SavedObjectTree;
	[Export] public Node StaticObjectTree;
	[Export] public Node HiddenObjectTree;
	[Export] public PlayerCharacter PlayerCharacter;

	public PauseMenu PauseMenu;

	protected bool IsImportingData = false;
	protected Dictionary SaveDataToImport;

	public override void _Ready()
	{
		base._Ready();

		if (IsImportingData) ImportData(SaveDataToImport);

		//GD.Print(ExportData());

		if (HiddenObjectTree != null) {
			foreach (Node node in HiddenObjectTree.GetChildren()) {
				if (node is CanvasItem canvasItem) {
					canvasItem.Visible = false;
				}
			}
		}

		PauseMenu = (PauseMenu) ScenesPacked.PAUSE_MENU.Instantiate();
		CanvasLayer.AddChild(PauseMenu);

		PauseMenu.QuitButton.Connect(BaseButton.SignalName.Pressed, Callable.From(OnQuitButtonPressed));
	}

	protected void OnQuitButtonPressed() {
		Dictionary SaveTo = SaveUtils.LoadSave(SessionData.LastLoadedSaveDirectory);
		Dictionary SavedLevels = (Dictionary)SaveTo[KeySavedLevels];
		
		SaveTo[KeyPlayerData] = PlayerCharacter.ExportData();

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

			Dictionary PlayerData = (Dictionary) saveData[KeyPlayerData];
			PlayerCharacter.ImportData(PlayerData);

			Dictionary LevelData = (Dictionary) LevelsDict[(string) saveData[KeyCurrentLevel]];
			Array<Dictionary> LevelObjects = (Array<Dictionary>) LevelData[KeyLevelObjects];

			GD.Print(LevelData);

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
		
		foreach(Node mutableObject in SavedObjects) {
			
			if (mutableObject is Saveable levelObject && levelObject.GetIsSaved()) {
				((Godot.Collections.Array)Data[KeyLevelObjects]).Add(levelObject.ExportData());
			}
		}

		GD.Print("[Level.ExportData] Successfully exported level");
		return Data;
	}
}
