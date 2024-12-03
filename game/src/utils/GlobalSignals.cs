using Godot;
using Godot.Collections;

public partial class GlobalSignals : Node {
	[Signal] public delegate void SaveLoadedEventHandler(Dictionary saveDict);
}
