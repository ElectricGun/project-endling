using Godot;
using System;

[GlobalClass]
public partial class BaseAIComp : Node
{
	public int MoveDirX {get; protected set;} = 0;
	public int MoveDirY {get; protected set;} = 0;

	public bool IsJumping {get; protected set;}
	[Signal] public delegate void ToggleRunEventHandler();
	[Signal] public delegate void InteractEventHandler();
}
