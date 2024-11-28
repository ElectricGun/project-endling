using System;
using Godot;
using utils;

[GlobalClass]
public partial class InteractiveObject : LevelObject
{   
	[Export] public string PopupName;
	[Export] public string[] LearnedWords;
	[Export] public float InteractionCooldownTime = 5f;
	[Export] public float JumpingLabelGravity = 0f;
	[Export] public float JumpingLabelDrag = 0.1f;
	[Export] public Vector2 JumpingLabelVelocity = new Vector2(0, -1000f);
	[Export] public float JumpingLabelLifetime = 5f;

	[Export] public bool Enabled = true;

	[Signal] public delegate string WordLearnedEventHandler(string word);

	protected Timer _Timer;

	public virtual bool IsActive() {
		return _Timer.IsStopped() && Enabled;
	}

	public override void _Ready()
	{
		base._Ready();
		_Timer = new Timer();
		_Timer.OneShot = true;
		AddChild(_Timer);
	}
	public virtual void ObjectInteract()
	{
		if (_Timer.IsStopped()) {
			_Timer.WaitTime = InteractionCooldownTime;
			_Timer.Start();

			JumpingLabel _JumpingLabel = JumpingLabel.Spawn(this, JumpingLabelVelocity.X, JumpingLabelVelocity.Y, JumpingLabelLifetime, JumpingLabelGravity, JumpingLabelDrag, PopupName);

			_JumpingLabel.GlobalPosition = new Vector2(GlobalPosition.X - _JumpingLabel.GetContentWidth() * 0.5f, GlobalPosition.Y);

			try {
				foreach (string learnWord in LearnedWords) {
					if(SessionData.LearnWord(learnWord)) EmitSignal(SignalName.WordLearned, learnWord);
				}
			} catch (Exception e) {
				GD.Print("[InteractiveObject.ObjectInteract]");
			}
		}
	}
}
