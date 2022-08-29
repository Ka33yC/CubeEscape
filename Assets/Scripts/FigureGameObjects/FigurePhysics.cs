using System;
using UnityEngine;

namespace FigureGameObjects
{
	[RequireComponent(typeof(Collider), typeof(Rigidbody))]
	public class FigurePhysics : MonoBehaviour
	{
		[SerializeField] private SpeedParameters speedParameters;
		
		private Transform _transform;
		private float _nowSpeed;
		private Vector3 _direction;
		
		private event Action OnFixedUpdate;
		public event Action OnPositionReach;
		public event Action<FigureGameObject> OnCollision;

		public float NowSpeed
		{
			get => _nowSpeed;
			set
			{
				_nowSpeed = value;
				if (_nowSpeed > speedParameters.MaxSpeed)
				{
					_nowSpeed = speedParameters.MaxSpeed;
				}
				else if (_nowSpeed < 0)
				{
					_nowSpeed = 0;
				}
			}
		}

		public Vector3 Position
		{
			get => _transform.position;
			set => _transform.position = value;
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
		
			OnCollision?.Invoke(figureGameObject);
		}
		
		public void SetNowSpeedToStart() => NowSpeed = speedParameters.StartSpeed;

		public void UpSpeedOnAcceleration() =>
			NowSpeed += NowSpeed * speedParameters.Acceleration * Time.fixedDeltaTime;
		
		public void StartMoveTo(Vector3 endPosition)
		{
			_direction = (_transform.position - endPosition).normalized;
			
			OnFixedUpdate += () => DoStepTo(endPosition);
		}

		private void DoStepTo(Vector3 position)
		{
			Vector3 step = _direction * (NowSpeed * Time.fixedDeltaTime);
			Vector3 newPosition = _transform.position - step;
			float distanceToPosition = (newPosition - position).magnitude;
			if (distanceToPosition < step.magnitude)
			{
				_transform.position = position;
				OnPositionReach?.Invoke();
				StopMove();
				return;
			}

			_transform.position = newPosition;
			UpSpeedOnAcceleration();
		}

		public void StopMove()
		{
			OnFixedUpdate = null;
			OnPositionReach = null;
		}
	}
}