using Godot;
using System;

[GlobalClass]
public partial class UserControlComp : BaseAIComp
{
    public override void _Process(double delta)
    {
        MoveDirX = (Input.IsActionPressed(InputNames.left) ? -1 : 0) +  (Input.IsActionPressed(InputNames.right) ? 1 : 0);
		IsJumping = (Input.IsActionPressed(InputNames.jump));
    }
}
