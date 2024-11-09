using Godot;
using System;

[GlobalClass]
public partial class FollowCamera : Camera2D
{
	[Export] public Node2D FollowObject;
	[Export] public float LerpSpeed = 0.5f;
	public override void _Process(double delta)
	{	
		Vector2 StartVector = GlobalPosition;
		Vector2 TargetVector = FollowObject.GlobalPosition;
		Vector2 LerpedVector = StartVector.Lerp(TargetVector, LerpSpeed);
		GlobalPosition = LerpedVector;
	}
}
