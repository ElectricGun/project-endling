using Godot;
using Godot.Collections;
using System;
using System.IO;

public partial class LoadSaveButton : Button
{
	public string LinkToSaveDirectory;
	public bool active = true;

	[Signal]
	public delegate void SaveLoadedEventHandler(Dictionary saveDict);

	public void OnButtonDown() {
		GD.Print("[LoadSaveButton.OnButtonDown] attempting save load...");
		try {
			EmitSignal("SaveLoaded", SaveUtils.LoadSave(LinkToSaveDirectory));
			GD.Print("[LoadSaveButton.OnButtonDown] save loaded");
		} catch (Exception e) {
			GD.Print("[LoadSaveButton.OnButtonDown] " + LinkToSaveDirectory );
			GD.Print(e);
		}
	}
}
