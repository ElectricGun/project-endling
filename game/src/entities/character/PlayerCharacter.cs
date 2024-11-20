using Godot;
using Godot.Collections;

using static DictionaryKeys;

[GlobalClass]
public partial class PlayerCharacter : CharacterBody2D, Saveable
{
	public Vector2 GetSize() {
		return ((RectangleShape2D)((CollisionShape2D)GetNode("CollisionShape2D")).Shape).Size;
	}

	public Dictionary ExportData() {
		Dictionary data = SaveUtils.GenerateObjectDataTemplateDict();

		data[KeyNodePath]  = GetPath().ToString();
		data[KeyNodeType]  = GetType().ToString();
		data[KeyPositionX] = GlobalPosition.X;
		data[KeyPositionY] = GlobalPosition.Y;
		data[KeyVelocityX] = Velocity.X;
		data[KeyVelocityY] = Velocity.Y;

		return data;
	}
	
	public void ImportData(Dictionary levelObjectData) {
		GlobalPosition = new Vector2((float) levelObjectData[KeyPositionX], (float) levelObjectData[KeyPositionY]);
		Velocity = new Vector2((float) levelObjectData[KeyVelocityX], (float) levelObjectData[KeyVelocityY]);
		GD.Print("[PlayerCharacter.ImportData] " + Name + " data imported!");
	}

	public bool GetIsSaved()
	{
		return true;
	}

	public void SetIsSaved(bool saved)
	{
		GD.Print("[PlayerCharacter.SetIsSaved] IsSaved is always true");
	}
}
