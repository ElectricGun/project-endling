using Godot;
using Godot.Collections;

[GlobalClass]
public partial class Switch : InteractiveObject {
    
  [Export] public bool ActivateOnce = false;
  [Export] public InteractiveObject[] Activatables;

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

        if (Activatables != null) {
          foreach (InteractiveObject Activatable in Activatables) {
            if (Activated) Activatable.Activate();
            else Activatable.Deactivate();
          }
        }
    }
} 