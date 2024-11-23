using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class RunningState : MovementState {
	public float WalkingTime {get; private set;} = 0f;

	public override void Enter() {
		base.Enter();
		WalkingTime = 0f;
	}

	public override void Update(double delta) {
		base.Update(delta);
	}

	public override void PhysicsUpdate(double delta) {
		base.PhysicsUpdate(delta);

		bool IsWalking = AIComp.MoveDirX != 0;

		MovementComponent.TryMoveX(AIComp.MoveDirX, true);
		MovementComponent.TryJump(AIComp.IsJumping);
		
		if (!MovementComponent.IsAlreadyMidair) {
			if (!IsWalking) Transition(StateNames.IDLE_STATE);
			if (!MovementComponent.IsRunning) Transition(StateNames.WALKING_STATE);
		} else {
			Transition(StateNames.MIDAIR_STATE);
		}
	}
}
