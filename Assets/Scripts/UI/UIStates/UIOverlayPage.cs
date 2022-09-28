namespace UI.UIStates
{
	public class UIOverlayPage : UIBasePage
	{
		public override bool CanPlayAnimation => true;
		
		public override void CurrentPageShow(UIBasePage previousPage)
		{
			PlayShowAnimation();
		}

		public override void CurrentPageHide(UIBasePage previousPage)
		{
			PlayHideAnimation();
		}
	}
}
