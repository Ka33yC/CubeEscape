#if UNITY_EDITOR

using System;
using GenerationData;
using UnityEngine;

namespace LevelGeneration
{
	public class LevelBuilder : MonoBehaviour
	{
		[SerializeField] private bool isDifficultGeneration;
		[SerializeField, Min(1)] private Vector3Int size;
		[SerializeField] private bool saveDirections;
		[SerializeField] private DirectedFigureForLevelBuilder directedFigurePrefab;

		private DirectedFigureForLevelBuilder[,,] _directedFigures;

		private void Awake()
		{
			Generate();
		}

		public void Generate()
		{
			DestroyExistedFigures();
			_directedFigures = GenerateForEach(size);
		}

		private void DestroyExistedFigures()
		{
			if (_directedFigures == null) return;
			
			foreach (DirectedFigureForLevelBuilder directedFigureForLevelBuilder in _directedFigures)
			{
				Destroy(directedFigureForLevelBuilder.gameObject);
			}
		}

		private DirectedFigureForLevelBuilder[,,] GenerateForEach(Vector3Int sizes)
		{
			DirectedFigureForLevelBuilder[,,] directedFigures =
				new DirectedFigureForLevelBuilder[sizes.x, sizes.y, sizes.z];

			Vector3Int shift = new Vector3Int(sizes.x / 2 + 1, sizes.y / 2 + 1, sizes.z / 2 + 1);
			
			for (int x = 0; x < sizes.x; x++)
			{
				for (int y = 0; y < sizes.y; y++)
				{
					for (int z = 0; z < sizes.z; z++)
					{
						DirectedFigureForLevelBuilder instantiatedFigure = Instantiate(directedFigurePrefab);
						instantiatedFigure.SetParent(transform);
						instantiatedFigure.Position = new Vector3Int(x, y, z) + shift;
						
						directedFigures[x, y, z] = instantiatedFigure;
					}
				}
			}

			return directedFigures;
		}

		public LevelParameters ConstructLevelParameters()
		{
			LevelParameters result = new LevelParameters();
			result.IsDifficultGeneration = isDifficultGeneration;
			result.DirectedFigures = new bool[size.x, size.y, size.z];
			if (saveDirections)
			{
				result.Directions = new Direction[size.x, size.y, size.z];
			}
			
			for (int x = 0; x < size.x; x++)
			{
				for (int y = 0; y < size.y; y++)
				{
					for (int z = 0; z < size.z; z++)
					{
						DirectedFigureForLevelBuilder figure = _directedFigures[x, y, z];
						result.DirectedFigures[x, y, z] = figure.Exist;
						if (!figure.Exist || !saveDirections) continue;
						
						result.Directions[x, y, z] = figure.Direction;
						if (figure.Direction == Direction.None)
						{
							throw new Exception($"Direction.None can't be save at cube ({x}, {y}, {z})");
						}
					}
				}
			}

			return result;
		}
	}
}
#endif