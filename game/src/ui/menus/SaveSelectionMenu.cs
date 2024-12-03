using Godot;
using Godot.Collections;
using System;
using static DictionaryKeys;
public partial class SaveSelectionMenu : Menu
{
	[Export] protected Button BackButton;
	[Export] protected Container GridContainer;
	[Export] protected int MaxSavesDisplayed = 9;
	private int SavesDisplayed = 0;
	protected NewSaveButton CurrentNewSaveButton;

	public override void _Ready()
	{
		base._Ready();
		BackButton.Connect(Button.SignalName.Pressed, Callable.From(GoBack));
		Autoloads.GlobalSignals(this).SaveLoaded += OnSaveLoaded;
		Reset();
	}

	private void GoBack() {
		Transition(ScenesPacked.MAIN_MENU.Instantiate());
	}

	private void AddNewSaveButton() {
		NewSaveButton Button = (NewSaveButton) ScenesPacked.NEW_SAVE_BUTTON.Instantiate();
		GridContainer.AddChild(Button);
		Button.Connect(BaseButton.SignalName.Pressed, Callable.From(OnButtonPressed));
	}

	private void AddLoadSaveButton(string PathToSaveFolder) {
		PathToSaveFolder = ProjectSettings.GlobalizePath(PathToSaveFolder);
		LoadSaveButton LoadButton = (LoadSaveButton) ScenesPacked.LOAD_SAVE_BUTTON.Instantiate();
		LoadButton.Text = PathToSaveFolder.GetFile();
		//LoadButton.SaveLoaded += OnSaveLoaded;
		LoadButton.LinkToSaveDirectory = PathToSaveFolder;
		GridContainer.AddChild(LoadButton);
	}

	public void OnSaveLoaded(Dictionary saveData) {		
		string CurrentLevelID = (string) saveData[KeyCurrentLevel];
		Level CurrentLevel = (Level) Levels.LevelMapping[CurrentLevelID].Instantiate();
		CurrentLevel.QueueImportData(saveData);
		Transition(CurrentLevel);
	}

	public void OnButtonPressed()
	{
		Reset();
	}

	public override void OnAnimationFinished(StringName animationFinished) {
		if (DoTransition) 
			GetTree().Root.AddChild(TransitionToScene.Instantiate());
			
		if (DoTransitionNode) {
			QueueFree();
			Autoloads.GlobalSignals(this).SaveLoaded -= OnSaveLoaded;
			GetTree().Root.AddChild(TransitionToNode);
		}
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
}
