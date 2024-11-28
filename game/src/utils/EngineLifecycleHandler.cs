using Godot.Collections;
using Godot;
using utils;
using static DictionaryKeys;
public partial class EngineLifecycleHandler : Node
{
	public override void _Ready()
	{
		// Load settings
		Dictionary Settings = SaveUtils.LoadSettings(Directories.DataPath);
		SessionData.IsFullscreen = (bool)Settings[KeyIsFullscreen];
		Functions.UpdateConfig();
	}

	public override void _Notification(int what)
	{
		if (what == NotificationWMCloseRequest) {
			GetTree().Quit();
			SaveUtils.SaveSettingsFile(Directories.DataPath);
		}
	}
}
