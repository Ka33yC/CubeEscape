using System;
using UnityEngine;

namespace FigureGameObjects.Animations
{
	public class CubeAnimator
	{
		private Transform _transform;
		private CubeAnimatorParameters _animatorParameters;
		
		public CubeAnimator(Transform cubeTransform, CubeAnimatorParameters animatorParameters)
		{
			_transform = cubeTransform;
			_animatorParameters = animatorParameters;
		}

		public void PlayShakeAnimation()
		{
			//_transform.
		}
	}
}