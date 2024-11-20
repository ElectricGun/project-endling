using Godot;
using Godot.Collections;
using System;

using static DictionaryKeys;

[GlobalClass]
public partial class FollowCamera : Camera2D, Saveable
{
	[Export] public Node2D FollowObject;
	[Export] public float LerpSpeed = 0.5f;
	public override void _Process(double delta)
	{	
		Vector2 StartVector = GlobalPosition;
		Vector2 TargetVector = FollowObject.GlobalPosition;
		Vector2 LerpedVector = StartVector.Lerp(TargetVector, LerpSpeed);
		GlobalPosition = LerpedVector;
	}

	public Dictionary ExportData() {
        Dictionary data = SaveUtils.GenerateObjectDataTemplateDict();


        data[KeyNodePath]  = GetPath().ToString();
        data[KeyNodeType]  = GetType().ToString();
        data[KeyPositionX] = GlobalPosition.X;
        data[KeyPositionY] = GlobalPosition.Y;
        data[KeyCameraFollowObject] = FollowObject.GetPath();
        data[KeyCameraLerpSpeed] = LerpSpeed;

        return data;
    }
    
    public void ImportData(Dictionary levelObjectData) {
        GlobalPosition = new Vector2((float) levelObjectData[KeyPositionX], (float) levelObjectData[KeyPositionY]);
        FollowObject = (Node2D) GetNode((string) levelObjectData[KeyCameraFollowObject]);
        LerpSpeed = (float) levelObjectData[KeyCameraLerpSpeed];

        GD.Print("[FollowCamera.ImportData] " + Name + " data imported!");
    }

    public bool GetIsSaved()
    {
        return true;
    }

    public void SetIsSaved(bool saved)
    {
        GD.Print("[FollowCamera.SetIsSaved] IsSaved is always true");
    }
}
