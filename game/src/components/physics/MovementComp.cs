using Godot;
using System;

[GlobalClass]
public partial class MovementComp : BaseComp
{
	[Export] public PhysicsComp PhysicsComponent;
	[Export] BaseAIComp AIComp;
	[Export] public float HorizontalSpeed = 1;
	[Export] public float HorizontalLerpSpeed = 1;
	[Export] public float JumpStrength = 1000; 
	[Export] public int MaxJumps = 2;
	public int JumpCounter {get; protected set;} = 0;
	public bool CanJump {get; protected set;} = true;
	public bool IsAlreadyJumped {get; private set;} = false;
	public bool IsAlreadyMidair  {get; private set;} = false;


	public override void _PhysicsProcess(double delta) 
	{
		// horizontal movement
		float StartVelocityX = PhysicsComponent.PhysicsObject.Velocity.X;
		float TargetVelocityX = AIComp.MoveDirX * HorizontalSpeed;
		float LerpedVelocity = Mathf.Lerp(StartVelocityX, TargetVelocityX, HorizontalLerpSpeed);
		PhysicsComponent.MoveX(LerpedVelocity);

		// jumping 
		if (CanJump && JumpCounter > 0 && AIComp.IsJumping && !IsAlreadyJumped) {
			PhysicsComponent.SetVelocityY(-JumpStrength);
			IsAlreadyJumped = true;
			JumpCounter--;
		} else if (!AIComp.IsJumping) {
			IsAlreadyJumped = false;
		}

		if (PhysicsComponent.IsLanded()) {
			JumpCounter = MaxJumps;
			IsAlreadyJumped = false;
			IsAlreadyMidair = false;
		} else {
			if (!(IsAlreadyMidair || IsAlreadyJumped)) {
				JumpCounter--;
			}
			IsAlreadyMidair = true;
		}

			PrintDebugLabel("MovementComp",
								"JumpCounter: " + JumpCounter + "\n" +
								"AlreadyJumped: " + IsAlreadyJumped + "\n" +
								"AlreadyMidair: " + IsAlreadyMidair);
	}
}
