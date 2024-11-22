using Godot;

[GlobalClass]
public partial class InteractableArea2D : Area2D, Interactable {
    [Export] InteractiveObject _InteractiveObject;

    public InteractiveObject GetInteractiveObject() {
        return _InteractiveObject;
    }

    public void Interact()
    {
        _InteractiveObject.ObjectInteract();
    }
}