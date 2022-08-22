using System;

namespace GenerationData
{
	[Serializable]
	public class SpeedParameters
	{
		public readonly float EscapeDistance;
		
		public readonly float StartSpeed;
		public readonly float Acceleration;
		public readonly float MaxSpeed;

		
		public SpeedParameters(float escapeDistance, float minSpeed, float acceleration, float maxSpeed)
		{
			EscapeDistance = escapeDistance;
			StartSpeed = minSpeed;
			Acceleration = acceleration;
			MaxSpeed = maxSpeed;
		}
	}
}