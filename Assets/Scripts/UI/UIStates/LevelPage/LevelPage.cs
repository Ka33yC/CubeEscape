using UnityEngine;

namespace UI.UIStates.LevelPage
{
	public class LevelPage : UISimplePage
	{
		[SerializeField] private RectTransform easyLevelsContent;
		[SerializeField] private RectTransform mediumLevelsContent;
		[SerializeField] private RectTransform hardLevelsContent;

		public RectTransform EasyLevelsContent => easyLevelsContent;
		
		public RectTransform MediumLevelsContent => mediumLevelsContent;
		
		public RectTransform HardLevelsContent => hardLevelsContent;
		
		public override void OnPageStackExit()
		{
			base.OnPageStackExit();
			Destroy(gameObject);
		}
	}
}