using Godot;
using Godot.Collections;
using System;

using static DictionaryKeys;
public partial class State : Node {

	// Time for timer that plays upon entering the state
	[Export] public float TransitionTime = 0;
	// Time for timer that plays constantly while the state is active
	[Export] public float CycleTime = 0;
	// Emit to change state
	[Signal] public delegate void TransitionedEventHandler(State State, string NewState, Dictionary Flags);
	// Unique state flags optionally set via signal
	public Dictionary StateFlags;
	public bool Alive {get; private set;} = true;
	public Timer TransitionTimer = new();
	public Timer CycleTimer = new();

	private bool ForceTransitioning;
	private Dictionary ForceTransitionTo;

	public override void _Ready() {

		AddChild(TransitionTimer);
		AddChild(CycleTimer);

		TransitionTimer.WaitTime = TransitionTime;
		TransitionTimer.OneShot = true;
		CycleTimer.WaitTime = CycleTime;

	}

	// Runs upon entering
	public virtual void Enter() {

		if (TransitionTime > 0 ) TransitionTimer.Start();

	}

	// Set state flags
	public void SetFlags(Dictionary Flags) {
		StateFlags = Flags;
	}

	// Runs upon exit
	public virtual void Exit() {
		ForceTransitionTo = new Dictionary{};
		ForceTransitioning = false;
	}

	// Runs every frame
	public virtual void Update(double delta) {

		AttemptForceTransition();
	}

	// Runs every physics frame
	public virtual void PhysicsUpdate(double delta) {

		AttemptForceTransition();
	}

	// Transition signal func
	public void Transition(string NodeName, Dictionary Flags = null) {

		Kill();
		EmitSignal(SignalName.Transitioned, this, NodeName, Flags);

	}

	// Force transition: if transition fails, keep trying until it works. State is inactive during process.
	public void ForceTransition(string NodeName, Dictionary Flags = null) {

		ForceTransitioning = true;
		ForceTransitionTo = new Dictionary
		{
			{KeyStateNodeName, NodeName},
			{KeyStateFlags, Flags}
		};

		Transition(NodeName, Flags);
	}

	private void AttemptForceTransition() {

		if (!Alive && ForceTransitioning) {
			Transition((string) ForceTransitionTo[KeyStateNodeName], (Dictionary) ForceTransitionTo[KeyStateFlags]);
		}
		
	}

	public void Kill() {
		Alive = false;
	}
}
