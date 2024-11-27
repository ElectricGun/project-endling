using Godot;
using System;

public partial class AnimatedText : RichTextLabel
{
	[Export] AnimationPlayer AnimPlayer;
	private bool DoRemove = false;
	private float fadeOutRemoveSpeed;

	public override void _Ready()
	{
		base._Ready();
		AnimPlayer.AnimationFinished += Remove; 
		SelfModulate = new Color(1, 1, 1, 0);
	}

	public void FadeIn(float speed) {
		AnimPlayer.SpeedScale = speed;
		AnimPlayer.Play("fade_in");
	}

	public void FadeOut(float speed) {
		AnimPlayer.SpeedScale = speed;
		AnimPlayer.Play("fade_out");
	}

	public void FadeOutRemove(float speed) {
		FadeOut(speed);
		DoRemove = true;
	}

	public void RemoveFadeOutDelay(float time, float speed) {
		Timer _Timer = new Timer();
		_Timer.OneShot = true;
		AddChild(_Timer);
		_Timer.WaitTime = time;
		fadeOutRemoveSpeed = speed;
		_Timer.Connect(Timer.SignalName.Timeout, Callable.From(fadeOutRemoveTimer));
		_Timer.Start();
	}

	private void fadeOutRemoveTimer () {
		FadeOutRemove(fadeOutRemoveSpeed);
	}

	private void Remove(StringName animationFinished) {
		if (DoRemove) {
			QueueFree();
			GD.Print("[AnimatedText.Remove]");
		}
	}

	internal void SetAnchor(LayoutPreset centerTop)
	{
		throw new NotImplementedException();
	}
}
