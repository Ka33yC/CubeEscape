using DG.Tweening;
using UI.Animations.AnimationScriptableObjects.AnimationParametersByPosition;
using UnityEngine;

namespace UI.Animations
{
	public class SimplePageAnimator : BasePageAnimator
	{
		[SerializeField] private AnchoredPositionAnimationParameters animations;

		private RectTransform _transform;
		
		public override void Initialize()
		{
			base.Initialize();
			_transform = GetComponent<RectTransform>();
		}

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

		private void StartAnimation(PositionAnimationParameters animationParameters)
		{
			_transform.anchoredPosition = animationParameters.AnimationStartPosition;
			Vector2 endPosition = animationParameters.AnimationEndPosition;
			
			Tween animationByX = _transform.DOAnchorPosX(endPosition.x, animationParameters.Duration)
				.SetEase(animationParameters.AnimationXPositionCurve);
			Tween animationByY = _transform.DOAnchorPosY(endPosition.y, animationParameters.Duration)
				.SetEase(animationParameters.AnimationYPositionCurve);
			
			_nowAnimation.Append(animationByX);
			_nowAnimation.Insert(0, animationByY);
		}
	}
}