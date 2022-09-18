using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace GenerationData
{
	public class FiguresParent
	{
		protected readonly Figure[,,] _figures;
		private readonly Action<Figure> _generationAction;
		
		private int _remaningFigures;
		
		public readonly int[] Length;
		public event Action OnAllFiguresKnocked;

		public FiguresParent(Figure[,,] figures, bool isDifficult)
		{
			_figures = figures;
			
			_generationAction = isDifficult
				? figure => figure.SetDifficultRandomDirection()
				: figure => figure.SetRandomDirection();
			
			Length = new int[]
			{
				_figures.GetLength(0),
				_figures.GetLength(1),
				_figures.GetLength(2),
			};
		}

		protected virtual void OnFigureKnockOut(Figure knockedFigure)
		{
			_remaningFigures--;
			if(_remaningFigures != 0) return;
			
			OnAllFiguresKnocked?.Invoke();
		}

		public Figure this[int x, int y, int z]
		{
			get => _figures[x, y, z]; 
			set => _figures[x, y, z] = value; 
		}

		public Figure this[Vector3Int coordinates]
		{
			get => _figures[coordinates.x, coordinates.y, coordinates.z]; 
			set => _figures[coordinates.x, coordinates.y, coordinates.z] = value; 
		}

		public IEnumerator<Figure> GetEnumerator()
		{
			foreach (Figure figure in _figures)
			{
				yield return figure;
			}
		}

		public HashSet<Figure> GetFiguresByDirection(Vector3Int coordinatesInFiguresParent, Direction direction)
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
				if (figureOnDirection != null && !figureOnDirection.IsKnockedOut)
				{
					figuresOnDirection.Add(figureOnDirection);
				}

				shift += convertedDirection;
			}

			return figuresOnDirection;
		}

		public HashSet<Figure> GetFiguresOnFiguresDirecion(Figure figureToCheck)
		{
			HashSet<Figure> escapeStack = new HashSet<Figure>();
			List<Figure> figuresOnDirection = new List<Figure>();
			if (figureToCheck == null || figureToCheck.IsKnockedOut) return escapeStack;
			
			figuresOnDirection.AddRange(figureToCheck.GetFiguresOnDirection());
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

		public virtual void GenerateFigures()
		{
			for (int i = 0; i < Length[0]; i++)
			{
				for (int j = 0; j < Length[1]; j++)
				{
					for (int k = 0; k < Length[2]; k++)
					{
						DirectedFigure cube = new DirectedFigure(this, new Vector3Int(i, j, k));
						_generationAction(cube);
					}
				}
			}

			SubscribeOnKnockOutChecker();
		}

		protected void SubscribeOnKnockOutChecker()
		{
			foreach (Figure figure in _figures)
			{
				if(figure == null) continue;

				figure.OnKnockOut += OnFigureKnockOut;
				_remaningFigures++;
			}
		}
	}
}