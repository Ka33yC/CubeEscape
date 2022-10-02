namespace UI.FixedScroll
{
	public class FixedScrollRectSelfInitialize : FixedScrollRect
	{
		protected override void Awake()
		{
			base.Awake();
			Initialize();
		}
	}
}