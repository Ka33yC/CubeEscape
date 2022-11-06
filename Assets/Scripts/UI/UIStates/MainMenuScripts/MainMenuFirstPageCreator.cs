using System;
using UnityEngine;

namespace UI.UIStates.MainMenuScripts
{
	public class MainMenuFirstPageCreator : MonoBehaviour
	{
		[SerializeField] private UIPagesController uiPagesController;
		[SerializeField] private MainMenuCreator mainMenuCreator;

		private void Start()
		{
			uiPagesController.ShowPage(mainMenuCreator.Create());
		}
	}
}