using DG.Tweening;
using UnityEngine;

namespace FigureGameObjects.Animations
{
	public class CubeAnimator
	{
		private readonly Transform _transform;
		private readonly CubeAnimatorParameters _animatorParameters;

		private Sequence _nowAnimation;

		public CubeAnimator(Transform cubeTransform, CubeAnimatorParameters animatorParameters)
		{
			_transform = cubeTransform;
			_animatorParameters = animatorParameters;
		}

		public void PlayShakeAnimation()
		{
			_nowAnimation = DOTween.Sequence();
			_nowAnimation.Append(_transform
				.DOShakePosition(_animatorParameters.Duration, _animatorParameters.Strength,
					_animatorParameters.VibratoFrequency)
				.SetEase(_animatorParameters.ShakeAnimationIntensity));
		}

		public void StopShakeAnimation()
		{
			_nowAnimation.Kill(true);
		}
	}
}