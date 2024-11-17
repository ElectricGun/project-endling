using System;
using Godot;
using Godot.Collections;

using static DictionaryKeys;
[GlobalClass]
public partial class Level : Menu
{
	[Export] public int LevelID = -999;
	[Export] public Node SavedObjectTree;
	[Export] public Node StaticObjectTree;
	[Export] public Node HiddenObjectTree;
	[Export] public PlayerCharacter PlayerCharacter;

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
	}

	public void QueueImportData(Dictionary saveData) {
		IsImportingData = true;
		SaveDataToImport = saveData;
	}

	

	protected void ImportData(Dictionary saveData) {
		Dictionary LevelsDict = (Dictionary)saveData[KeySavedLevels];

		if (LevelsDict.ContainsKey((string) saveData[KeyCurrentLevel])) {

			Dictionary PlayerData = (Dictionary) saveData[KeyPlayerData];
			PlayerCharacter.GlobalPosition = new Vector2((float) PlayerData[KeyPositionX], (float) PlayerData[KeyPositionY]);
			PlayerCharacter.Velocity = new Vector2((float) PlayerData[KeyVelocityX], (float) PlayerData[KeyVelocityY]);

			Dictionary LevelData = (Dictionary) LevelsDict[(string) saveData[KeyCurrentLevel]];
			Array<Dictionary> LevelObjects = (Array<Dictionary>) LevelData[KeyLevelObjects];

			foreach (Dictionary LevelObjectData in LevelObjects) {
				string NodePath = (string) LevelObjectData[KeyNodePath];

				// import data to objects that are placed by default, otherwise spawn the objects and import data
				if (!string.IsNullOrEmpty(NodePath) && !(bool) LevelObjectData[KeyIsSpawned]) {
					try {
						((LevelObject) GetNode(NodePath)).ImportData(LevelObjectData);
					} catch (Exception e) {
						GD.Print("[Level.ImportData] Error getting node " + NodePath);
						GD.Print(e);
					}

				} else {
					LevelObject Object = (LevelObject) Activator.CreateInstance(Type.GetType((string) LevelObjectData[KeyNodeType]));
					Object.ImportData(LevelObjectData);
					SavedObjectTree.AddChild(Object);
				}
			}
			GD.Print("[Level.ImportData] Successfully imported level");
		} else {
			GD.Print("[Level.ImportData] Level not found in save file, creating...");
		}
	}

	public Dictionary ExportData() {
		Dictionary Data = SaveUtils.GenerateLevelDataTemplateDict();
		Array<Node> SavedObjects = SavedObjectTree.GetChildren();

		foreach(Node mutableObject in SavedObjects) {
			
			if (mutableObject is LevelObject levelObject && levelObject.IsSaved) {
				((Godot.Collections.Array)Data[KeyLevelObjects]).Add(levelObject.ExportData());
			}
		}
		return Data;
	}
}
