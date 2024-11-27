using Godot;
using utils;

[GlobalClass]
public partial class InteractiveObject : LevelObject
{   
	[Export] public string PopupName;
	[Export] public string[] LearnedWords;
	[Export] public float InteractionCooldownTime = 5f;
	[Export] public float JumpingLabelGravity = 250f;
	[Export] public float JumpingLabelDrag = 0.2f;
	[Export] public Vector2 JumpingLabelVelocity = new Vector2(0, -100);
	[Export] public float JumpingLabelLifetime = 10f;

	[Export] public bool Enabled = true;

    [Signal] public delegate string WordLearnedEventHandler(string word);

	protected Timer _Timer;

	public bool IsActive() {
		return _Timer.IsStopped() && Enabled;
	}

	public override void _Ready()
	{
		base._Ready();
		_Timer = new Timer();
		_Timer.OneShot = true;
		AddChild(_Timer);
	}
	public void ObjectInteract()
	{
		if (_Timer.IsStopped()) {
			_Timer.WaitTime = InteractionCooldownTime;
			_Timer.Start();

			JumpingLabel _JumpingLabel = JumpingLabel.Spawn(this, JumpingLabelVelocity.X, JumpingLabelVelocity.Y, JumpingLabelLifetime, JumpingLabelGravity, JumpingLabelDrag, PopupName);

			_JumpingLabel.GlobalPosition = new Vector2(GlobalPosition.X - _JumpingLabel.GetContentWidth() * 0.5f, GlobalPosition.Y);

			foreach (string learnWord in LearnedWords) {
				if(SessionData.LearnWord(learnWord)) EmitSignal(SignalName.WordLearned, learnWord);
			}
		}
	}
}
