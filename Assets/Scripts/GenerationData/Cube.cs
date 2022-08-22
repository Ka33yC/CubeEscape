using System;
using System.Collections.Generic;
using System.Linq;
using GenerationData.States;
using GenerationData.States.CubeStates;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace GenerationData
{
	[Serializable]
	public class Cube : Figure
	{
		public readonly FigurePhysics FigurePhysics;
		public readonly CubeStateMachine CubeStateMachine = new CubeStateMachine();

		protected Direction _direction;

		public Vector3 DirectionVector3 { get; protected set; }

		public Cube(FiguresParent parent, Vector3Int startLocalPosition, SpeedParameters speedParameters) : 
			base(parent, startLocalPosition)
		{
			FigurePhysics = new FigurePhysics(speedParameters);
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
			Parent.GetFiguresByDirection(CoordinatesInFiguresParent + Direction.ToVector(), Direction);
	}
}
