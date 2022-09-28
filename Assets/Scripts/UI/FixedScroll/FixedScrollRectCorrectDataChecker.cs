#if UNITY_EDITOR

using UnityEngine;
using UnityEngine.UI;

namespace UI.FixedScroll
{
	[RequireComponent(typeof(FixedScrollRect)), ExecuteInEditMode]
	public class FixedScrollRectCorrectDataChecker : MonoBehaviour
	{
		private ScrollRect _scrollView;
		private RectTransform _contentTransform;
		
		private void Start()
		{
			_scrollView = GetComponent<ScrollRect>();
			_contentTransform = _scrollView.content;
		}

		private void Update()
		{
			CheckForErrorPivot();
		}

		private void CheckForErrorPivot()
		{
			if (_contentTransform.pivot != Vector2.up)
			{
				Debug.LogError("Pivot of Content must be Vector2(0, 1)");
			}

			for (int i = 0; i < _contentTransform.childCount; i++)
			{
				RectTransform child = (RectTransform) _contentTransform.GetChild(i);
				if (child.pivot == Vector2.up) continue;
				
				Debug.LogError($"Pivot of {child} must be Vector2(0, 1)");
			}
		}
	}
}

#endif