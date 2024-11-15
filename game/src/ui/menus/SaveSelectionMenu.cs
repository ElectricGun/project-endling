using Godot;
using Godot.Collections;
using System;
using static DictionaryKeys;
public partial class SaveSelectionMenu : Menu
{
	[Export] protected Container GridContainer;
	[Export] protected int MaxSavesDisplayed = 9;
	private int SavesDisplayed = 0;
	protected NewSaveButton CurrentNewSaveButton;

	private void AddNewSaveButton() {
		NewSaveButton Button = (NewSaveButton) SceneReferences.NEW_SAVE_BUTTON.Instantiate();
		GridContainer.AddChild(Button);
		Button.Connect(BaseButton.SignalName.Pressed, Callable.From(OnButtonPressed));
		}

	private void AddLoadSaveButton(string PathToSaveFolder) {
		PathToSaveFolder = ProjectSettings.GlobalizePath(PathToSaveFolder);
		LoadSaveButton LoadButton = (LoadSaveButton) SceneReferences.LOAD_SAVE_BUTTON.Instantiate();
		LoadButton.Text = PathToSaveFolder.GetFile();
		LoadButton.SaveLoaded += OnSaveLoaded;
		LoadButton.LinkToSaveDirectory = PathToSaveFolder;
		GridContainer.AddChild(LoadButton);
	}

	public void OnSaveLoaded(Dictionary saveData) {
		GD.Print(saveData);
		
		int CurrentLevelID = (int) saveData[KeyCurrentLevel];
		Level CurrentLevel = (Level) Levels.LevelMapping[CurrentLevelID].Instantiate();
		CurrentLevel.Load(saveData);
		Transition(CurrentLevel);
	}

	public void OnButtonPressed()
	{
		Reset();
	}

	private void Reset() {

		SavesDisplayed = 0;

		foreach (Node Child in GridContainer.GetChildren()) {
			Child.QueueFree();
		}	

		foreach (string SaveFolder in DirAccess.GetDirectoriesAt(Directories.SaveDirGlobal)) {
			AddLoadSaveButton(Directories.SaveDirGlobal.PathJoin(SaveFolder));
			SavesDisplayed++;

			if (SavesDisplayed >= MaxSavesDisplayed) 
			{
				break;
			}
		}

		if (!(SavesDisplayed >= MaxSavesDisplayed)) {
			AddNewSaveButton();
		}
	}
	public override void _Ready()
	{
		base._Ready();
		Reset();
	}
}
