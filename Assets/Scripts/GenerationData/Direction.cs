using System;
using UnityEngine;

namespace GenerationData
{
	public enum Direction
	{
		None = -1,
		Right = 0,
		Up = 1,
		Forward = 2,
		Left = 3,
		Down = 4,
		Back = 5
	}

	public static class DirectionConverter
	{
		public static Vector3Int ToVector(this Direction direction) => direction switch
		{
			Direction.None => Vector3Int.zero,
			Direction.Right => Vector3Int.right,
			Direction.Up => Vector3Int.up,
			Direction.Forward => Vector3Int.forward,
			Direction.Left => Vector3Int.left,
			Direction.Down => Vector3Int.down,
			Direction.Back => Vector3Int.back,
			_ => throw new ArgumentException("Неизвестный Direction"),
		};

		public static Quaternion ToQuaternion(this Direction direction) =>
			Quaternion.LookRotation(direction.ToVector(), Vector3.zero);

		public static int GetAxisIndex(this Direction direction) => (int)direction % 3;
	}
}