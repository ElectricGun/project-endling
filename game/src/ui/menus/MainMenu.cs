using Godot;
using System;

public partial class MainMenu : Menu
{
	[Export] public Label myLabel;
	public override void _Ready()
	{
		base._Ready();
	}
	
	public void OnButtonStartUp(){
		Transition(ScenesPacked.SAVE_SELECTION_MENU.Instantiate());
	}
	
	public void OnButtonOptionsUp(){
		myLabel.Text = "opening the options menu";
	}
	
	public void OnButtonQuitUp(){
		GetTree().Quit(1);
	}
}
