using Godot;
using System;

[GlobalClass]
public partial class UserControlComp : BaseAIComp
{
    public override void _Process(double delta)
    {
        MoveDirX = (Input.IsActionPressed(InputNames.LEFT) ? -1 : 0) + (Input.IsActionPressed(InputNames.RIGHT) ? 1 : 0);
		IsJumping = Input.IsActionPressed(InputNames.JUMP);
        EmitSignal(SignalName.ToggleRun);
    }

    public bool IsMoving() {
        return MoveDirX != 0;
    }
}
