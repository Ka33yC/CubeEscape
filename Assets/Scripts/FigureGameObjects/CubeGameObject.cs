using System;
using FigureGameObjects.States;
using FigureGameObjects.States.CubeStates;
using GenerationData;
using UnityEngine;

namespace FigureGameObjects
{
	[RequireComponent(typeof(Collider), typeof(Rigidbody))]
	public class CubeGameObject : FigureGameObject
	{
		[SerializeField] private SpeedParameters speedParameters;
		
		private Transform _transform;
		private Vector3 _startPosition;
		
		private Cube _cube;
		private FigurePhysics _figurePhysics;
		private CubeStateMachine _cubeStateMachine;

		private event Action OnFixedUpdate;
	
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
			_transform = GetComponent<Transform>();
		}

		private void Start()
		{
			_figurePhysics = new FigurePhysics(speedParameters);
			_cubeStateMachine = new CubeStateMachine(this);
		}

		private void FixedUpdate()
		{
			OnFixedUpdate?.Invoke();
		}
		
		private void OnCollisionEnter(Collision collision)
		{
			FigureGameObject figureGameObject = collision.collider.GetComponent<FigureGameObject>();
			if (figureGameObject == null) return;
		
			_cubeStateMachine.HandleInput(FigureAction.Collision);
		}

		public override void Escape() => _cubeStateMachine.HandleInput(FigureAction.Escape);

		public override void Initialize(Figure cube)
		{
			Cube = (Cube)cube;
		}

		public void StartIdleInBaseState()
		{
			_figurePhysics.NowSpeed = 0;
			_transform.position = Cube.StartPosition;
		}
		
		public void StartMoveForward()
		{
			_figurePhysics.SetNowSpeedToStart();
			OnFixedUpdate += DoStepOnForwardDirection;
		}

		private void DoStepOnForwardDirection()
		{
			_transform.position += Cube.DirectionVector3 * (_figurePhysics.NowSpeed * Time.fixedDeltaTime);
			_figurePhysics.UpSpeedOnAcceleration();
		}

		public void StopMoveForward()
		{
			OnFixedUpdate -= DoStepOnForwardDirection;
		}

		public void StartMoveBack()
		{
			_figurePhysics.SetNowSpeedToStart();
			OnFixedUpdate += DoStepOnBackDirection;
		}

		private void DoStepOnBackDirection()
		{
			Vector3 step = Cube.DirectionVector3 * (_figurePhysics.NowSpeed * Time.fixedDeltaTime);
			Vector3 newPosition = _transform.position - step;
			float distanceToPosition = (newPosition - Cube.StartPosition).magnitude;
			if (distanceToPosition < step.magnitude)
			{
				_cubeStateMachine.HandleInput(FigureAction.Idle);
				return;
			}

			_transform.position = newPosition;
			_figurePhysics.UpSpeedOnAcceleration();
		}

		public void StopMoveBack()
		{
			OnFixedUpdate -= DoStepOnBackDirection;
		}
	}
}