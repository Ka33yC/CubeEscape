using UnityEngine;

namespace FigureGameObjects
{
	[CreateAssetMenu(fileName = "New SpeedParameters", menuName = "Create SpeedParameters")]
	public class SpeedParameters : ScriptableObject
	{
		[SerializeField] private float startSpeed;
		[SerializeField] private AnimationCurve accelerationCurve;
		[SerializeField] private float maxSpeed;

		public float StartSpeed => startSpeed;
		public float MaxSpeed => maxSpeed;

		public float Evaluate(float time)
		{
			float maxAcceleration = MaxSpeed - StartSpeed;
			return accelerationCurve.Evaluate(time) * maxAcceleration;
		}
	}
}