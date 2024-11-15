using Godot;

public partial class Menu : Node2D 
{
    [Export] public CanvasLayer CanvasLayer;
    public TransitionElement TransitionElement;

    private PackedScene TransitionToScene = SceneReferences.MAIN_MENU;
    private bool DoTransition = false;

    public override void _Ready()
    {
        TransitionElement = (TransitionElement) SceneReferences.TRANSITION_ELEMENT.Instantiate();
        TransitionElement.Color = Colours.TransitionColour;
        CanvasLayer.AddChild(TransitionElement);
        TransitionElement.AnimPlayer.AnimationFinished += OnAnimationFinished; 
        TransitionElement.AnimPlayer.Play(TransitionElement.FADE_OUT);
    }

    public void OnAnimationFinished(StringName animationFinished) {
        if (!DoTransition) return;
        GetTree().ChangeSceneToPacked(TransitionToScene);
    }

    public void Transition(PackedScene packedScene) {
        DoTransition = true;
        TransitionToScene = packedScene;
        TransitionElement.AnimPlayer.Play(TransitionElement.FADE_IN);

    }
}