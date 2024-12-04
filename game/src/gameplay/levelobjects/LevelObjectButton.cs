using Godot;
using Godot.Collections;

namespace levelobjects;

[GlobalClass]
public partial class LevelObjectButton : InteractiveObject {
    
  [Export] public InteractiveObject[] Activatables;
  public override void ObjectInteract()
  {
    base.ObjectInteract();
        if (Activatables != null) {
          foreach (InteractiveObject Activatable in Activatables) {
            Activatable.Toggle();
          }
        }
  }
} 