using Godot;

// for instancing purposes
public static class ScenesPacked {
    // buttons
    public static readonly PackedScene LOAD_SAVE_BUTTON = GD.Load<PackedScene>("res://game/scenes/ui/buttons/load_save_button.tscn");
    public static readonly PackedScene NEW_SAVE_BUTTON = GD.Load<PackedScene>("res://game/scenes/ui/buttons/new_save_button.tscn");

    // menus
    public static readonly PackedScene MAIN_MENU = GD.Load<PackedScene>("res://game/scenes/ui/menus/main_menu.tscn");
    public static readonly PackedScene SAVE_SELECTION_MENU = GD.Load<PackedScene>("res://game/scenes/ui/menus/save_selection_menu.tscn");

    //overlays
    public static readonly PackedScene PAUSE_MENU = GD.Load<PackedScene>("res://game/scenes/ui/overlays/pause_menu.tscn");

    // transition
    public static readonly PackedScene TRANSITION_ELEMENT = GD.Load<PackedScene>("res://game/scenes/utils/graphics/transition_element.tscn");
}