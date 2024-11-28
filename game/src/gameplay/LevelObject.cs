using System;
using System.Globalization;
using Godot;
using Godot.Collections;
using static DictionaryKeys;

[GlobalClass]
public partial class LevelObject : Node2D, Saveable
{
    [Export] public bool IsSpawned {get; private set;} = false;
    [Export] public bool IsSaved {get; private set;} = true;
    public string PathToScene;

    public virtual Dictionary ExportData() {
        Dictionary data = SaveUtils.GenerateObjectDataTemplateDict();

        data[KeyIsSpawned] = IsSpawned;
        data[KeyNodePath]  = GetPath().ToString();
        data[KeyNodeType]  = GetType().ToString();
        data[KeyPositionX] = GlobalPosition.X;
        data[KeyPositionY] = GlobalPosition.Y;

        return data;
    }
    
    public virtual void ImportData(Dictionary levelObjectData) {
        GlobalPosition = new Vector2((float) levelObjectData[KeyPositionX], (float) levelObjectData[KeyPositionY]);
        IsSpawned = (bool) levelObjectData[KeyIsSpawned];
        GD.Print("[LevelObject.ImportData] " + Name + " data imported!");
    }

    public bool GetIsSaved()
    {
        return IsSaved;
    }

    public void SetIsSaved(bool saved)
    {
        IsSaved = saved;
    }
}