using UnityEngine;

namespace LevelGeneration
{
	public class LevelParameters
	{
		public bool IsDifficultGeneration;
		public bool[,,] DirectedFigures;

		public Vector3Int Size() => new Vector3Int(DirectedFigures.GetLength(0), DirectedFigures.GetLength(1),
			DirectedFigures.GetLength(2));
	}
}