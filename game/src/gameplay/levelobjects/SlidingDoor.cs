using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class SlidingDoor : InteractiveObject
{
	[Export] public float OpeningTime = 1f;
	[Export] public Vector2 OpenedOffset = new();
	[Export] public bool DisableInteractionsWhenOpened = false;
	public bool IsOpened {get; private set;} = false;
	
	public override void _Ready()
	{
		base._Ready();
	}

	public override void _Process(double delta)
	{
	}

    public override bool IsActive()
    {
        return base.IsActive() && (!IsOpened && DisableInteractionsWhenOpened || !DisableInteractionsWhenOpened); 
    }

    public void OpenDoor() 
	{
		if (IsOpened && IsActive()) return;

		_Timer.WaitTime = OpeningTime;
		foreach (Node child in GetChildren()) {
			if (child is Node2D node2D) {
				Tween tween = GetTree().CreateTween();
				tween.TweenProperty(node2D, "position", node2D.Position + OpenedOffset, OpeningTime); 
			}
		}
		IsOpened = true;
	}

	public void CloseDoor() 
	{
		if (!IsOpened && IsActive()) return;

		foreach (Node child in GetChildren()) {
			if (child is Node2D node2D) {
				Tween tween = GetTree().CreateTween();
				tween.TweenProperty(node2D, "position", node2D.Position - OpenedOffset, OpeningTime); 
			}
		}
		IsOpened = false;
	}

    public override void ImportData(Dictionary levelObjectData)
    {
        base.ImportData(levelObjectData);

		bool temp = (bool) levelObjectData["isOpened"];

		if (temp && !IsOpened) {
			OpenDoor();
		} else if (!temp && IsOpened) {
			CloseDoor();
		}

		IsOpened = temp;
    }

    public override Dictionary ExportData()
    {
		Dictionary Base = base.ExportData();

        Base.Add("isOpened", IsOpened);

		return Base;
    }

    public override void ObjectInteract()
    {
        base.ObjectInteract();
		if (IsOpened) CloseDoor();
		else OpenDoor();
    }
}
