using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class SlidingDoor : InteractiveObject
{
	[Export] public float OpeningTime = 1f;
	[Export] public float ClosingTime = 1f;
	[Export] public Vector2 OpenedOffset = new();
	[Export] public bool DisableInteractionsWhenOpened = false;
	protected bool AlreadyOpened = false;
	protected bool _QueueOpen = false;
	protected bool _QueueClose = false;
	protected float _QueueToggleTime = 0f;

	protected Timer DoorTimer;

	public Dictionary<Node2D, Vector2> StartPositions {get; private set;} = new();
	public Dictionary<Node2D, Vector2> EndPositions {get; private set;} = new();
	
	public override void _Ready()
	{
		base._Ready();
		DoorTimer = new Timer();
		DoorTimer.OneShot = true;
		AddChild(DoorTimer);

		foreach (Node child in GetChildren()) {
			if (child is Node2D node2D) {
				StartPositions.Add(node2D, node2D.Position);
				EndPositions.Add(node2D, node2D.Position + OpenedOffset);
			}
		}
	}

	public override void _Process(double delta)
	{
		if (_QueueOpen) {
			OpenDoor(_QueueToggleTime);
		} else if (_QueueClose) {
			CloseDoor(_QueueToggleTime);
		}

		GD.Print("" + Activated + AlreadyOpened + !DoorTimer.IsStopped());

		if (Activated) OpenDoor(OpeningTime);
		else CloseDoor(ClosingTime);
	}

	public override bool IsActive()
	{
		return base.IsActive() && (!Activated && DisableInteractionsWhenOpened || !DisableInteractionsWhenOpened); 
	}

	protected void OpenDoor(float openingTime) 
	{
		if (AlreadyOpened || !DoorTimer.IsStopped()) return;
		DoorTimer.WaitTime = openingTime;
		DoorTimer.Start();

		foreach (Node child in GetChildren()) {
			if (child is Node2D node2D) {
				Tween tween = GetTree().CreateTween();
				tween.TweenProperty(node2D, "position", EndPositions[node2D], openingTime); 
			}
		}
		AlreadyOpened = true;
		_QueueOpen = false;
	}

	protected void CloseDoor(float closingTime) 
	{
		if (!AlreadyOpened || !DoorTimer.IsStopped()) return;
		DoorTimer.WaitTime = closingTime;
		DoorTimer.Start();

		foreach (Node child in GetChildren()) {
			if (child is Node2D node2D) {
				Tween tween = GetTree().CreateTween();
				tween.TweenProperty(node2D, "position", StartPositions[node2D], closingTime); 
			}
		}
		_QueueClose = false;
		AlreadyOpened = false;

	}

	public override void ImportData(Dictionary levelObjectData)
	{
		base.ImportData(levelObjectData);

		if (Activated) {
			OpenDoor(0);
		} else if (!Activated) {
			CloseDoor(0);
		}
	}

	public override Dictionary ExportData()
	{
		Dictionary Base = base.ExportData();

		Base.Add("isOpened", Activated);

		return Base;
	}

	public override void ObjectInteract()
	{
		base.ObjectInteract();
	}
}
