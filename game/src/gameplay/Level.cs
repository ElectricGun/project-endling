using Godot;
using Godot.Collections;
using static DictionaryKeys;
[GlobalClass]
public partial class Level : Menu
{
	[Export] public Node MutableObjectTree;
	[Export] public Node StaticObjectTree;
	[Export] public PlayerCharacter PlayerCharacter;

	public void Load(Dictionary levelData) {
		Dictionary PlayerData = (Dictionary) levelData[KeyPlayerData];
		PlayerCharacter.GlobalPosition = new Vector2((float) PlayerData[KeyPositionX], (float) PlayerData[KeyPositionY]);
		PlayerCharacter.Velocity = new Vector2((float) PlayerData[KeyVelocityX], (float) PlayerData[KeyVelocityY]);
	}

/* TODO implement saving
	public Dictionary Save() {
		LevelData Out = new LevelData();
		Array<Node> MutableObjects = MutableObjectTree.GetChildren();

		foreach(Node mutableObject in MutableObjects) {
			ObjectData CurrObjectData = new ObjectData();
			
			if (mutableObject is Node2D node2D)
				Position = node2D.GlobalPosition;

			if (mutableObject is CharacterBody2D collisionObject2D)
				CurrObjectData.Velocity = collisionObject2D.Velocity;
			else
				CurrObjectData.Velocity = new Vector2();

			Out.MutableObjects.Add(CurrObjectData);
		}
		return Out;
	}
	*/
}
