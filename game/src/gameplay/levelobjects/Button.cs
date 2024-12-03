using Godot;
using Godot.Collections;

namespace levelobjects;

[GlobalClass]
public partial class Button : InteractiveObject {
    
  [Export] public InteractiveObject[] Activatables;
  public override void ObjectInteract()
  {
    base.ObjectInteract();
  }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Activatables != null) {
          foreach (InteractiveObject Activatable in Activatables) {
            Activatable.Toggle();
          }
        }
    }
} 