using System;
using Godot;
using Godot.Collections;
public static class Levels 
{
    public static Dictionary<int, PackedScene> LevelMapping = new Dictionary<int, PackedScene>();
    
    static Levels() 
    {
        LevelMapping.Add(-1, GD.Load<PackedScene>("res://game/scenes/worlds/test_world.tscn"));
        LevelMapping.Add(0, GD.Load<PackedScene>("res://game/scenes/worlds/level_1.tscn"));
    }
}