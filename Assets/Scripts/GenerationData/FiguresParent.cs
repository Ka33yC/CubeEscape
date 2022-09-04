using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace GenerationData
{
	public class FiguresParent
	{
		private readonly Figure[,,] _figures;
		public readonly int[] Length;

		public FiguresParent(Figure[,,] figures, bool isDifficult)
		{
			_figures = figures;
			Length = new int[]
			{
				_figures.GetLength(0),
				_figures.GetLength(1),
				_figures.GetLength(2),
			};
		}

		public Figure this[int i, int j, int k] => _figures[i, j, k];

		public Figure this[Vector3Int coordinates] => _figures[coordinates.x, coordinates.y, coordinates.z];

		public IEnumerable<Figure> GetFiguresByDirection(Vector3Int coordinatesInFiguresParent, Direction direction)
		{
			HashSet<Figure> figuresOnDirection = new HashSet<Figure>();
			if (direction == Direction.None) return figuresOnDirection;

			int iteratorStartValue = 0, border = 0, addedPerIteration = 0;
			Vector3Int convertedDirection = direction.ToVector();
			Vector3Int shift = coordinatesInFiguresParent;

			for (int i = 0; i < 3; i++)
			{
				if (convertedDirection[i] == 0) continue;

				border = convertedDirection[i] == 1 ? _figures.GetLength(i) : -1;
				addedPerIteration = convertedDirection[i];
				iteratorStartValue = coordinatesInFiguresParent[i];
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

			return from figure in figuresOnDirection
				where !figure.IsKnockedOut
				select figure;
		}

		public HashSet<Figure> GetFiguresOnFiguresDirecion(Figure figureToCheck)
		{
			HashSet<Figure> escapeStack = new HashSet<Figure>();
			List<Figure> figuresOnDirection = new List<Figure>();
			if (!figureToCheck.IsKnockedOut)
			{
				figuresOnDirection.AddRange(figureToCheck.GetFiguresOnDirection());
			}

			for (int i = 0; i < figuresOnDirection.Count; i++)
			{
				if (figureToCheck == figuresOnDirection[i])
					throw new ArgumentException("Невозможно получить все Figure, т.к. фигура вовзаращется сама в себя");

				figuresOnDirection.AddRange(figuresOnDirection[i].GetFiguresOnDirection());
			}

			figuresOnDirection.Reverse();
			escapeStack.AddRange(figuresOnDirection);

			return escapeStack;
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