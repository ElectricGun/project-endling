using Godot;
using utils;

public partial class ColourBarrier : LevelObject {
    public override void _Process(double delta)
    {
        base._Process(delta);
    
        if (SessionData.UnlockedWords.Contains("colour")) {
            QueueFree();
        }
    }
}