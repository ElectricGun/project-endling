using Godot;
using System;

public partial class SaveSelectionMenu : Node2D
{
	[Export] protected GridContainer GridContainer;
	protected NewSaveButton CurrentNewSaveButton;

	private void AddNewSaveButton() {
		NewSaveButton Button = (NewSaveButton) SceneReferences.NEW_SAVE_BUTTON.Instantiate();
		GridContainer.AddChild(Button);
		Button.Connect(BaseButton.SignalName.Pressed, Callable.From(OnButtonPressed));
		}

	private void AddLoadSaveButton(string PathToScene) {
		LoadSaveButton Button = (LoadSaveButton) SceneReferences.LOAD_SAVE_BUTTON.Instantiate();
		Button.Text = PathToScene.GetFile();
		Button.LinkToSaveDirectory = PathToScene;
		GridContainer.AddChild(Button);
	}


	public void OnButtonPressed()
	{
		Reset();
	}

	private void Reset() {

		foreach (Node Child in GridContainer.GetChildren()) {
			Child.QueueFree();
		}

		foreach (string Directory in DirAccess.GetDirectoriesAt(Directories.SaveDir)) {
			AddLoadSaveButton(Directory);
		}

		AddNewSaveButton();
	}
	public override void _Ready()
	{
		Reset();
	}
}
