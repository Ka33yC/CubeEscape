using System;
using System.Collections.Generic;
using System.Linq;
using GenerationData;
using UnityEngine;

namespace FigureGameObjects.InfinityLevel
{
	public class InfinityLevelFigureSpawner : FigureSpawner
	{
		protected override void GenerateFigures()
		{
			int x = cubeSize.x, y = cubeSize.y, z = cubeSize.z;
			if (x < 3 || y < 3 || z < 3)
			{
				throw new ArgumentException("For infinity level size must be more than 2");
			}
			
			Figure[,,] figures = new Figure[x, y, z];
			FiguresParent = new FiguresParent(figures, isDifficult);
			Action<Figure> generationAction = isDifficult
				? figure => figure.SetDifficultRandomDirection(GetNotAvailableDirections(figure))
				: figure => figure.SetRandomDirection(GetNotAvailableDirections(figure));
			
			for (int i = 0; i < x; i++)
			{
				for (int j = 0; j < y; j++)
				{
					for (int k = 0; k < z; k++)
					{
						DirectedFigure cube = new DirectedFigure(FiguresParent, new Vector3Int(i, j, k));
						if(!IsOnAnyBoder(cube)) continue;
						
						figures[i, j, k] = cube;
						generationAction(cube);
					}
				}
			}
		}

		private Direction[] GetNotAvailableDirections(Figure figure)
		{
			List<Direction> result = new List<Direction>();
			for (int i = 0; i < 3; i++)
			{
				if (figure.CoordinatesInFiguresParent[i] == 0)
				{
					result.Add((Direction)i);
				}
				else if (figure.CoordinatesInFiguresParent[i] == figure.Parent.Length[i] - 1)
				{
					result.Add((Direction)i + 3);
				}

				if (result.Count == 2) return Array.Empty<Direction>();
			}
			
			return result.ToArray();
		}

		private bool IsOnAnyBoder(Figure figure)
		{
			for (int i = 0; i < 3; i++)
			{
				if (figure.CoordinatesInFiguresParent[i] == 0 ||
				    figure.CoordinatesInFiguresParent[i] == figure.Parent.Length[i] - 1) return true;
			}

			return false;
		}
	}
}