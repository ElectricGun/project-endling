using Godot;
using static Scenes;

// for instancing purposes
public static class ScenesPacked {
    // buttons
    public static readonly PackedScene LOAD_SAVE_BUTTON = GD.Load<PackedScene>(LINK_TO_LOAD_SAVE_BUTTON);
    public static readonly PackedScene NEW_SAVE_BUTTON = GD.Load<PackedScene>(LINK_TO_NEW_SAVE_BUTTON);

    // menus
    public static readonly PackedScene MAIN_MENU = GD.Load<PackedScene>(LINK_TO_MAIN_MENU);
    public static readonly PackedScene SAVE_SELECTION_MENU = GD.Load<PackedScene>(LINK_TO_SAVE_SELECTION_MENU);
    public static readonly PackedScene OPTIONS_MENU = GD.Load<PackedScene>(LINK_TO_OPTIONS_MENU);

    //overlays
    public static readonly PackedScene PAUSE_MENU = GD.Load<PackedScene>(LINK_TO_PAUSE_MENU);

    // transition
    public static readonly PackedScene TRANSITION_ELEMENT = GD.Load<PackedScene>(LINK_TO_TRANSITION_ELEMENT);
}