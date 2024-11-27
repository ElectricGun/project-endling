using System;
using Godot;

[GlobalClass]
public partial class Interactor : Node2D 
{
    [Export] public CharacterBody2D Player;
    [Export] public BaseAIComp InputComp;
    [Export] public Area2D InteractionArea;
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

            foreach (Area2D Area in InteractionArea.GetOverlappingAreas()) {
                if (Area is Interactable interactable && interactable.GetInteractiveObject().IsActive()) {
                    float CurrentDistance = Area.GlobalPosition.DistanceTo(MousePosition);

                    if (CurrentDistance < SmallestDistance) {
                        ClosestInteractable = interactable;
                        SmallestDistance = CurrentDistance;
                    }
                }
            }

            _Interactable = ClosestInteractable;

            Line.ClearPoints();
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