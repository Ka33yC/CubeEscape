using System;
using UnityEngine;

public enum Direction
{
	None,
	Up,
	Right,
	Forward,
	Down,
	Left,
	Back
}

public static class DirectionConverter
{
	public static Vector3Int ToVector(this Direction direction) => direction switch
	{
		Direction.Up => Vector3Int.up,
		Direction.Right => Vector3Int.right,
		Direction.Forward => Vector3Int.forward,
		Direction.Down => Vector3Int.down,
		Direction.Left => Vector3Int.left,
		Direction.Back => Vector3Int.back,
		_ => throw new ArgumentException("Неизвестный Direction"),
	};


	public static Direction ToDirection(this Vector3Int direction)
	{
		if (direction == Vector3Int.up) return Direction.Up;
		if (direction == Vector3Int.right) return Direction.Right;
		if (direction == Vector3Int.forward) return Direction.Forward;
		if (direction == Vector3Int.down) return Direction.Down;
		if (direction == Vector3Int.left) return Direction.Left;
		if (direction == Vector3Int.back) return Direction.Back;

		throw new ArgumentException("Неизвестный Direction");
	}
}