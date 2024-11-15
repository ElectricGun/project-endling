using Godot;
using System;
using System.IO;

public partial class LoadSaveButton : Button
{
	public String LinkToSaveDirectory;


	public void OnButtonDown() {

		try {
			GD.Print(SaveUtils.LoadSave(LinkToSaveDirectory));
		} catch (Exception e) {
			GD.Print("[LoadSaveButton.OnButtonDown] " + LinkToSaveDirectory );
			GD.Print(e);
		}
	}
}
