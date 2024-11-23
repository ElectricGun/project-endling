using System;
using Godot;
using Godot.Collections;

public partial class MovementState : State {

	[Export] protected BaseAIComp AIComp;
	[Export] protected MovementComp MovementComponent;
	[Export] public CollisionShape2D CollisionShape;
	public InstantCast2D InstantCast2D {get; private set;}

	public override void Enter() {
		base.Enter();
		//UserInputComponent.UpdateLastMoveDir = DoUpdateDirection;
	}

	public override void Exit() {
		base.Exit();
	}
	private void OnUserUnhandeledInput(InputEvent input) {
		if (input is InputEventKey inputEventKey) {

		}
	}

	public override void Update(double delta) {
		
		base.Update(delta);

	}

	public override void PhysicsUpdate(double delta) {
		
		base.PhysicsUpdate(delta);
	}

	public override void _Ready()
    {
        base._Ready();

		InstantCast2D = new InstantCast2D();
		AddChild(InstantCast2D);
		
		AIComp.Connect(BaseAIComp.SignalName.ToggleRun, Callable.From(ToggleRun));
    }

	protected void ToggleRun()
    {
		MovementComponent.ToggleSprint();
    }
}
