using System;
using System.Linq;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class HiddenRoom : LevelObject 
{
    public Area2D Area {get; private set;}
    [Export] public bool ModulateChildren = true;
    [Export] public Node2D[] HiddenObjects;
    [Export] public float RevealingSpeed = 0.1f;
    [Export] public float AlphaTo = 0;
    [Export] public bool PermanentReveal = false;
    public bool AlreadyRevealed {get; private set;} = false;
    protected float setTransparencyValue = 1;

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

    protected void MakeTransparent(Array<Node> objects) {
        foreach (Node _object in objects) {
            if (_object is ShaderTransparentable shaderTransparentable)
                shaderTransparentable.SetAlpha(setTransparencyValue);
            else if (_object is Node2D node2d)
                node2d.Modulate = new Color(node2d.Modulate.R, node2d.Modulate.G, node2d.Modulate.B, setTransparencyValue);
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        float ModulateTarget = 1;

        if (!PermanentReveal) {
            AlreadyRevealed = false;
        }
        
        if (AlreadyRevealed) {
            ModulateTarget = AlphaTo;
        } else {
            foreach (Node2D node2D in Area.GetOverlappingBodies()) {
                if (node2D is PlayerCharacter playerCharacter) {
                    ModulateTarget = AlphaTo;
                    AlreadyRevealed = true;
                    break;
                }
            }
        }
        setTransparencyValue = (float)Mathf.Lerp(setTransparencyValue, ModulateTarget, RevealingSpeed * delta);
        if (HiddenObjects!= null) {
            MakeTransparent(new Array<Node>(HiddenObjects));
        }

        if (ModulateChildren) {
            MakeTransparent(GetChildren());
        }
    }

    public override void ImportData(Dictionary levelObjectData)
    {
        base.ImportData(levelObjectData);
        setTransparencyValue = (float) levelObjectData["setTransparencyValue"];
        AlreadyRevealed = (bool) levelObjectData["alreadyRevealed"];
    }

    public override Dictionary ExportData()
    {
        Dictionary Base = base.ExportData();
        Base["setTransparencyValue"] = setTransparencyValue;
        Base["alreadyRevealed"] = AlreadyRevealed;
        return Base;

    }
}