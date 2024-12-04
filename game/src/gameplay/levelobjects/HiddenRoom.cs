using System;
using Godot;

[GlobalClass]
public partial class HiddenRoom : LevelObject 
{
    public Area2D Area {get; private set;}
    [Export] public Node2D[] HiddenObjects;
    [Export] public float revealingSpeed = 0.1f;

    public override void _Ready()
    {
        base._Ready();
        foreach (Node node in GetChildren()) {
            if (node is Area2D area2D) {
                Area = area2D;
                break;        
            }
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        float ModulateTarget = 1;
        foreach (Node2D node2D in Area.GetOverlappingBodies()) {
            if (node2D is PlayerCharacter playerCharacter) {
                ModulateTarget = 0;
                break;
            }
        }
        Modulate = new Color(Modulate.R, Modulate.G, Modulate.B, (float)Mathf.Lerp(Modulate.A, ModulateTarget, revealingSpeed));
        if (HiddenObjects!= null) {
            foreach (Node2D node2d in HiddenObjects) {
                node2d.Modulate = Modulate;
            }
        }
    }
}