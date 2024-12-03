using Godot;
using Godot.Collections;
using System;

public partial class LoadSaveButton : Button
{
	public string LinkToSaveDirectory;
	public bool active = true;

	[Signal]
	public delegate void SaveLoadedEventHandler(Dictionary saveDict);

	public void OnButtonDown() {
		GD.Print("[LoadSaveButton.OnButtonDown] attempting save load...");
		try {
			Autoloads.GlobalSignals(this).EmitSignal(GlobalSignals.SignalName.SaveLoaded, SaveUtils.LoadSave(LinkToSaveDirectory));
			GD.Print("[LoadSaveButton.OnButtonDown] save loaded");
		} catch (Exception e) {
			GD.Print("[LoadSaveButton.OnButtonDown] " + LinkToSaveDirectory );
			GD.Print(e);
		}
	}
}
