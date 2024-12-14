using Godot;
using utils;

[GlobalClass]
public partial class ColouredObject : InteractiveObject {


	public override void WordPopup()
	{
		if (SessionData.UnlockedWords.Contains("colour")) {
			base.WordPopup();
		} else {
		JumpingLabel _JumpingLabel = JumpingLabel.Spawn(this, JumpingLabelVelocity.X, JumpingLabelVelocity.Y, JumpingLabelLifetime, JumpingLabelGravity, JumpingLabelDrag, "???");
		_JumpingLabel.GlobalPosition = new Vector2(GlobalPosition.X - _JumpingLabel.GetContentWidth() * 0.5f, GlobalPosition.Y);
		_JumpingLabel.Scale *= PopupScale;
		}
	}
}
