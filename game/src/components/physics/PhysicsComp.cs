using Godot;
using System;
using System.Runtime.CompilerServices;

[GlobalClass]
public partial class PhysicsComp : Node2D
{
	[Export] 
	public CharacterBody2D PhysicsObject {get; private set;}
	[Export] 
	public float Gravity = 1;
	[Export] 
	public Vector2 MaxSpeed = new(0, 0); 
	[Export] public RichTextLabel DebugText;


	public override void _Ready()
	{
	}


	public override void _Process(double delta)
	{
		
	}

	public bool IsLanded() {
		return PhysicsObject.IsOnFloor();
	}

	public void AddVelocity(float X, float Y) {
		Vector2 NewVelocity = PhysicsObject.Velocity;
		NewVelocity.X += X;
		NewVelocity.Y += Y;
		PhysicsObject.Velocity = NewVelocity;
	}

	public void SetVelocityX(float X) {
		Vector2 NewVelocity = PhysicsObject.Velocity;
		NewVelocity.X = X;
		PhysicsObject.Velocity = NewVelocity;
	}

	public void SetVelocityY(float Y) {
		Vector2 NewVelocity = PhysicsObject.Velocity;
		NewVelocity.Y = Y;
		PhysicsObject.Velocity = NewVelocity;
	}

	public void Accelerate(float MoveX, float MoveY, double delta) {
		Vector2 NewVelocity = PhysicsObject.Velocity;
		NewVelocity.X += (float) (MoveX * delta);
		NewVelocity.Y += (float) (MoveY * delta);
		PhysicsObject.Velocity = NewVelocity;
	}

	public void MoveX(float MoveX) {
		Vector2 NewVelocity = PhysicsObject.Velocity;
		NewVelocity.X = MoveX;
		PhysicsObject.Velocity = NewVelocity;
	}

	public void MoveY(float MoveY) {
		Vector2 NewVelocity = PhysicsObject.Velocity;
		NewVelocity.Y = MoveY;
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


		if (DebugText != null) {
			DebugText.Text = "PhysicsComp\n" + 
								"VelocityX: " + PhysicsObject.Velocity.X + "\n" +
								"VelocityY: " + PhysicsObject.Velocity.Y;
		}
	}
}
