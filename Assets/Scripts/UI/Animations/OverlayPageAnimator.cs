using DG.Tweening;
using UI.Animations.AnimationScriptableObjects.AnimationParametersByCanvasGroup;
using UnityEngine;

namespace UI.Animations
{
	public class OverlayPageAnimator : BasePageAnimator
	{
		[SerializeField] private CanvasGroupAnimationParametes animations;

		public override void Show()
		{
			base.Show();
			StartAnimation(animations.ShowAnimationParameters);
		}

		public override void Hide()
		{
			base.Hide();
			StartAnimation(animations.HideAnimationParameters);
		}
		
		private void StartAnimation(AlphaAnimationParameters animationParameters)
		{
			_canvasGroup.alpha = animationParameters.AnimationStartAlpha;
			
			Tween animationCanvasGroup = _canvasGroup.DOFade(animationParameters.AnimationEndAlpha, animationParameters.Duration)
				.SetEase(animationParameters.AnimationAlphaCurve);
			
			_nowAnimation.Append(animationCanvasGroup);
		}
	}
}