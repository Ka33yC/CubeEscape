using System.Collections.Generic;
using UnityEngine;

namespace UI.UIStates.LevelPage
{
	public class LevelPageCreator : MonoBehaviour, IUICreator<LevelPage>
	{
		[SerializeField] private UIPagesController uiPagesController;
		[SerializeField] private LevelPage levelPagePrefab;
		[SerializeField] private RectTransform parent;

		[SerializeField] private OpenLevelButton openLevelButtonPrefab;
		[SerializeField] private LevelLoader levelLoader;
		[SerializeField] private List<int> easyLevelsIndexes;

		public LevelPage Create()
		{
			LevelPage levelPage = Instantiate(levelPagePrefab, parent);
			levelPage.UIPagesController = uiPagesController;

			foreach (int easyLevelsIndex in easyLevelsIndexes)
			{
				OpenLevelButton button = Instantiate(openLevelButtonPrefab, levelPage.EasyLevelsContent);
				button.Button.onClick.AddListener(() => levelLoader.StartLevel(easyLevelsIndex));
				button.Text.text = easyLevelsIndex.ToString();
			}
			
			return levelPage;
		}

		public void CreateAndShow() => uiPagesController.ShowPage(Create());
	}
}