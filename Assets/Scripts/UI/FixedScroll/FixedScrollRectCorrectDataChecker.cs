#if UNITY_EDITOR

using UnityEngine;
using UnityEngine.UI;

namespace UI.FixedScroll
{
	[ExecuteInEditMode]
	public class FixedScrollRectCorrectDataChecker : MonoBehaviour
	{
		private readonly Vector2 _correctContentPivot = Vector2.zero;
		private readonly Vector2 _correctContentChildPivot = new Vector2(0.5f, 1);

		
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
			if (_contentTransform.pivot != _correctContentPivot)
			{
				Debug.LogError($"Pivot of Content must be {_correctContentPivot}");
			}

			for (int i = 0; i < _contentTransform.childCount; i++)
			{
				RectTransform child = (RectTransform) _contentTransform.GetChild(i);
				if (child.pivot == _correctContentChildPivot) continue;
				
				Debug.LogError($"Pivot of {child} must be {_correctContentChildPivot}");
			}
		}
	}
}

#endif