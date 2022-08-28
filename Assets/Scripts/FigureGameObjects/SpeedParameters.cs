using UnityEngine;

namespace FigureGameObjects
{
	[CreateAssetMenu (fileName = "New Speed Parameters", menuName = "Create SpeedParameters")]
	public class SpeedParameters : ScriptableObject
	{
		[SerializeField] private float startSpeed;
		[SerializeField] private float acceleration;
		[SerializeField] private float maxSpeed;
		
		public float StartSpeed => startSpeed;
		public float Acceleration => acceleration;
		public float MaxSpeed => maxSpeed;
	}
}