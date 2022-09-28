using UI.Animations;
using UnityEngine;

namespace UI.UIStates
{
	[RequireComponent(typeof(BasePageAnimator))]
	public abstract class UIBasePage : MonoBehaviour
	{
		[SerializeField] protected bool hideByEscape = true;
		[SerializeField] protected bool showInterAdOnHide = true;

		protected BasePageAnimator _animator;

		public UIPagesController UIPagesController;

		private void Awake()
		{
			Initialize();
		}

		protected virtual void Initialize()
		{
			_animator = GetComponent<BasePageAnimator>();
			_animator.Initialize();
		}

		public bool IsHideByEscape => hideByEscape;

		public bool IsShowInterAdOnHide => showInterAdOnHide;

		public virtual bool CanPlayAnimation => !_animator.IsNowPlayingAnyAnimation;

		public virtual void PlayShowAnimation() => _animator.Show();

		public virtual void PlayHideAnimation() => _animator.Hide();
		
		public abstract void CurrentPageShow(UIBasePage previousPage);

		public abstract void CurrentPageHide(UIBasePage previousPage);

		/// <summary>
		/// Показывает объект, вместе с добавлением его в списки и корректным показом включая предыдущие элементы.
		/// </summary>
		public virtual void Show() => UIPagesController.ShowPage(this);

		public virtual void OnPageStackEnter()
		{
		}
		
		public virtual void OnPageStackExit()
		{
		}
	}
}