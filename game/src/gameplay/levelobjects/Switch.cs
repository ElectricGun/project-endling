using Godot;
using Godot.Collections;

[GlobalClass]
public partial class Switch : InteractiveObject {
	
  [Export] public bool ActivateOnce = false;
  [Export] public InteractiveObject[] Activatables;

    public override void _Ready()
    {
        base._Ready();

    }

	protected void UpdateSprite(bool activated) {
		foreach (Node node in GetChildren()) {
			if (node is Sprite2D sprite2D) {
				sprite2D.FrameCoords = new Vector2I(activated ? 1 : 0, sprite2D.FrameCoords.Y);
			}
		}
	}

    public override bool IsActive() {
		return base.IsActive() && (!Activated && ActivateOnce || !ActivateOnce);
  	}

	public override void ObjectInteract()
	{
		base.ObjectInteract();
		Activated = !Activated;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		UpdateSprite(Activated);

		if (Activatables != null) {
		  foreach (InteractiveObject Activatable in Activatables) {
			if (Activated) Activatable.Activate();
			else Activatable.Deactivate();
		  }
		}
	}
} 
