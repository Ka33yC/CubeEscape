using System;
using UnityEngine;

namespace UI.Animations.AnimationScriptableObjects.AnimationParametersByPosition
{
	[Serializable]
	public class PositionAnimationParameters
	{
		[SerializeField] private Vector2 animationStartPosition;
		[SerializeField, Min(0)] private float duration;
		[SerializeField] private AnimationCurve animationXPositionCurve;
		[SerializeField] private AnimationCurve animationYPositionCurve;
		[SerializeField] private Vector2 animationEndPosition;

		public Vector2 AnimationStartPosition => animationStartPosition;
		public float Duration => duration;
		public AnimationCurve AnimationXPositionCurve => animationXPositionCurve;
		public AnimationCurve AnimationYPositionCurve => animationYPositionCurve;
		public Vector2 AnimationEndPosition => animationEndPosition;
	}
}