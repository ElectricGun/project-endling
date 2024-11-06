using Godot;
using System;

[GlobalClass]
public partial class MovementComp : Node2D
{
	[Export] 
	public PhysicsComp PhysicsComponent;
	[Export] BaseAIComp AIComp;
	[Export] 
	public float HorizontalSpeed = 1;
	[Export]
	public float JumpStrength = 1; 

	private bool CanJump = true;

	public override void _PhysicsProcess(double delta) 
	{
		//horizontal movement
		PhysicsComponent.Move(AIComp.MoveDir.X * HorizontalSpeed, 0, delta);
	}
}
