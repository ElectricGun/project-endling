using System;
using Godot;

[GlobalClass]
public partial class Interactor : Node2D 
{
	[Export] public CharacterBody2D Player;
	[Export] public BaseAIComp InputComp;
	[Export] public Area2D InteractionArea;
	[Export] public float InteractionRadius;
	[Export] public Node2D InteractionPopup;
	[Export] public bool Enabled;
	[Export] public bool DebugMode;
	protected Interactable _Interactable = null;
	protected Line2D Line;

	public override void _Ready()
	{
		base._Ready();

		InputComp.Connect(BaseAIComp.SignalName.Interact, Callable.From(Interact));

		Node Anchor = new Node();
		Line = new Line2D();
		AddChild(Anchor);
		Anchor.AddChild(Line);
	}

	protected void DrawLine(Vector2 xy0, Vector2 xy1, float width) {
		Line.DefaultColor = Colours.LineDebugColour;
		Line.Width = width;
		Line.AddPoint(xy0);
		Line.AddPoint(xy1);
	}

	protected void Interact() {
		_Interactable?.GetInteractiveObject().ObjectInteract();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		Vector2 MousePosition = GetGlobalMousePosition();

		if (InteractionArea != null && Enabled) {
			GlobalPosition = MousePosition;
			float SmallestDistance = float.MaxValue;
			Interactable ClosestInteractable = null;
			
			Line.ClearPoints();
			foreach (Area2D Area in InteractionArea.GetOverlappingAreas()) {
				
				if (Area is Interactable interactable && interactable.GetInteractiveObject().IsInteractable()) {
					float CurrentDistance = Area.GlobalPosition.DistanceTo(MousePosition);

					float DistanceToPlayer = Area.GlobalPosition.DistanceTo(Player.GlobalPosition);
					
					if (DebugMode) {
						Vector2 Start = Player.GlobalPosition;
						Vector2 Target = interactable.GetInteractiveObject().GlobalPosition;
						Vector2 Dir = (Target - Start).Normalized();
						Vector2 End = Start + Dir * InteractionRadius;
						DrawLine(Start, End, 20);
					}

					if (DistanceToPlayer > InteractionRadius) continue;

					if (CurrentDistance < SmallestDistance) {
						ClosestInteractable = interactable;
						SmallestDistance = CurrentDistance;
					}
				}
			}

			_Interactable = ClosestInteractable;


			if (ClosestInteractable != null) {
				if (DebugMode) DrawLine(MousePosition, ClosestInteractable.GetInteractiveObject().GlobalPosition, 20);
				InteractionPopup.Visible = true;
				InteractionPopup.GlobalPosition =  ((Node2D) ClosestInteractable).GlobalPosition;

				// codeblock for interaction


			} else {
				InteractionPopup.Visible = false;
			}
		}
	}
}
