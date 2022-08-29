using System;
using FigureGameObjects.Animations;
using FigureGameObjects.States;
using FigureGameObjects.States.CubeStates;
using GenerationData;
using UnityEngine;

namespace FigureGameObjects
{
	[RequireComponent(typeof(FigurePhysics), typeof(CubeAnimator))]
	public class CubeGameObject : MonoBehaviour, IFigureGameObject
	{
		[SerializeField] private CubeAnimatorParameters cubeAnimatorParameters;
		
		private FigurePhysics _figurePhysics;
		private CubeAnimator _cubeAnimator;
		
		private Cube _cube;
		private CubeStateMachine _cubeStateMachine;
	
		public Cube Cube
		{
			get => _cube;
			private set
			{
				_cube = value;
				transform.position = _cube.StartPosition;
				transform.rotation = _cube.Direction.ToQuaternion();
			}
		}

		private void Awake()
		{
			_figurePhysics = GetComponent<FigurePhysics>();
			_cubeAnimator = new CubeAnimator(transform, cubeAnimatorParameters);
			_figurePhysics.OnCollision += Collide;
		}

		private void Start()
		{
			_cubeStateMachine = new CubeStateMachine(this);
		}

		public void Escape() => _cubeStateMachine.HandleInput(FigureAction.Escape);
		
		public void Collide(IFigureGameObject collideWith) => _cubeStateMachine.HandleInput(FigureAction.Collision);

		public void Initialize(Figure cube)
		{
			Cube = (Cube)cube;
		}

		public void StartIdle()
		{
			_figurePhysics.NowSpeed = 0;
			_figurePhysics.Position = Cube.StartPosition;
		}

		public void PlayShakeAnimation() => _cubeAnimator.PlayShakeAnimation();
		
		public void StartMoveForward()
		{
			_figurePhysics.SetNowSpeedToStart();
			_figurePhysics.StartMoveTo(_cube.StartPosition + _cube.DirectionVector3 * 20);
		}

		public void StartMoveBack()
		{
			_figurePhysics.SetNowSpeedToStart();
			_figurePhysics.StartMoveTo(_cube.StartPosition);
			
			_figurePhysics.OnPositionReach += () => _cubeStateMachine.HandleInput(FigureAction.Idle);
		}
		
		public void StopMove()
		{
			_figurePhysics.StopMove();
		}
	}
}