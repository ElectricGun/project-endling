using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class WalkingState : MovementState {

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

		if (Alive) {
			MovementComponent.TryMoveX(AIComp.MoveDirX);
		}		
	}
}
