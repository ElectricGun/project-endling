using Godot;
using System;

public partial class InstantCast2D : RayCast2D
{
	[Signal] public delegate void HitEventHandler(Vector2 HitPosition, Variant HitObject, InstantCast2D Ray);
	[Export] public bool DrawLines = false;
	public Vector2 CollisionPoint;


	public override void _Ready() {
	}

	public override void _Process(double delta) {
		QueueRedraw();
	}

	public override void _Draw() {
		if (DrawLines) {
			DrawLine(Vector2.Zero, TargetPosition, Colors.LawnGreen, 5, true);
			DrawCircle(Vector2.Zero, 5, Colors.Blue);

			if (CollisionPoint.Length() > 0) {
				DrawCircle(CollisionPoint - GlobalPosition, 5, Colors.HotPink);
			}
		}
	}

	public InstantCast2D Cast(Vector2 from, Vector2 to) {
		TargetPosition = to - from;
		Position = from;

		ForceRaycastUpdate();

		if (IsColliding()) {
			var HitObject = GetCollider();
			CollisionPoint = GetCollisionPoint();
			EmitSignal(SignalName.Hit, CollisionPoint, HitObject, this);
			return this;
		} else {
			CollisionPoint = new Vector2(0, 0);
			return this;
		}
	}
}
