using JetBrains.Annotations;

namespace UI.UIStates
{
	public class UISimplePage : UIBasePage
	{
		public override void CurrentPageShow([CanBeNull] UIBasePage pageToHide)
		{
			pageToHide?.PlayHideAnimation();
			_animator.Show();
		}

		public override void CurrentPageHide([CanBeNull] UIBasePage pageToShow)
		{
			_animator.Hide();
			pageToShow?.PlayShowAnimation();
		}
	}
}
