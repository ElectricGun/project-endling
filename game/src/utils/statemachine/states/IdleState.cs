using System;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class IdleState : State {

	[Export] protected BaseAIComp AIComp;
	[Export] protected MovementComp MovementComponent;
	

	public override void Update(double delta) {
		
		base.Update(delta);
	}

	public override void PhysicsUpdate(double delta) {
		base.PhysicsUpdate(delta);


		MovementComponent.TryMoveX(0);
		MovementComponent.TryJump(AIComp.IsJumping);

		if (AIComp.MoveDirX != 0) Transition(StateNames.WALKING_STATE);
	}
}
