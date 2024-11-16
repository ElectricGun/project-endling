using System;
using System.Globalization;
using Godot;
using Godot.Collections;
using static DictionaryKeys;

[GlobalClass]
public partial class LevelObject : Node2D
{
    [Export] public bool IsSpawned {get; private set;} = false;
    [Export] public bool IsSaved {get; private set;} = true;

    public Dictionary ExportData() {
        Dictionary data = SaveUtils.GenerateObjectDataTemplateDict();

        data[KeyIsSpawned] = IsSpawned;
        data[KeyNodePath]  = GetPath().ToString();
        data[KeyNodeType]  = GetType().ToString();
        data[KeyPositionX] = GlobalPosition.X;
        data[KeyPositionY] = GlobalPosition.Y;

        return data;
    }
    
    public void ImportData(Dictionary data) {
        GlobalPosition = new Vector2((float) data[KeyPositionX], (float) data[KeyPositionY]);
        IsSpawned = (bool) data[KeyIsSpawned];
        GD.Print("[LevelObject.ImportData] " + Name + " data imported!");
    }
}