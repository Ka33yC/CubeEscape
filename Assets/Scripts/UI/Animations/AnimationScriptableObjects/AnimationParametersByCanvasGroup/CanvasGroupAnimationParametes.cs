using UnityEngine;

namespace UI.Animations.AnimationScriptableObjects.AnimationParametersByCanvasGroup
{
	[CreateAssetMenu(fileName = "New AnimationParameters", menuName = "Animation Parameters/Canvas Group Animation Parameters")]
	public class CanvasGroupAnimationParametes : ScriptableObject
	{
		[SerializeField] private AlphaAnimationParameters showAnimationParameters;
		[SerializeField] private AlphaAnimationParameters hideAnimationParameters;

		public AlphaAnimationParameters ShowAnimationParameters => showAnimationParameters;
		public AlphaAnimationParameters HideAnimationParameters => hideAnimationParameters;
	}

}