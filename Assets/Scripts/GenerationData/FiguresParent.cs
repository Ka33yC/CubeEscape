using System;
using System.Collections.Generic;
using UnityEngine;

namespace GenerationData
{
	public class FiguresParent
	{
		public readonly Figure[,,] Figures;

		public FiguresParent(Figure[,,] figures)
		{
			Figures = figures;
		}

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
			
				border = convertedDirection[i] == 1 ? Figures.GetLength(i) : -1;
				addedPerIteration = convertedDirection[i];
				iteratorStartValue = startPosition[i] + addedPerIteration;
			}

			for (int i = iteratorStartValue; i != border; i += addedPerIteration)
			{
				Figure figureOnDirection = Figures[shift.x, shift.y, shift.z];
				if (figureOnDirection != null)
				{
					figuresOnDirection.Add(figureOnDirection);
				}
			
				shift += convertedDirection;
			}

			return figuresOnDirection;
		}
	}
}