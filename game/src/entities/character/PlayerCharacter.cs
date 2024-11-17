using Godot;
using System;

[GlobalClass]
public partial class PlayerCharacter : CharacterBody2D
{
	public Vector2 GetSize() {
		return ((RectangleShape2D)((CollisionShape2D)GetNode("CollisionShape2D")).Shape).Size;
	}
}
