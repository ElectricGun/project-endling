using Godot;

public partial class MainMenu : Menu
{	
	[Export] public Button StartButton;
	[Export] public Button SettingsButton;
	[Export] public Button QuitButton;
	public override void _Ready()
	{
		base._Ready();
		StartButton.Connect(Button.SignalName.Pressed, Callable.From(OnButtonStartUp));
		SettingsButton.Connect(Button.SignalName.Pressed, Callable.From(OnButtonOptionsUp));
		QuitButton.Connect(Button.SignalName.Pressed, Callable.From(OnButtonQuitUp));

	}
	
	public void OnButtonStartUp(){
		Transition(ScenesPacked.SAVE_SELECTION_MENU.Instantiate());
	}
	
	public void OnButtonOptionsUp(){
		Transition(ScenesPacked.OPTIONS_MENU.Instantiate());
	}
	
	public void OnButtonQuitUp(){
		GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
	}
}
