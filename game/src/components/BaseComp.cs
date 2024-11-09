using System;
using Godot;

public partial class BaseComp : Node2D {
    [Export] public RichTextLabel DebugText;

    public void PrintDebugLabel(String title, String text) {
        if (DebugText == null) return;
        DebugText.Text = "[b]" + title + "[/b]" + "\n" + text;
    }
}