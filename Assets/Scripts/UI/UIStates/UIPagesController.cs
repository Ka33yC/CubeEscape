using System;
using System.Collections.Generic;
using System.Linq;
using UI.UIStates.MainMenuScripts;
using UnityEngine;

namespace UI.UIStates
{
	public class UIPagesController : MonoBehaviour
	{
		private List<UIBasePage> _pageStack;

		private void Awake()
		{
			_pageStack = new List<UIBasePage>();
		}

		private void Update()
		{
			if (!Input.GetKeyDown(KeyCode.Escape)) return;

			int stackCount = _pageStack.Count;
			if (stackCount > 1 && _pageStack[stackCount - 1].IsHideByEscape && Time.timeScale != 0)
			{
				HideLastState();
			}
		}

		public void ShowPage(UIBasePage pageToShow)
		{
			int pageStackCount = _pageStack.Count;

			UIBasePage lastPage = pageStackCount > 0 ? _pageStack[pageStackCount - 1] : null;
			if (lastPage == pageToShow || !pageToShow.CanPlayAnimation || (lastPage != null && !lastPage.CanPlayAnimation)) return;

			pageToShow.CurrentPageShow(lastPage);
			_pageStack.Add(pageToShow);
			pageToShow.OnPageStackEnter();
		}

		public void HideLastState()
		{
			int pageStackCount = _pageStack.Count;

			UIBasePage lastState = _pageStack[pageStackCount - 1];
			UIBasePage previousLastState = pageStackCount > 1 ? _pageStack[pageStackCount - 2] : null;
			
			if (!lastState.CanPlayAnimation || (previousLastState != null && !previousLastState.CanPlayAnimation)) return;
			
			lastState.CurrentPageHide(previousLastState);
			_pageStack.RemoveAt(pageStackCount - 1);
			lastState.OnPageStackExit();
		}
	}
}
