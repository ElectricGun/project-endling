using Godot;
using System;

public partial class MainMenu : Node2D
{
	[Export] public Label myLabel;
	public override void _Ready()
	{
		myLabel.Text = "ENDLING";
	}

	public override void _Process(double delta)
	{
	}
	
	public void OnButtonStartUp(){
		myLabel.Text = "Starting da game";
	}
	
	public void OnButtonOptionsUp(){
		myLabel.Text = "opening the options menu";
	}
	
	public void OnButtonQuitUp(){
		myLabel.Text = "quitting...";
	}
}
