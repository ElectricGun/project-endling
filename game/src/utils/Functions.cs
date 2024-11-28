using Godot;
using System;
using utils;

public partial class Functions : Node
{
	public static void UpdateConfig () {
		if (SessionData.IsFullscreen)
		{
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		}
		else
		{
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
		}
	}
}
