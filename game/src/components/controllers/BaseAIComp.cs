using Godot;
using System;

public partial class BaseAIComp : Node
{
	public Vector2 MoveDir {get; protected set;}
	public bool IsJumping {get; protected set;}
}
