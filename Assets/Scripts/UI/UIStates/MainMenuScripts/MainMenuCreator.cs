using UnityEngine;
using UnityEngine.Events;

namespace UI.UIStates.MainMenuScripts
{
	public class MainMenuCreator : MonoBehaviour, IUICreator<MainMenu>
	{
		[SerializeField] private UIPagesController uiPagesController;
		[SerializeField] private MainMenu mainMenuPrefab;
		[SerializeField] private RectTransform parent;

		[SerializeField] private UnityEvent levelsButtonAction;
		[SerializeField] private UnityEvent infinityLevelButtonAction;
		[SerializeField] private UnityEvent skinsButtonAction;
		[SerializeField] private UnityEvent challangesButtonAction;

		public MainMenu Create()
		{
			MainMenu mainMenu = Instantiate(mainMenuPrefab, parent);
			mainMenu.UIPagesController = uiPagesController;
			
			mainMenu.LevelsButton.onClick.AddListener(levelsButtonAction.Invoke);
			mainMenu.InfinityLevelButton.onClick.AddListener(infinityLevelButtonAction.Invoke);
			mainMenu.SkinsButton.onClick.AddListener(skinsButtonAction.Invoke);
			mainMenu.ChallangesButton.onClick.AddListener(challangesButtonAction.Invoke);
			
			return mainMenu;
		}
	}
}