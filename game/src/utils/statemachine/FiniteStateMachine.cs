using Godot;
using Godot.Collections;

[GlobalClass]
public partial class FiniteStateMachine : Node {

	// Self explanatory
	[Export] public State InitialState;
	[Export] private RichTextLabel DebugText;
	//
	public State CurrentState;
	// Dictionary list of all children
	public Dictionary States = new();

	public override void _Ready() {
		
		// Define States
		var Children = GetChildren();
		for (int i = 0; i < Children.Count; i++) {
			State Child = (State) Children[i];
			States.Add(Child.Name, Child);
			Child.Transitioned += OnChildTransition;
		}

		if (InitialState != null) {
			InitialState.Enter();
			CurrentState = InitialState;

		}
	}

	public override void _Process(double delta) {
		if (CurrentState != null) {
			CurrentState.Update(delta);
		}
	}

	public override void _PhysicsProcess(double delta) {

		if (CurrentState != null) CurrentState.PhysicsUpdate(delta);
		
		
		if (DebugText != null) DebugText.Text = "State: " + CurrentState.Name;
	}

	// Runs when child emits transition signal
	public void OnChildTransition(State State, string NewStateName, 
								/* Optional */ //string IntialAnimation, 
								/* Optional */ //string Animation, 
								/* Optional */ Dictionary Flags) {
									
		State NewState = (State) States[NewStateName];
		if (State != CurrentState) {
			return;
		}

		if (CurrentState != null) {
			CurrentState.Exit();
		}

		if (Flags != null) {
			NewState.SetFlags(Flags);
		}

		//NewState.TransitionAnimationOverride = IntialAnimation;
		//NewState.AnimationOverride = Animation;
		
		NewState.Enter();

		CurrentState = NewState;
	}
}
