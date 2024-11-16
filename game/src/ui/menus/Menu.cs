using Godot;

public partial class Menu : Node2D 
{
	[Export] public CanvasLayer CanvasLayer;
	public TransitionElement TransitionElement;
	private Node TransitionToNode = null;
	private PackedScene TransitionToScene = SceneReferences.MAIN_MENU;
	private bool DoTransition = false;
	private bool DoTransitionNode = false;

	public override void _Ready()
	{
		TransitionElement = (TransitionElement) SceneReferences.TRANSITION_ELEMENT.Instantiate();
		TransitionElement.Color = Colours.TransitionColour;
		CanvasLayer.AddChild(TransitionElement);
		TransitionElement.AnimPlayer.AnimationFinished += OnAnimationFinished; 
		TransitionElement.AnimPlayer.Play(TransitionElement.FADE_OUT);
	}

	public void OnAnimationFinished(StringName animationFinished) {
		if (DoTransition) 
			GetTree().ChangeSceneToPacked(TransitionToScene);
		if (DoTransitionNode) {
			QueueFree();
			GetTree().Root.AddChild(TransitionToNode);
		}
	}

	public void Transition(PackedScene packedScene) {
		DoTransition = true;
		TransitionToScene = packedScene;
		TransitionElement.AnimPlayer.Play(TransitionElement.FADE_IN);
	}

	public void Transition(Node node) {
		DoTransitionNode = true;
		TransitionToNode = node;
		TransitionElement.AnimPlayer.Play(TransitionElement.FADE_IN);

	}
}
