using Godot;
using Godot.Collections;

using static DictionaryKeys;

[GlobalClass]
public partial class PlayerCharacter : CharacterBody2D, Saveable
{
	[Export] public CharacterBodyPhysicsComp PhysicsComp {get; private set;}
	[Export] public UserControlComp UserControlComp {get; private set;}
	[Export] public MovementComp MovementComp {get; private set;}
	[Export] public FiniteStateMachine FiniteStateMachine {get; private set;}
	[Export] public Sprite2D Sprite {get; private set;}

	public Vector2 BaseSpriteScale {get; private set;}
	public bool PlayerIsAlreadySpawned;
	public override void _Ready()
	{
		base._Ready();
		BaseSpriteScale = Sprite.Scale;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		if (UserControlComp.IsMoving()) {
			Sprite.Scale = new Vector2(BaseSpriteScale.X * UserControlComp.MoveDirX, BaseSpriteScale.Y);
		} 
	}


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
		data[KeyPlayerAlreadySpawned] = PlayerIsAlreadySpawned;

		return data;
	}
	
	public void ImportData(Dictionary levelObjectData) {
		GlobalPosition = new Vector2((float) levelObjectData[KeyPositionX], (float) levelObjectData[KeyPositionY]);
		Velocity = new Vector2((float) levelObjectData[KeyVelocityX], (float) levelObjectData[KeyVelocityY]);
		PlayerIsAlreadySpawned = (bool) levelObjectData[KeyPlayerAlreadySpawned];
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
