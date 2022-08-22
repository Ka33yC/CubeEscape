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
	
		public Cube Cube
		{
			get => _cube;
			private set
			{
				_cube = value;
				transform.position = value.StartPosition;
				transform.rotation = _cube.Direction.ToQuaternion();
				
				_cube.OnPositionChanged += newPosition => _transform.position = newPosition;
			}
		}

		private void Awake()
		{
			_transform = GetComponent<Transform>();
		}

		private void FixedUpdate()
		{
			Cube.FixedUpdate();
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
		}
	}
}