using Godot;

public partial class WorldRenderer : CanvasLayer
{
	[Export] public Vector2 RenderScale = new Vector2(1, 1);
	[Export] Camera2D Camera;
	 SubViewport _SubViewport;
	Control _Control;
	public override void _Ready()
	{
		base._Ready();
		_Control = (Control) GetNode("Control");
		_SubViewport = (SubViewport) GetNode("Control/SubViewportContainer/SubViewport");
	}

	public void SetRenderTree(Node tree) {
		tree.Reparent(_SubViewport);
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		_Control.Scale = new Vector2((float)(Camera.Zoom.X * RenderScale.X), (float)(Camera.Zoom.Y * RenderScale.Y));
	}
}
