﻿using System.Collections.Generic;
using UnityEngine;

namespace GenerationData
{
	public class FiguresParent
	{
		private readonly Figure[,,] _figures;

		public FiguresParent(Figure[,,] figures)
		{
			_figures = figures;
		}

		public Figure this[int i, int j, int k] => _figures[i, j, k];

		public int GetFiguresCount(int dimension) => _figures.GetLength(dimension);

		public IEnumerable<Figure> GetFiguresByDirection(Vector3Int startPosition, Direction direction)
		{
			HashSet<Figure> figuresOnDirection = new HashSet<Figure>();
			if (direction == Direction.None) return figuresOnDirection;
		
			int iteratorStartValue = 0, border = 0, addedPerIteration = 0;
			Vector3Int convertedDirection = direction.ToVector();
			Vector3Int shift = startPosition;

			for (int i = 0; i < 3; i++)
			{
				if(convertedDirection[i] == 0) continue;
			
				border = convertedDirection[i] == 1 ? _figures.GetLength(i) : -1;
				addedPerIteration = convertedDirection[i];
				iteratorStartValue = startPosition[i];
			}

			for (int i = iteratorStartValue; i != border; i += addedPerIteration)
			{
				Figure figureOnDirection = _figures[shift.x, shift.y, shift.z];
				if (figureOnDirection != null)
				{
					figuresOnDirection.Add(figureOnDirection);
				}
			
				shift += convertedDirection;
			}

			return figuresOnDirection;
		}

		public IEnumerator<Figure> GetEnumerator()
		{
			foreach (Figure figure in _figures)
			{
				yield return figure;
			}
		}
	}
}