using Godot;
using System;
using System.Linq;
using Godot.Collections;

[GlobalClass]
[Tool]
public partial class Platform : LevelObject
{
	// Timer used to prevent clipping
	[Export] public bool Enabled = true;
	[Export] public float ActivationTime = 0.2f;
	protected Timer Timer = new();
	protected bool TimerJustStarted = false;			


	public override void _Ready()
	{
		AddChild(Timer);
		Timer.OneShot = true;
	}

	public override void _Process(double delta)
	{
		if (!Engine.IsEditorHint() && Enabled) { 
			

			// disable this functionality, unneeded
			if (false) {
				Array<Node> Children = GetChildren();
				if (GetTree().CurrentScene is Level level) {	

					PlayerCharacter Player = level.PlayerCharacter;

					// deactivate all CollisionShape2D in every CollisionObject2D
					foreach (Node child in Children) {
						if (child is CollisionObject2D collisionObject2D) {
							bool SetDisabled = Player.GlobalPosition.Y > (collisionObject2D.GlobalPosition.Y - Player.GetSize().Y / 2);
							foreach(Node _child in collisionObject2D.GetChildren())
							if (_child is CollisionShape2D collisionShape2D) {
								UpdateCollision2D(collisionShape2D, SetDisabled);

							} else
							if (_child is CollisionPolygon2D collisionPolygon2D) {
								UpdateCollision2D(collisionPolygon2D, SetDisabled);
							}
						}
					}
				}
			}


		}
 	}

	protected void UpdateCollision2D(CollisionShape2D collisionShape2D, bool SetDisabled) {
		if (!SetDisabled && Timer.IsStopped() && collisionShape2D.Disabled && !TimerJustStarted) {
			Timer.Start(ActivationTime);
			TimerJustStarted = true;
		}

		if (!SetDisabled && Timer.IsStopped()) {
			collisionShape2D.Disabled = false;
		} else if (SetDisabled) {
			collisionShape2D.Disabled = true;
			Timer.Stop();
			TimerJustStarted = false;
		}
	}

	protected void UpdateCollision2D(CollisionPolygon2D collisionPolygon2D, bool SetDisabled) {
		if (!SetDisabled && Timer.IsStopped() && collisionPolygon2D.Disabled && !TimerJustStarted) {
			Timer.Start(ActivationTime);
			TimerJustStarted = true;
		}

		if (!SetDisabled && Timer.IsStopped()) {
			collisionPolygon2D.Disabled = false;
		} else if (SetDisabled) {
			collisionPolygon2D.Disabled = true;
			Timer.Stop();
			TimerJustStarted = false;
		}
	}

	public override string[] _GetConfigurationWarnings() {
		var warnings = new System.Collections.Generic.List<string>();

		bool HasCollisions = false;
		int Counter = 0;
		foreach (Node child in GetChildren()) {
			if (child is CollisionObject2D) {
				HasCollisions = true;
				Counter++;
			}
		}

		if (!HasCollisions) {
			warnings.Add("Node must have at least one CollisionObject2D!");
		}

	/*
		if (GetTree().CurrentScene is not Level) {
		warnings.Add("Root node is not of instance Level!");
		}
		*/

		return warnings.Count > 0 ? warnings.ToArray() : System.Array.Empty<string>();
	}

}
