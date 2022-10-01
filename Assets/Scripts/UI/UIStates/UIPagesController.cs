using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.UIStates
{
	public class UIPagesController : MonoBehaviour
	{
		[SerializeField] private List<UIBasePage> pageStack = new List<UIBasePage>();

		private void Start()
		{
			pageStack.Last().PlayShowAnimation();
			pageStack.RemoveAt(pageStack.Count - 1);
		}
		
		private void Update()
		{
			if (!Input.GetKeyDown(KeyCode.Escape)) return;

			int stackCount = pageStack.Count;
			if (stackCount > 1 && pageStack[stackCount - 1].IsHideByEscape && Time.timeScale != 0)
			{
				HideLastState();
			}
		}

		public void ShowPage(UIBasePage pageToShow)
		{
			int pageStackCount = pageStack.Count;

			UIBasePage lastPage = pageStackCount > 0 ? pageStack[pageStackCount - 1] : null;
			if (lastPage == pageToShow || !pageToShow.CanPlayAnimation || (lastPage != null && !lastPage.CanPlayAnimation)) return;

			pageToShow.CurrentPageShow(lastPage);
			pageStack.Add(pageToShow);
			pageToShow.OnPageStackEnter();
		}

		public void HideLastState()
		{
			int pageStackCount = pageStack.Count;

			UIBasePage lastState = pageStack[pageStackCount - 1];
			UIBasePage previousLastState = pageStackCount > 1 ? pageStack[pageStackCount - 2] : null;
			
			if (!lastState.CanPlayAnimation || (previousLastState != null && !previousLastState.CanPlayAnimation)) return;
			
			lastState.CurrentPageHide(previousLastState);
			pageStack.RemoveAt(pageStackCount - 1);
			lastState.OnPageStackExit();
		}
	}
}
