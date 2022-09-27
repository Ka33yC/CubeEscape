using System;
using System.Collections.Generic;
using System.Linq;
using LevelGeneration;
using Unity.VisualScripting;
using UnityEngine;

namespace GenerationData
{
	public class FiguresParent
	{
		protected readonly LevelParameters _levelParameters;
		protected readonly Figure[,,] _figures;
		private readonly Action<Figure> _generationAction;
		
		private int _remaningFigures;
		
		public readonly int[] Length;
		public event Action OnAllFiguresKnocked;

		public FiguresParent(LevelParameters levelParameters)
		{
			Length = new int[]
			{
				levelParameters.DirectedFigures.GetLength(0),
				levelParameters.DirectedFigures.GetLength(1),
				levelParameters.DirectedFigures.GetLength(2),
			};

			_figures = new Figure[Length[0], Length[1], Length[2]];
			_generationAction = levelParameters.IsDifficultGeneration
				? figure => figure.SetDifficultRandomDirection()
				: figure => figure.SetRandomDirection();

			_levelParameters = levelParameters;
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
		
		/// <returns>Result not contains figureToCheck</returns>
		public HashSet<Figure> GetFiguresOnFiguresDirecion(Figure figureToCheck)
		{
			HashSet<Figure> checkedFigures = new HashSet<Figure>();
			if (figureToCheck == null || figureToCheck.IsKnockedOut) return checkedFigures;

			var figuresOnDirection = figureToCheck.GetFiguresOnDirection().Reverse();
			foreach (Figure figure in figuresOnDirection)
			{
				GetFiguresOnFiguresDirecion(figure, figureToCheck, checkedFigures);
			}
			
			return checkedFigures;
		}
		
		private void GetFiguresOnFiguresDirecion(Figure figureToCheck, Figure startFigure, HashSet<Figure> checkedFigures)
		{
			if (startFigure == figureToCheck)
			{
				throw new ArgumentException("Вернулись к тому с чего начинали");
			}
			if (checkedFigures.Contains(figureToCheck)) return;

			var figuresOnDirection = figureToCheck.GetFiguresOnDirection().Reverse();
			foreach (Figure figure in figuresOnDirection)
			{
				GetFiguresOnFiguresDirecion(figure, startFigure, checkedFigures);
			}

			checkedFigures.Add(figureToCheck);
		}

		public virtual void GenerateDirectedFigure()
		{
			for (int x = 0; x < Length[0]; x++)
			{
				for (int y = 0; y < Length[1]; y++)
				{
					for (int z = 0; z < Length[2]; z++)
					{
						if(!_levelParameters.DirectedFigures[x,y,z]) continue;
						
						DirectedFigure cube = new DirectedFigure(this, new Vector3Int(x, y, z));
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