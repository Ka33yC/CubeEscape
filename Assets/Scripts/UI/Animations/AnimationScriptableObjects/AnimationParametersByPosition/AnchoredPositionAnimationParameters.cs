using UnityEngine;

namespace UI.Animations.AnimationScriptableObjects.AnimationParametersByPosition
{
	[CreateAssetMenu(fileName = "New AnimationParameters", menuName = "Animation Parameters/Anchored Position Animation Parameters")]
	public class AnchoredPositionAnimationParameters : ScriptableObject
	{
		[SerializeField] private PositionAnimationParameters showAnimationParameters;
		[SerializeField] private PositionAnimationParameters hideAnimationParameters;

		public PositionAnimationParameters ShowAnimationParameters => showAnimationParameters;
		public PositionAnimationParameters HideAnimationParameters => hideAnimationParameters;
	}
}