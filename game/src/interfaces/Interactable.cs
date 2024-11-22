using Godot;
using Godot.Collections;

public interface Interactable {
    abstract void Interact();
    abstract InteractiveObject GetInteractiveObject();
}