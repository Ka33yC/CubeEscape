using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GenerationData
{
	[Serializable]
	public class Cube : Figure
	{
		protected Direction _direction;
		protected Vector3 _directionVector3;

		public Direction Direction
		{
			get => _direction;
			protected set
			{
				_direction = value;
				_directionVector3 = _direction.ToVector();
			}
		}
		
		public Vector3 DirectionVector3 => _directionVector3;
		
		public Cube(FiguresParent parent, Vector3Int startLocalPosition) : 
			base(parent, startLocalPosition)
		{
			
		}

		public override bool SetRandomDirection(params Direction[] notAvailableDirections)
		{
			List<Direction> availableDirections = new List<Direction>()
			{
				Direction.Down, Direction.Left, Direction.Back, Direction.Up, Direction.Right, Direction.Forward, 
			};
		
			foreach (Direction notAvailableDirection in notAvailableDirections)
			{
				availableDirections.Remove(notAvailableDirection);
			}

			for (int i = availableDirections.Count; i > 0; i--)
			{
				Direction = availableDirections[Random.Range(0, availableDirections.Count)];
				HashSet<Figure> checkedFigures = new HashSet<Figure>();
				List<Figure> notCheckedFigures = GetFiguresOnDirection().ToList();
				bool isFindDirection = true;
			
				for (int j = 0; j < notCheckedFigures.Count; j++)
				{
					Figure figureToCheck = notCheckedFigures[j];
					if (figureToCheck == this)
					{
						availableDirections.Remove(Direction);
						isFindDirection = false;
						break;
					}

					if (checkedFigures.Add(figureToCheck))
					{
						notCheckedFigures.AddRange(figureToCheck.GetFiguresOnDirection());
					}
				}

				if(isFindDirection) return true;
			}
			
			Direction = Direction.None;
			return false;
		}

		public override bool SetDifficultRandomDirection(params Direction[] notAvailableDirections)
		{
			List<Direction> undesirableDirections = new List<Direction>(notAvailableDirections);

			void AddIfCubeByCoordinates(Vector3Int chekingCubeCoordinates)
			{
				if (Parent[chekingCubeCoordinates] is Cube checkingCube)
				{
					undesirableDirections.Add(checkingCube._direction);
				}
			}

			for (int i = 0; i < 3; i++)
			{
				if (CoordinatesInFiguresParent[i] + 1 < Parent.Length[i])
				{
					Vector3Int chekingCubeCoordinates = CoordinatesInFiguresParent;
					chekingCubeCoordinates[i]++;

					AddIfCubeByCoordinates(chekingCubeCoordinates);
				}
				if (CoordinatesInFiguresParent[i] - 1 >= 0)
				{
					Vector3Int chekingCubeCoordinates = CoordinatesInFiguresParent;
					chekingCubeCoordinates[i]--;

					AddIfCubeByCoordinates(chekingCubeCoordinates);
				}
			}
			
			if(SetRandomDirection(undesirableDirections.ToArray())) return true;

			return SetRandomDirection(notAvailableDirections);
		}

		public override IEnumerable<Figure> GetFiguresOnDirection() =>
			Parent.GetFiguresByDirection(CoordinatesInFiguresParent + Direction.ToVector(), Direction);
	}
}
