using Godot;
using System;

public partial class TransitionElement : ColorRect
{
	[Export] public AnimationPlayer AnimPlayer {get; private set;}
	[Export] public string FADE_IN {get; private set;}= "fade_in";
	[Export] public string FADE_OUT {get; private set;} = "fade_out";

	public override void _Ready()
	{
		base._Ready();
		AnimPlayer.SpeedScale = Vars.TransitionSpeed;
	}
}
