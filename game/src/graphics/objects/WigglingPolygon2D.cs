using Godot;

[Tool]
[GlobalClass]
public partial class WigglingPolygon2D : Polygon2D
{
    [Export] public bool Regenerate = true;
    [Export] public bool Enabled = true;
	protected Vector2[] originalPolygon;
	protected float time = 0f;
	[Export] public int Interval = 60;
	protected int ticker = 0;
	[Export] public float WiggleAmount = 1;
	[Export] public float WiggleSpeed = 1f;

	public override void _Ready()
	{
		originalPolygon = (Vector2[])Polygon.Clone();
	}

	public override void _Process(double delta)
	{
        if (Enabled) {
            ticker ++;
            if (ticker % Interval == 0) {
                time += (float)(delta * WiggleSpeed) * Interval;

                Vector2[] newPolygon = new Vector2[originalPolygon.Length];

                for (int i = 0; i < originalPolygon.Length; i++)
                {
                    float wiggleX = Mathf.Sin(time + i) * WiggleAmount;
                    float wiggleY = Mathf.Cos(time + i) * WiggleAmount;
                    newPolygon[i] = originalPolygon[i] + new Vector2(wiggleX, wiggleY);
                }

                Polygon = newPolygon;
            }
        } else {
            if (Regenerate)
                    Polygon = originalPolygon;
            else {
                originalPolygon = (Vector2[])Polygon.Clone();
            }
        }
	}
}
