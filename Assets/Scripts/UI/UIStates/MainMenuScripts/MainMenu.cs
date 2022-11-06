using UI.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIStates.MainMenuScripts
{
	public class MainMenu : UISimplePage
	{
		[SerializeField] private Button levelsButton;
		[SerializeField] private Button infinityLevelButton;
		[SerializeField] private Button skinsButton;
		[SerializeField] private Button challangesButton;

		public Button LevelsButton => levelsButton;
		
		public Button InfinityLevelButton => infinityLevelButton;
		
		public Button SkinsButton => skinsButton;
		
		public Button ChallangesButton => challangesButton;

		public override void OnPageStackExit()
		{
			base.OnPageStackExit();
			Destroy(gameObject);
		}
	}
}