using System;
using UnityEngine;

namespace GenerationData
{
	[Serializable]
	public class SpeedParameters
	{
		public readonly float EscapeDistance;
		
		public readonly float StartSpeed;
		public readonly float Acceleration;
		public readonly float MaxSpeed;

		public readonly float StartSpeedPerPhysicsFrame;
		public readonly float AccelerationPerPhysicsFrame;
		public readonly float MaxSpeedPerPhysicsFrame;
		
		public SpeedParameters(float escapeDistance, float startSpeed, float acceleration, float maxSpeed)
		{
			EscapeDistance = escapeDistance;
			StartSpeed = startSpeed;
			Acceleration = acceleration;
			MaxSpeed = maxSpeed;

			StartSpeedPerPhysicsFrame = StartSpeed * Time.fixedDeltaTime;
			AccelerationPerPhysicsFrame = Acceleration * Time.fixedDeltaTime;
			MaxSpeedPerPhysicsFrame = MaxSpeed * Time.fixedDeltaTime;
		}
	}
}