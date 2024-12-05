using System;
using System.ComponentModel;
using Godot;
using Godot.Collections;
using utils;
using static DictionaryKeys;
using static ScenesPacked;
[GlobalClass]
public partial class Level : Menu
{
	[Export] public Node RenderedTree;
	[Export] public WorldRenderer WorldRenderer;
	[Export] public Node2D InitialSpawnPoint;
	[Export] public string LevelID {get; set;} = "DEFAULT";
	[Export] public Node SavedObjectTree;
	[Export] public Node StaticObjectTree;
	[Export] public Node HiddenObjectTree;
	[Export] public PlayerCharacter PlayerCharacter;

	//[Export] public ColorRect ScreenGrayscaleRect;
	[Export] public ColorRect ScreenBlurRect;

	public PauseMenu PauseMenu;

	protected bool IsImportingData = false;
	protected Dictionary SaveDataToImport;

	public override void _Ready()
	{
		base._Ready();
		if (!Engine.IsEditorHint()) { 
			
			UpdateShaders();

			WorldRenderer.SetRenderTree(RenderedTree);

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

			foreach (Node node in SavedObjectTree.GetChildren()) {
				if (node is InteractiveObject interactiveObject) {
					interactiveObject.Connect(InteractiveObject.SignalName.WordLearned, Callable.From<string>(NewWordLearned));
				}
			}

			foreach (Node node in StaticObjectTree.GetChildren()) {
				if (node is InteractiveObject interactiveObject) {
					interactiveObject.Connect(InteractiveObject.SignalName.WordLearned, Callable.From<string>(NewWordLearned));
				}
			}
		}
	}

	private void NewWordLearned(string word) {


		if (Vars.SignificantWords.Contains(word)) {
			AnimatedText AnimText = (AnimatedText) ANIMATED_TEXT.Instantiate();
			CanvasLayer.AddChild(AnimText);
			float FadeInOutTime = 1;
			float UpTime = 5;
			AnimText.FadeIn(FadeInOutTime);
			AnimText.RemoveFadeOutDelay(UpTime + FadeInOutTime, FadeInOutTime);
			AnimText.Text = "[center]" + word.Capitalize() + "[/center]";
			AnimText.SetAnchor(Control.LayoutPreset.CenterTop);
			AnimText.PushFontSize(121);
			GD.Print("[Level.NewWordLearned] Significant word " + word);
		} else {
			GD.Print("[Level.NewWordLearned] " + word);
		}
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		UpdateShaders(delta);
	}

	public void UpdateShaders(double delta) {
		// set grayscale based on "colour" word unlock
		//if (ScreenGrayscaleRect.Material is ShaderMaterial shaderMaterial) {
			//float GrayscaleFactor = (float)shaderMaterial.GetShaderParameter("strength");
			//float LerpSpeed = .1f * (float)delta;
			//shaderMaterial.SetShaderParameter("strength", SessionData.UnlockedWords.Contains("colour") ? Mathf.Lerp(GrayscaleFactor, 0, LerpSpeed) : Mathf.Lerp(GrayscaleFactor, 1, LerpSpeed));
		//}
	}


	public void UpdateShaders() {
		// set grayscale based on "colour" word unlock
		//if (ScreenGrayscaleRect.Material is ShaderMaterial shaderMaterial) {
			//shaderMaterial.SetShaderParameter("strength", SessionData.UnlockedWords.Contains("colour") ? 0 : .5);
		//}
	}

	protected void Save() {
		Dictionary SaveTo = SaveUtils.LoadSave(SessionData.LastLoadedSaveDirectory);
		Dictionary SavedLevels = (Dictionary)SaveTo[KeySavedLevels];

		if (SavedLevels.ContainsKey(LevelID)) {
			SavedLevels[LevelID] = ExportData();
		} else {
			SavedLevels.Add(LevelID, ExportData());
		}

		SaveUtils.OverwriteSave(SessionData.LastLoadedSaveDirectory, SaveTo);
	}

	protected void TransitionAndSave(Node node) {
		Save();
		Transition(node);
	}

	protected void OnQuitButtonPressed() {
		Save();

		Transition(ScenesPacked.MAIN_MENU.Instantiate());
	}

	public void QueueImportData(Dictionary saveData) {
		IsImportingData = true;
		SaveDataToImport = saveData;
	}

	protected void ImportData(Dictionary saveData) {

		// import non-level data
		Dictionary LevelsDict = (Dictionary)saveData[KeySavedLevels];
		SessionData.UnlockedWords = (Godot.Collections.Array) saveData[KeyUnlockedWords];

		UpdateShaders();

		// import level data
		if (LevelsDict.ContainsKey((string) saveData[KeyCurrentLevel])) {

			Dictionary LevelData = (Dictionary) LevelsDict[(string) saveData[KeyCurrentLevel]];
			Array<Dictionary> LevelObjects = (Array<Dictionary>) LevelData[KeyLevelObjects];

			// load player data
			if (LevelData.ContainsKey(KeyPlayerData)) {
				Dictionary PlayerData = (Dictionary) LevelData[KeyPlayerData];
				PlayerCharacter.ImportData(PlayerData);

				if (!PlayerData.ContainsKey(KeyPlayerAlreadySpawned) || !(bool) PlayerData[KeyPlayerAlreadySpawned]) {
					SpawnPlayer();
				}
			} else {
				SpawnPlayer();
			}

			foreach (Dictionary LevelObjectData in LevelObjects) {
				string NodePath = (string) LevelObjectData[KeyNodePath];

				// import data to objects that are placed by default, otherwise spawn the objects and import data
				if (!string.IsNullOrEmpty(NodePath) && !(bool) LevelObjectData[KeyIsSpawned]) {
					GD.Print("[Level.ImportData] Importing object " + NodePath);
					try {
						((Saveable) GetNode(NodePath)).ImportData(LevelObjectData);
					} catch (Exception e) {
						GD.Print("[Level.ImportData] Error getting node " + NodePath);
						GD.Print(e);
					}

				} else {
					Node Object = (Node) Activator.CreateInstance(Type.GetType((string) LevelObjectData[KeyNodeType]));
					((Saveable)Object).ImportData(LevelObjectData);
					SavedObjectTree.AddChild(Object);
					GD.Print("[Level.ImportData] Creating object " + Object.GetPath());

				}
			}
			GD.Print("[Level.ImportData] Successfully imported level");
		}  else
		{
			SpawnPlayer();
		}
	}

	private void SpawnPlayer() {
		if (InitialSpawnPoint != null) PlayerCharacter.GlobalPosition = InitialSpawnPoint.GlobalPosition;
		PlayerCharacter.PlayerIsAlreadySpawned = true;
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
