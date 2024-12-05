using Godot;
using System;

public partial class TilemapShaderScript : TileMapLayer, ShaderTransparentable
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

    public void SetAlpha(float alpha)
    {
        if (Material is ShaderMaterial shaderMaterial) {
			shaderMaterial.SetShaderParameter("alpha", alpha);
		}
    }

    public float GetAlpha()
    {
        if (Material is ShaderMaterial shaderMaterial) {
			return (float) shaderMaterial.GetShaderParameter("alpha");
		} else {
			return -1;
		}
    }
}
