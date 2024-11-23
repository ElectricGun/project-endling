using Godot;
using System;

[GlobalClass]
public partial class MovementComp : BaseComp
{
	//[Export] public BaseAIComp BaseAIComp;
	[Export] public PhysicsComp PhysicsComponent;
	[Export] public float HorizontalSpeed = 1;
	[Export] public float HorizontalSprintSpeed = 1;

	[Export] public float HorizontalLerpSpeed = 1;
	[Export] public float JumpStrength = 1000; 
	[Export] public int MaxJumps = 2;
	public bool IsRunning = false;
	public int JumpCounter {get; protected set;} = 0;
	public bool CanJump {get; protected set;} = true;
	public bool IsAlreadyJumped {get; private set;} = false;
	public bool IsAlreadyMidair  {get; private set;} = false;
	protected bool DoToggleIsRunning = false;

    public override void _PhysicsProcess(double delta) 
	{
		PrintDebugLabel("MovementComp",
							"JumpCounter: " + JumpCounter + "\n" +
							"AlreadyJumped: " + IsAlreadyJumped + "\n" +
							"AlreadyMidair: " + IsAlreadyMidair);
		
		if (DoToggleIsRunning) {
			DoToggleIsRunning = false;
			IsRunning = !IsRunning;
		}
	}

	public override void _Ready() {
		base._Ready();
		//BaseAIComp?.Connect(BaseAIComp.SignalName.ToggleRun, Callable.From(ToggleSprint));
	}

    public void ToggleSprint() {
		DoToggleIsRunning = true;
	}

	public bool TryMoveX(int moveDirX, bool sprint = false) {
		// horizontal movement
		float StartVelocityX = PhysicsComponent.PhysicsObject.Velocity.X;
		float TargetVelocityX = moveDirX * (sprint ? HorizontalSprintSpeed : HorizontalSpeed);
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
