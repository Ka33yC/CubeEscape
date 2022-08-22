using System;
using UnityEngine;

namespace GenerationData
{
	[Serializable]
	public class SpeedParameters
	{
		public readonly float EscapeDistance;
		
		public readonly float MinSpeed;
		public readonly float Acceleration;
		public readonly float MaxSpeed;

		public readonly float MinSpeedPerPhysicsFrame;
		public readonly float AccelerationPerPhysicsFrame;
		public readonly float MaxSpeedPerPhysicsFrame;
		
		public SpeedParameters(float escapeDistance, float minSpeed, float acceleration, float maxSpeed)
		{
			EscapeDistance = escapeDistance;
			MinSpeed = minSpeed;
			Acceleration = acceleration;
			MaxSpeed = maxSpeed;

			MinSpeedPerPhysicsFrame = MinSpeed * Time.fixedDeltaTime;
			AccelerationPerPhysicsFrame = Acceleration * Time.fixedDeltaTime;
			MaxSpeedPerPhysicsFrame = MaxSpeed * Time.fixedDeltaTime;
		}
	}
}