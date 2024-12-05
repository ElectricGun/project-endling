using System.Runtime.Serialization.Formatters;
using Godot;
using utils;

[GlobalClass]
public partial class GreyPatch : LevelObject {

    public override void _Process(double delta)
    {
        base._Process(delta);
        foreach (Node node in GetChildren()) {
            if (node is Node2D node2D && node2D.Material is ShaderMaterial shaderMaterial) {
                float GrayscaleFactor = (float)shaderMaterial.GetShaderParameter("strength");
                float LerpSpeed = .1f * (float)delta;
                shaderMaterial.SetShaderParameter("strength", SessionData.UnlockedWords.Contains("colour") ? Mathf.Lerp(GrayscaleFactor, 0, LerpSpeed) : Mathf.Lerp(GrayscaleFactor, 1, LerpSpeed));
            }
        }

    }
}