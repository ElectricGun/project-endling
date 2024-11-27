using Godot;
using System;
using static ScenesPacked;

public partial class JumpingLabel : RichTextLabel
{
	[Export] public ObjectPhysicsComp _ObjectPhysicsComp;
	[Export] public float Time;
	public Timer _Timer;

	public override void _Ready()
	{
		base._Ready();

		_Timer = new Timer();
	}

	public void Init() {
		_Timer.WaitTime = Time;
		_Timer.OneShot = true;
		_Timer.Connect(Timer.SignalName.Timeout, Callable.From(OnTimerTimeout));
		AddChild(_Timer);
		_Timer.Start();
	}

	private void OnTimerTimeout() {
		QueueFree();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
	}
	public static JumpingLabel Spawn(Node node, float vX, float vY, float time = 1, float gravity = 100, float drag = .1f, string text = "" ) {
		JumpingLabel _JumpingLabel = (JumpingLabel) JUMPING_LABEL.Instantiate();
		node.AddChild(_JumpingLabel);
		_JumpingLabel.Text = text;
		_JumpingLabel.Time = time;
		_JumpingLabel._ObjectPhysicsComp.DragFactor = drag;
		_JumpingLabel._ObjectPhysicsComp.Gravity = gravity;
		_JumpingLabel._ObjectPhysicsComp.Velocity = new Vector2(vX, vY);
		_JumpingLabel.Init();

		return _JumpingLabel;
	}
}
