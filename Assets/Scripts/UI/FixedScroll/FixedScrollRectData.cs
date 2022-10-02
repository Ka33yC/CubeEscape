using UnityEngine;

namespace UI.FixedScroll
{
	public class FixedScrollRectData : MonoBehaviour
	{
		[Tooltip("How fast will page lerp to target position"), SerializeField]
		private float decelerationRate = 10f;
		[SerializeField] private float minLerpedMagnitude = 0.25f;

		public float DecelerationRate => decelerationRate;
		
		public float MinLerpedMagnitude => minLerpedMagnitude;
	}
}