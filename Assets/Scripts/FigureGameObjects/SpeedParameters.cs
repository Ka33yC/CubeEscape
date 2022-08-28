using UnityEngine;

namespace GenerationData
{
	[CreateAssetMenu (fileName = "New Speed Parameters", menuName = "Create SpeedParameters")]
	public class SpeedParameters : ScriptableObject
	{
		[SerializeField] private float escapeDistance;
		[SerializeField] private float startSpeed;
		[SerializeField] private float acceleration;
		[SerializeField] private float maxSpeed;
		
		public float EscapeDistance => escapeDistance;
		public float StartSpeed => startSpeed;
		public float Acceleration => acceleration;
		public float MaxSpeed => maxSpeed;
	}
}