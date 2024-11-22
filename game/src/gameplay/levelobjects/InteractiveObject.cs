using Godot;
using Godot.Collections;

using static DictionaryKeys;

[GlobalClass]
public partial class InteractiveObject : LevelObject
{
    public void ObjectInteract()
    {
        GD.Print("Interacted!!!!!!");
    }
}