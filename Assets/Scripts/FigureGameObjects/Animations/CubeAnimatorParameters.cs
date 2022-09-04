using UnityEngine;

namespace FigureGameObjects.Animations
{
	[CreateAssetMenu(fileName = "New CubeAnimatorParameters", menuName = "Create CubeAnimatorParameters")]
	public class CubeAnimatorParameters : ScriptableObject
	{
		[SerializeField] private float duration = 1;
		[SerializeField] private AnimationCurve shakeAnimationIntensity;
		[SerializeField] private float strength = 0.1f;
		[SerializeField] private int vibratoFrequency = 20;

		public float Duration => duration;

		public AnimationCurve ShakeAnimationIntensity => shakeAnimationIntensity;

		public float Strength => strength;

		public int VibratoFrequency => vibratoFrequency;
	}
}