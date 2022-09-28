using System;
using UnityEngine;

namespace UI.Animations.AnimationScriptableObjects.AnimationParametersByCanvasGroup
{
	[Serializable]
	public class AlphaAnimationParameters
	{
		[SerializeField] private float animationStartAlpha;
		[SerializeField, Min(0)] private float duration;
		[SerializeField] private AnimationCurve animationAlphaCurve;
		[SerializeField] private float animationEndAlpha;

		public float AnimationStartAlpha => animationStartAlpha;
		public float Duration => duration;
		public AnimationCurve AnimationAlphaCurve => animationAlphaCurve;
		public float AnimationEndAlpha => animationEndAlpha;
	}
}