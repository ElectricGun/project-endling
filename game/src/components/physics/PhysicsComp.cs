using Godot;
using System;

[GlobalClass]
public partial class PhysicsComp : Node2D
{
	[Export] 
	public CharacterBody2D PhysicsObject;
	[Export] 
	public float Gravity = 1;
	[Export] 
	public Vector2 MaxSpeed = new(0, 0); 

	public override void _Ready()
	{
	}


	public override void _Process(double delta)
	{
		
		
	}

	public void Move(float MoveX, float MoveY, double delta) {
		Vector2 NewVelocity = PhysicsObject.Velocity;
		NewVelocity.X += (float) (MoveX * delta);
		NewVelocity.Y += (float) (MoveY * delta);
		PhysicsObject.Velocity = NewVelocity;
	}

	public override void _PhysicsProcess(double delta) 
	{
		//apply gravity to object
		Vector2 NewVelocity = PhysicsObject.Velocity;
		NewVelocity.Y += (float) (Gravity * delta);

		//clamp velocity values
		NewVelocity.X = Math.Clamp(NewVelocity.X, MaxSpeed.X * -1, MaxSpeed.X);
		NewVelocity.Y = Math.Clamp(NewVelocity.Y, MaxSpeed.Y * -1, MaxSpeed.Y);


		PhysicsObject.Velocity = NewVelocity;
		PhysicsObject.MoveAndSlide();
	}
}
