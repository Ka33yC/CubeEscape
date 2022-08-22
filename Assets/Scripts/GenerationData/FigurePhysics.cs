using System;
using UnityEngine;

namespace GenerationData
{
	[Serializable]
	public class FigurePhysics
	{
		public readonly SpeedParameters SpeedParameters;
		protected float _nowSpeed;
		protected Vector3 _position;
		
		public event Action<Vector3> OnPositionChanged;
		
		public FigurePhysics(SpeedParameters speedParameters)
		{
			SpeedParameters = speedParameters;
		}
		
		public Vector3 Position
		{
			get => _position;
			set
			{
				_position = value;
				OnPositionChanged?.Invoke(_position);
			}
		}
		
		public float NowSpeed
		{
			get => _nowSpeed;
			set
			{
				_nowSpeed = value;
				if (_nowSpeed > SpeedParameters.MaxSpeed)
				{
					_nowSpeed = SpeedParameters.MaxSpeed;
				}
				else if (_nowSpeed < 0)
				{
					_nowSpeed = 0;
				}
			}
		}
		
		public void SetDefaultNowSpeed() => NowSpeed = SpeedParameters.StartSpeed;

		public void UpSpeedOnAcceleration() =>
			NowSpeed += NowSpeed * SpeedParameters.Acceleration * Time.fixedDeltaTime;
	}
}