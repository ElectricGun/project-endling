using System.Collections.Generic;
using Godot;

[Tool]
[GlobalClass]
public partial class WigglingPolygon2D : Polygon2D
{
    [Export] public bool Regenerate = true;
    [Export] public bool Enabled = true;
    [Export] public int Subdivisions = 0;
    [Export] public float Wavelength = 1;
    [Export] public bool PreviewSubdivisions = false;
	protected Vector2[] originalPolygon;
    protected Vector2[] wiggledPolygon;
	protected float time = 0f;
	[Export] public int Interval = 60;
	protected int ticker = 0;
	[Export] public float WiggleAmount = 1;
	[Export] public float WiggleSpeed = 1f;

	public override void _Ready()
	{
		originalPolygon = (Vector2[])Polygon.Clone();
        wiggledPolygon = originalPolygon;
	}

    protected void Wiggle() {
        
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
                    float wiggleX = Mathf.Sin((time + i) / Wavelength) * WiggleAmount;
                    float wiggleY = Mathf.Cos((time + i) / Wavelength) * WiggleAmount;
                    newPolygon[i] = originalPolygon[i] + new Vector2(wiggleX, wiggleY);
                }

                Polygon = wiggledPolygon;
                wiggledPolygon = newPolygon;
            }
        }   
        else {
            if (Regenerate) {
                Polygon = originalPolygon;
            } else {
                originalPolygon = (Vector2[])Polygon.Clone();
            }
        }

        Vector2[] smoothedPolygon;
        
        if (Subdivisions > 0 && PreviewSubdivisions) {
            smoothedPolygon = (Vector2[])wiggledPolygon.Clone();

            for (int h = 0; h < Subdivisions; h++) {
                List<Vector2> smoothedPoints = new List<Vector2>();
                for (int i = 0; i < smoothedPolygon.Length; i++)
                {
                    Vector2 p0 = smoothedPolygon[i];
                    Vector2 p1 = smoothedPolygon[(i + 1) % smoothedPolygon.Length];

                    smoothedPoints.Add(p0.Lerp(p1, 0.25f));
                    smoothedPoints.Add(p0.Lerp(p1, 0.75f));
                }
                smoothedPolygon = smoothedPoints.ToArray();
            }

            Polygon = smoothedPolygon;
        } else if (!Engine.IsEditorHint())  {
            Polygon = wiggledPolygon;
        }
	}
}
