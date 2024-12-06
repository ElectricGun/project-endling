using System;
using Godot;
using Godot.Collections;
using utils;

[GlobalClass]
public partial class InteractiveObject : LevelObject
{   
	[Export] public string PopupName;
	[Export] public string[] LearnedWords;
	[Export] public float PopupScale = 1;
	[Export] public float InteractionCooldownTime = 5f;
	[Export] public float JumpingLabelGravity = 0f;
	[Export] public float JumpingLabelDrag = 0.1f;
	[Export] public Vector2 JumpingLabelVelocity = new Vector2(0, -1000f);
	[Export] public float JumpingLabelLifetime = 5f;
	[Export] public bool Enabled = true;
	[Export] public bool Interactable = true;
	[Export] public bool AutoInteract = false;
	[Export] public bool InteractOnce = false;
	public bool Interacted {get; protected set;} = false;
	public bool Activated {get; protected set;} = false;

	[Signal] public delegate string WordLearnedEventHandler(string word);

	protected Timer _Timer;

	public virtual bool IsActive() {
		return _Timer.IsStopped() && Enabled;
	}

	public virtual bool IsInteractable() {
		return IsActive() && Interactable && (!InteractOnce || InteractOnce && !Interacted);
	}

    public virtual void Activate()
    {
		Activated = true;
    }

	public virtual void Deactivate() 
	{
		Activated = false;
	}

	public virtual void Toggle() 
	{
		Activated = !Activated;
	}

	public override void _Ready()
	{
		base._Ready();
		_Timer = new Timer();
		_Timer.OneShot = true;
		AddChild(_Timer);
	}

	public virtual void WordPopup() {
		JumpingLabel _JumpingLabel = JumpingLabel.Spawn(this, JumpingLabelVelocity.X, JumpingLabelVelocity.Y, JumpingLabelLifetime, JumpingLabelGravity, JumpingLabelDrag, PopupName);
		_JumpingLabel.GlobalPosition = new Vector2(GlobalPosition.X - _JumpingLabel.GetContentWidth() * 0.5f, GlobalPosition.Y);
		_JumpingLabel.Scale *= PopupScale;

		try {
			foreach (string learnWord in LearnedWords) {
				if(SessionData.LearnWord(learnWord)) EmitSignal(SignalName.WordLearned, learnWord);
			}
		} catch (Exception e) {
			GD.Print("[InteractiveObject.ObjectInteract] " + e);
		}
	}
	public virtual void ObjectInteract()
	{
		if (_Timer.IsStopped()) {
			_Timer.WaitTime = InteractionCooldownTime;
			_Timer.Start();

			WordPopup();
		}
		Interacted = true;
		
	}

    public override void ImportData(Dictionary levelObjectData)
    {
        base.ImportData(levelObjectData);
		Activated = (bool) levelObjectData["activated"];
		Interacted = (bool) levelObjectData["interacted"];
    }

    public override Dictionary ExportData()
    {
        Dictionary Out = base.ExportData();
		Out["activated"] = Activated;
		Out["interacted"] = Interacted;
		return Out; 
    }
}
