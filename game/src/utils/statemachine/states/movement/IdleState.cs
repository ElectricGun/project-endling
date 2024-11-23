using System;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class IdleState : MovementState {

	public override void Update(double delta) {
		
		base.Update(delta);
	}

	public override void PhysicsUpdate(double delta) {
		base.PhysicsUpdate(delta);


		MovementComponent.TryMoveX(0);
		MovementComponent.TryJump(AIComp.IsJumping);

		if (!MovementComponent.IsAlreadyMidair) {
			if (AIComp.MoveDirX != 0) {
				if (!MovementComponent.IsRunning)
					Transition(StateNames.WALKING_STATE);
				else
					Transition(StateNames.RUNNING_STATE);
			}
		} else {
			Transition(StateNames.MIDAIR_STATE);
		}
	}
}
