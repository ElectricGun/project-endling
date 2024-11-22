using Godot;
using System;

[GlobalClass]
public partial class UserControlComp : BaseAIComp
{
    public override void _Process(double delta)
    {
        MoveDirX = (Input.IsActionPressed(InputNames.LEFT) ? -1 : 0) + (Input.IsActionPressed(InputNames.RIGHT) ? 1 : 0);
		IsJumping = Input.IsActionPressed(InputNames.JUMP);
        if (Input.IsActionJustPressed(InputNames.RUN)) EmitSignal(SignalName.ToggleRun);
        if (Input.IsActionJustPressed(InputNames.INTERACT)) EmitSignal(SignalName.Interact);
    }

    public bool IsMoving() {
        return MoveDirX != 0;
    }
}
