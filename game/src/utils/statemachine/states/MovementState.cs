using System;
using Godot;
using Godot.Collections;

public partial class MovementState : State {
	[Export] protected BaseAIComp AIComp;
	[Export] protected MovementComp MovementComponent;
	[Export] public CollisionShape2D CollisionShape;
	//[Export] public InstantCast2D WallHeightCast;
	//[Export] public InstantCast2D WallStepCheckCast;
	//[Export] public InstantCast2D ClimbingRaycast;
	/*
	private bool _DoUpdateDirection = true;
	public bool DoUpdateDirection
	{
		get
		{
			return _DoUpdateDirection;
		}
		set
		{
			UserInputComponent.UpdateLastMoveDir = value;
			_DoUpdateDirection = value;
		}
	}
	public bool DoUpdateDrag = true;
	public bool DoUpdateGravity = true;
	*/

	public override void Enter() {
		base.Enter();
		//UserInputComponent.UpdateLastMoveDir = DoUpdateDirection;
	}

	public override void Exit() {
		base.Exit();
	}

    public override void _Ready()
    {
        base._Ready();
		
		//UserInputComponent.UserUnhandledInput += OnUserUnhandeledInput;
    }

	private void OnUserUnhandeledInput(InputEvent input) {
		if (input is InputEventKey inputEventKey) {
			//if (inputEventKey.IsActionPressed()) {

			//}
		}
	}

	public override void Update(double delta) {
		
		base.Update(delta);

	}

	public override void PhysicsUpdate(double delta) {
		
		base.PhysicsUpdate(delta);
	}
}
