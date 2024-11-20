using Godot;
using System;

public partial class PauseMenu : Control
{
	[Export] public Button ContinueButton;
	[Export] public Button QuitButton;
	protected bool IsPaused = false;

	public override void _Ready()
	{
		base._Ready();
		Resume();
		ContinueButton.Connect(BaseButton.SignalName.Pressed, Callable.From(OnContinueButtonPressed));
		QuitButton.Connect(BaseButton.SignalName.Pressed, Callable.From(OnQuitButtonPressed));
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if (Input.IsActionJustPressed(InputNames.PAUSE)) {
			if (IsPaused) {
				Resume();
			} else {
				Pause();
			}
		}
	}

	

	protected void Resume() {
		GetTree().Paused = false;
		Visible = false;
		IsPaused = false;
	}

	protected void Pause() {
		GetTree().Paused = true;
		Visible = true;
		IsPaused = true;
	}

	public void OnContinueButtonPressed() {
		Resume();
	}

	public void OnQuitButtonPressed() {
		//Resume();
	}

}
