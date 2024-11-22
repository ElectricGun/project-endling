using Godot;
using System;

public partial class TransitionButton : Button {
    [Export] public string PathToScene;

    public override void _Ready()
    {
        base._Ready();
        Connect(SignalName.Pressed, Callable.From(Transition));
    }

    private void Transition() {
        if (GetTree().CurrentScene is Menu menu) {
            menu.Transition(GD.Load<PackedScene>(PathToScene));
        }
    } 
}