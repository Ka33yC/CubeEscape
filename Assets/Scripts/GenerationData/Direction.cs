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
			Direction.Up => Vector3Int.up,
			Direction.Right => Vector3Int.right,
			Direction.Forward => Vector3Int.forward,
			Direction.Down => Vector3Int.down,
			Direction.Left => Vector3Int.left,
			Direction.Back => Vector3Int.back,
			_ => throw new ArgumentException("Неизвестный Direction"),
		};

		public static Quaternion ToQuaternion(this Direction direction) => direction switch
		{
			Direction.Up => Quaternion.Euler(270,0,0),
			Direction.Right => Quaternion.Euler(90,0,0),
			Direction.Forward => Quaternion.Euler(0,180,0),
			Direction.Down => Quaternion.Euler(90,0,0),
			Direction.Left => Quaternion.Euler(270,0,0),
			Direction.Back => Quaternion.Euler(0,0,0),
			_ => throw new ArgumentException("Неизвестный Direction"),
		};
	}
}