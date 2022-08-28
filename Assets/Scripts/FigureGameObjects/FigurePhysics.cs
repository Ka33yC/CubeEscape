using System;
using UnityEngine;

namespace GenerationData
{
	[Serializable]
	public class FigurePhysics
	{
		public readonly SpeedParameters SpeedParameters;
		private float _nowSpeed;
		
		public FigurePhysics(SpeedParameters speedParameters)
		{
			SpeedParameters = speedParameters;
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
		
		public void SetNowSpeedToStart() => NowSpeed = SpeedParameters.StartSpeed;

		public void UpSpeedOnAcceleration() =>
			NowSpeed += NowSpeed * SpeedParameters.Acceleration * Time.fixedDeltaTime;
	}
}