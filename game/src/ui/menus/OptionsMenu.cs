using Godot;
using System;
using utils;

public partial class OptionsMenu : Menu
{
	[Export] public Button BackButton;
	public override void _Ready()
	{
		base._Ready();

		BackButton.Connect(Button.SignalName.Pressed, Callable.From(BackToMainMenu));
		
 		var windowSizeDropdown = GetNode<OptionButton>("CanvasLayer/Control/WindowSizeDropDown");
		windowSizeDropdown.AddItem("1920x1080"); // Index 0
		windowSizeDropdown.AddItem("1280x720");  // Index 1
		windowSizeDropdown.AddItem("1024x768");  // Index 2

		// Set the dropdown to the current window size
		Vector2I currentSize = DisplayServer.WindowGetSize();
		if (currentSize == new Vector2I(1920, 1080))
			windowSizeDropdown.Select(0);
		else if (currentSize == new Vector2I(1280, 720))
			windowSizeDropdown.Select(1);
		else if (currentSize == new Vector2I(1024, 768))
			windowSizeDropdown.Select(2);
	}

	private void BackToMainMenu() {
		Transition(ScenesPacked.MAIN_MENU.Instantiate());
	}

	private void _on_window_size_drop_down_item_selected(int index)
	{
		// Adjust the window size based on the selected index
		Vector2I newSize = Vector2I.Zero;

		switch (index)
		{
			case 0: // 1920x1080
				newSize = new Vector2I(1920, 1080);
				break;
			case 1: // 1280x720
				newSize = new Vector2I(1280, 720);
				break;
			case 2: // 1024x768
				newSize = new Vector2I(1024, 768);
				break;
		}

		if (newSize != Vector2I.Zero)
		{
			DisplayServer.WindowSetSize(newSize);
		}
	}
	
	private void _on_full_screen_check_box_toggled(bool isToggled)
	{
		SessionData.IsFullscreen = isToggled;
		
		Functions.UpdateConfig();
	}
	
	private void _on_volume_slider_value_changed(float value)
	{
		//volume slider
		float volumeDb = Mathf.DbToLinear(value);
		AudioServer.SetBusVolumeDb(0, volumeDb);
	}
}
