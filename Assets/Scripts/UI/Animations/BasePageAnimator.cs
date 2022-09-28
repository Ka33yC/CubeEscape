using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Animations
{
	[RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
	public abstract class BasePageAnimator : MonoBehaviour
	{
		protected CanvasGroup _canvasGroup;
		protected Sequence _nowAnimation;

		public bool DisableOnInitialize = true;
		
		public UnityEvent OnShowAnimationStart;
		public UnityEvent OnShowAnimationEnd;
		public UnityEvent OnHideAnimationStart;
		public UnityEvent OnHideAnimationEnd;
		
		public virtual void Initialize()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
			_nowAnimation = DOTween.Sequence();
			if (!DisableOnInitialize) return;
			
			gameObject.SetActive(false);
		}

		public bool IsNowPlayingAnyAnimation => _nowAnimation.IsPlaying();

		public virtual void Show()
		{
			_nowAnimation.Kill(true);
			_nowAnimation = DOTween.Sequence();
			gameObject.SetActive(true);
			
			OnShowAnimationStart.Invoke();
			_canvasGroup.blocksRaycasts = true;
			
			_nowAnimation.onKill += () => OnShowAnimationEnd?.Invoke();
		}
		
		public virtual void Hide()
		{
			_nowAnimation.Kill(true);
			_nowAnimation = DOTween.Sequence();
			OnHideAnimationStart.Invoke();
			_canvasGroup.blocksRaycasts = false;

			_nowAnimation.onKill += () =>
			{
				OnHideAnimationEnd?.Invoke();
				gameObject.SetActive(false);
			};
		}
	}
}