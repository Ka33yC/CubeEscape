using System;
using System.Collections.Generic;
using System.Linq;
using ActionStateMachines;
using ActionStateMachines.States;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GenerationData
{
	public class Cube : Figure
	{
		public readonly SpeedParameters SpeedParameters;
		public readonly CubeStateMachine CubeStateMachine = new CubeStateMachine();

		protected Direction _direction;
		protected float _nowSpeed;

		public Vector3 DirectionVector3 { get; protected set; }

		public Cube(FiguresParent parent, Vector3Int coordinates, SpeedParameters speedParameters) : 
			base(parent, coordinates)
		{
			SpeedParameters = speedParameters;
		}

		public Direction Direction
		{
			get => _direction;
			protected set
			{
				_direction = value;
				DirectionVector3 = _direction.ToVector();
			}
		}
		
		public float NowSpeed
		{
			get => _nowSpeed;
			set
			{
				_nowSpeed = value;
				if (_nowSpeed > SpeedParameters.MaxSpeedPerPhysicsFrame)
				{
					_nowSpeed = SpeedParameters.MaxSpeedPerPhysicsFrame;
				}
				else if (_nowSpeed < 0)
				{
					_nowSpeed = 0;
				}
			}
		}

		public override void SetRandomDirection(params Direction[] notAvailableDirections)
		{
			List<Direction> availableDirections = new List<Direction>()
			{
				Direction.Up, Direction.Right, Direction.Forward, Direction.Down, Direction.Left, Direction.Back
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
			
				if(isFindDirection) return;
			}
		}

		public override IEnumerable<Figure> GetFiguresOnDirection() =>
			Parent.GetFiguresByDirection(Position + Direction.ToVector(), Direction);
		
		public void UpSpeedOnAcceleration() => NowSpeed += SpeedParameters.AccelerationPerPhysicsFrame;
		
		public void SetDefaultNowSpeed() => NowSpeed = SpeedParameters.MinSpeedPerPhysicsFrame;
	}
}
