using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GenerationData
{
	[Serializable]
	public class DirectedFigure : Figure
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
		
		public DirectedFigure(FiguresParent parent, Vector3Int coordinatesInFiguresParent) : 
			base(parent, coordinatesInFiguresParent)
		{
			StartPosition = CoordinatesToWorldPosition(parent, coordinatesInFiguresParent);
		}
		
		public Vector3 CoordinatesToWorldPosition(FiguresParent figuresParent, Vector3Int coordinatesInFiguresParent)
		{
			float xCenterCoordinates = - ((float)figuresParent.Length[0] - 1) / 2;
			float yCenterCoordinates = - ((float)figuresParent.Length[1] - 1) / 2;
			float zCenterCoordinates = - ((float)figuresParent.Length[2] - 1) / 2;

			return new Vector3(xCenterCoordinates + coordinatesInFiguresParent.x, 
				yCenterCoordinates + coordinatesInFiguresParent.y,  zCenterCoordinates + coordinatesInFiguresParent.z);
		}

		public override bool SetRandomDirection(params Direction[] notAvailableDirections)
		{
			List<Direction> availableDirections = new List<Direction>()
			{
				Direction.Up, 
				Direction.Right, 
				Direction.Forward, 
				Direction.Down, 
				Direction.Left, 
				Direction.Back, 
			};
			foreach (Direction notAvailableDirection in notAvailableDirections)
			{
				availableDirections.Remove(notAvailableDirection);
			}

			for (int i = availableDirections.Count; i > 0; i--)
			{
				int directionRandomIndex = Random.Range(0, availableDirections.Count);
				Direction = availableDirections[directionRandomIndex];

				try
				{
					Parent.GetFiguresOnFiguresDirecion(this);
					return true;
				}
				catch (ArgumentException e)
				{
					availableDirections.RemoveAt(directionRandomIndex);
				}
			}
			
			Direction = Direction.None;
			return false;
		}

		public override bool SetDifficultRandomDirection(params Direction[] notAvailableDirections)
		{
			List<Direction> undesirableDirections = new List<Direction>(notAvailableDirections);

			void AddIfCubeByCoordinates(Vector3Int chekingCubeCoordinates)
			{
				if (Parent[chekingCubeCoordinates] is DirectedFigure checkingCube)
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
