using Godot;
using System;

public partial class NewSaveButton : Button
{
	public void OnButtonDown() {
		SaveUtils.NewSave(Directories.SaveDir);
	}
}
