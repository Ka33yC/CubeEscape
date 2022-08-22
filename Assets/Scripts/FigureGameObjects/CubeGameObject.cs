using System;
using ActionStateMachines;
using GenerationData;
using UnityEngine;

namespace FigureGameObjects
{
	[RequireComponent(typeof(Collider), typeof(Rigidbody))]
	public class CubeGameObject : FigureGameObject
	{
		private Cube _cube;
		private Transform _transform;
		private Vector3 _startPosition;

		private bool _isEscape;

		public Action OnFixedUpdateCall;
	
		public Cube Cube
		{
			get => _cube;
			private set
			{
				_cube = value;
				_startPosition = Figure.LocalToWorldPosition(_cube.Parent, _cube.Position);
				transform.position = _startPosition;
				transform.rotation = _cube.Direction.ToQuaternion();

				_cube.CubeStateMachine.OnEscapeStart += StartMoveForward;
				_cube.CubeStateMachine.OnEscapeStop += StopMoveForward;
				
				_cube.CubeStateMachine.OnReturnStart += StartMoveBack;
				_cube.CubeStateMachine.OnReturnStop += StopMoveBack;
			}
		}

		private void Awake()
		{
			_transform = GetComponent<Transform>();
		}

		private void FixedUpdate()
		{
			OnFixedUpdateCall?.Invoke();
		}

		private void OnCollisionEnter(Collision collision)
		{
			FigureGameObject figureGameObject = collision.collider.GetComponent<FigureGameObject>();
			if (figureGameObject == null) return;
		
			_cube.CubeStateMachine.HandleInput(FigureAction.Collision);
		}

		public override void Escape()
		{
			_cube.CubeStateMachine.HandleInput(FigureAction.Escape);
		}

		public override void Initialize(Figure cube)
		{
			Cube = (Cube)cube;
		}

		private void StartMoveForward()
		{
			_cube.SetDefaultNowSpeed();
			OnFixedUpdateCall += DoStepOnForwardDirection;
		}

		private void StopMoveForward()
		{
			OnFixedUpdateCall -= DoStepOnForwardDirection;
			_cube.NowSpeed = 0;
		}

		private void DoStepOnForwardDirection()
		{
			_transform.position += _cube.DirectionVector3 * Cube.NowSpeed;
			_cube.UpSpeedOnAcceleration();
		}

		private void StartMoveBack()
		{
			_cube.SetDefaultNowSpeed();
			OnFixedUpdateCall += DoStepOnBackDirection;
		}

		private void StopMoveBack()
		{
			OnFixedUpdateCall -= DoStepOnBackDirection;
			_cube.NowSpeed = 0;
		}

		private void DoStepOnBackDirection()
		{
			Vector3 step = _cube.DirectionVector3 * Cube.NowSpeed;
			Vector3 newPosition = _transform.position - step;
			float distanceToPosition = (newPosition - _startPosition).magnitude;
			if (distanceToPosition < step.magnitude)
			{
				_transform.position = _startPosition;
				_cube.CubeStateMachine.HandleInput(FigureAction.Idle);
				return;
			}

			_transform.position = newPosition;
			_cube.UpSpeedOnAcceleration();
		}
	}
}