using Godot;
using System;
using Godot.Collections;

//unused
public partial class SaveData : GodotObject
{   
    public int Revision;
    public string SaveName;
    public int CurrentLevel;
    public PlayerData PlayerData;
    public Array<LevelData> SavedLevels;   
}
