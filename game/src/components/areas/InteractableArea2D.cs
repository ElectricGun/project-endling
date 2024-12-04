using Godot;

[GlobalClass]
public partial class InteractableArea2D : Area2D, Interactable {
    InteractiveObject _InteractiveObject;

    public InteractiveObject GetInteractiveObject() {
        return _InteractiveObject;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (GetParent() is InteractiveObject interactiveObject) {
            _InteractiveObject = interactiveObject;
        }
    }

    public void Interact()
    {
        _InteractiveObject.ObjectInteract();
    }
}