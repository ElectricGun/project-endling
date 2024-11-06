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
	
	public void _on_button_button1_up(){
		myLabel.Text = "Starting da game";
	}
	
	public void _on_button_button2_up(){
		myLabel.Text = "opening the options menu";
	}
	
	public void _on_button_button3_up(){
		myLabel.Text = "quitting...";
	}
}
