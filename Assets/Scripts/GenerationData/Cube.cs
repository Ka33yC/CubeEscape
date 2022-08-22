using System;
using System.Collections.Generic;
using System.Linq;
using GenerationData.States;
using GenerationData.States.CubeStates;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GenerationData
{
	[Serializable]
	public class Cube : Figure
	{
		protected Direction _direction;
		protected Vector3 _directionVector3;
		protected Vector3 _position;
		
		public readonly FigurePhysics FigurePhysics;
		public readonly CubeStateMachine CubeStateMachine;

		public Vector3 DirectionVector3 => _directionVector3;

		public Vector3 Position
		{
			get => _position;
			set
			{
				_position = value;
				OnPositionChanged?.Invoke(_position);
			}
		}

		public event Action<Vector3> OnPositionChanged;

		public Cube(FiguresParent parent, Vector3Int startLocalPosition, SpeedParameters speedParameters) : 
			base(parent, startLocalPosition)
		{
			FigurePhysics = new FigurePhysics(speedParameters);
			CubeStateMachine = new CubeStateMachine(this);
			
			Position = StartPosition;
		}

		public Direction Direction
		{
			get => _direction;
			protected set
			{
				_direction = value;
				_directionVector3 = _direction.ToVector();
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
		
		public void StartMoveForward()
		{
			FigurePhysics.SetDefaultNowSpeed();
			OnFixedUpdate += DoStepOnForwardDirection;
		}

		private void DoStepOnForwardDirection()
		{
			Position += DirectionVector3 * (FigurePhysics.NowSpeed * Time.fixedDeltaTime);
			FigurePhysics.UpSpeedOnAcceleration();
		}

		public void StopMoveForward()
		{
			OnFixedUpdate -= DoStepOnForwardDirection;
		}

		public void StartMoveBack()
		{
			FigurePhysics.SetDefaultNowSpeed();
			OnFixedUpdate += DoStepOnBackDirection;
		}

		private void DoStepOnBackDirection()
		{
			Vector3 step = DirectionVector3 * (FigurePhysics.NowSpeed * Time.fixedDeltaTime);
			Vector3 newPosition = Position - step;
			float distanceToPosition = (newPosition - StartPosition).magnitude;
			if (distanceToPosition < step.magnitude)
			{
				Position = StartPosition;
				CubeStateMachine.HandleInput(FigureAction.Idle);
				return;
			}

			Position = newPosition;
			FigurePhysics.UpSpeedOnAcceleration();
		}

		public void StopMoveBack()
		{
			OnFixedUpdate -= DoStepOnBackDirection;
		}
	}
}
