using Godot;
using System;

[GlobalClass]
public partial class MovementComp : BaseComp
{
	[Export] public PhysicsComp PhysicsComponent;
	[Export] public float HorizontalSpeed = 1;
	[Export] public float HorizontalLerpSpeed = 1;
	[Export] public float JumpStrength = 1000; 
	[Export] public int MaxJumps = 2;
	public bool IsRunning = false;
	public int JumpCounter {get; protected set;} = 0;
	public bool CanJump {get; protected set;} = true;
	public bool IsAlreadyJumped {get; private set;} = false;
	public bool IsAlreadyMidair  {get; private set;} = false;


	public override void _PhysicsProcess(double delta) 
	{
		PrintDebugLabel("MovementComp",
							"JumpCounter: " + JumpCounter + "\n" +
							"AlreadyJumped: " + IsAlreadyJumped + "\n" +
							"AlreadyMidair: " + IsAlreadyMidair);
	}

	public bool TryMoveX(int moveDirX) {
		// horizontal movement
		float StartVelocityX = PhysicsComponent.PhysicsObject.Velocity.X;
		float TargetVelocityX = moveDirX * HorizontalSpeed;
		float LerpedVelocity = Mathf.Lerp(StartVelocityX, TargetVelocityX, HorizontalLerpSpeed);
		PhysicsComponent.MoveX(LerpedVelocity);
		return moveDirX != 0;
	}

	public bool TryJump(bool jumping) {
		bool Out = false;
		// jumping 
		if (CanJump && JumpCounter > 0 && jumping && !IsAlreadyJumped) {
			PhysicsComponent.SetVelocityY(-JumpStrength);
			IsAlreadyJumped = true;
			JumpCounter--;
			Out = true;
			
		} else if (!jumping) {
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
		return Out;
	}
}
