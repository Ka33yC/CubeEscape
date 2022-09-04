using System;
using UnityEngine;

namespace GenerationData
{
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
			Direction.None => Vector3Int.zero,
			Direction.Up => Vector3Int.up,
			Direction.Right => Vector3Int.right,
			Direction.Forward => Vector3Int.forward,
			Direction.Down => Vector3Int.down,
			Direction.Left => Vector3Int.left,
			Direction.Back => Vector3Int.back,
			_ => throw new ArgumentException("Неизвестный Direction"),
		};

		public static Quaternion ToQuaternion(this Direction direction) =>
			Quaternion.LookRotation(direction.ToVector(), Vector3.zero);
	}
}