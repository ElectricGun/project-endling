using Godot;

[GlobalClass]
public partial class ObjectPhysicsComp : BaseComp {
	protected Node ParentObject;
	[Export] public float Gravity = 1;
	[Export] public float DragFactor = 0.1f; 
	[Export] public bool Enabled = true;
	public Vector2 Velocity = new();

	public override void _Ready()
	{
		base._Ready();
		ParentObject = GetParent();
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Velocity = new Vector2(Velocity.X, (float)(Velocity.Y + Gravity * delta));
		Velocity = new Vector2(Mathf.Lerp(Velocity.X, 0, DragFactor), Mathf.Lerp(Velocity.Y, 0, DragFactor));
		
		if (ParentObject is Node2D node2D)
			node2D.GlobalPosition += new Vector2((float)(Velocity.X * delta), (float)(Velocity.Y * delta));
		else if (ParentObject is Control control)
			control.GlobalPosition += new Vector2((float)(Velocity.X * delta), (float)(Velocity.Y * delta));
	}

	public void AddVelocity(float x, float y) {
		Velocity = new Vector2(Velocity.X + x, Velocity.Y + y);
	}

	public void AddVelocity(Vector2 vel) {
		AddVelocity(vel.X, vel.Y);
	}
}
