using System;
using GenerationData;
using GenerationData.States;
using UnityEngine;

namespace FigureGameObjects
{
	[RequireComponent(typeof(Collider), typeof(Rigidbody))]
	public class CubeGameObject : FigureGameObject
	{
		private Cube _cube;
		private Transform _transform;
		private Vector3 _startPosition;

		public Action OnFixedUpdate;
	
		public Cube Cube
		{
			get => _cube;
			private set
			{
				_cube = value;
				transform.position = value.StartWorldPosition;
				transform.rotation = _cube.Direction.ToQuaternion();

			}
		}

		private void Awake()
		{
			_transform = GetComponent<Transform>();
		}

		private void FixedUpdate()
		{
			OnFixedUpdate?.Invoke();
		}

		private void OnCollisionEnter(Collision collision)
		{
			FigureGameObject figureGameObject = collision.collider.GetComponent<FigureGameObject>();
			if (figureGameObject == null) return;
		
			_cube.CubeStateMachine.HandleInput(FigureAction.Collision);
		}

		public override void Escape() => _cube.CubeStateMachine.HandleInput(FigureAction.Escape);

		public override void Initialize(Figure cube)
		{
			Cube = (Cube)cube;
			
			Cube.CubeStateMachine.OnIdleStart += () => Cube.FigurePhysics.NowSpeed = 0;
			
			Cube.CubeStateMachine.OnEscapeStart += StartMoveForward;
			Cube.CubeStateMachine.OnEscapeStop += StopMoveForward;
				
			Cube.CubeStateMachine.OnReturnStart += StartMoveBack;
			Cube.CubeStateMachine.OnReturnStop += StopMoveBack;
		}

		private void StartMoveForward()
		{
			Cube.FigurePhysics.SetDefaultNowSpeed();
			OnFixedUpdate += DoStepOnForwardDirection;
		}

		private void StopMoveForward()
		{
			OnFixedUpdate -= DoStepOnForwardDirection;
		}

		private void DoStepOnForwardDirection()
		{
			_transform.position += Cube.DirectionVector3 * (Cube.FigurePhysics.NowSpeed * Time.fixedDeltaTime);
			Cube.FigurePhysics.UpSpeedOnAcceleration();
		}

		private void StartMoveBack()
		{
			Cube.FigurePhysics.SetDefaultNowSpeed();
			OnFixedUpdate += DoStepOnBackDirection;
		}

		private void StopMoveBack()
		{
			OnFixedUpdate -= DoStepOnBackDirection;
		}

		private void DoStepOnBackDirection()
		{
			Vector3 step = Cube.DirectionVector3 * (Cube.FigurePhysics.NowSpeed * Time.fixedDeltaTime);
			Vector3 newPosition = _transform.position - step;
			float distanceToPosition = (newPosition - Cube.StartWorldPosition).magnitude;
			if (distanceToPosition < step.magnitude)
			{
				_transform.position = Cube.StartWorldPosition;
				Cube.CubeStateMachine.HandleInput(FigureAction.Idle);
				return;
			}

			_transform.position = newPosition;
			Cube.FigurePhysics.UpSpeedOnAcceleration();
		}
	}
}