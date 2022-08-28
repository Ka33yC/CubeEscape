using UnityEngine;

namespace FigureGameObjects.Animations
{
	public class CubeAnimatorParameters : ScriptableObject
	{
		[SerializeField] private float duration = 1;
		[SerializeField] private AnimationCurve shakeIntensity;

		public float Duration => duration;
		public AnimationCurve ShakeIntensity => shakeIntensity;
	}
}