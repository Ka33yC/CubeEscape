using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.FixedScroll
{
	[RequireComponent(typeof(FixedScrollRectData))]
#if UNITY_EDITOR
	[RequireComponent(typeof(FixedScrollRectCorrectDataChecker))]
#endif
	public class FixedScrollRect : ScrollRect
	{
		private FixedScrollRectData _data;

		private List<Vector2> _childPositions;
		private bool _needLerp = false;
		private int _nowChild;

		public event Action<int> OnNowChildChange;
		
		public bool NeedRecalculateChildPositions = true;
		
		private int NowChild
		{
			get => _nowChild;
			set
			{
				if(_nowChild == value) return;
				
				_nowChild = value;
				OnNowChildChange?.Invoke(_nowChild);
			}
		}
		
		public virtual void Initialize()
		{
			_data = GetComponent<FixedScrollRectData>();
			_childPositions = new List<Vector2>();
		}

		private List<Vector2> GetChildPositions()
		{
			Vector2 shiftToCenter = ((RectTransform)transform).rect.size / 2;

			if (horizontal)
			{
				shiftToCenter.y = 0;
			}
			if (vertical)
			{
				shiftToCenter.x = 0;
			}
			
			int childCount = content.childCount;
			List<Vector2> result = new List<Vector2>(childCount);
			
			for (int i = 0; i < childCount; i++)
			{
				RectTransform child = (RectTransform)content.GetChild(i);
				if (!child.gameObject.activeSelf) continue;
				
				result.Add(-child.anchoredPosition + shiftToCenter);
			}

			return result;
		}

		private void Update()
		{
			if(!NeedRecalculateChildPositions || !Application.isPlaying) return;
		
			RecalculateImmediatly();
			NeedRecalculateChildPositions = false;
		}

		public void RecalculateImmediatly()
		{
			_childPositions = GetChildPositions();
			content.anchoredPosition = _childPositions[NowChild];
		}

		protected override void LateUpdate()
		{
			base.LateUpdate();
			if (!_needLerp) return;

			float decelerate = Mathf.Min(_data.DecelerationRate * Time.deltaTime, 1f);
			Vector2 lerpTo = _childPositions[NowChild];
			Vector2 newAnchoredPosition = Vector2.Lerp(content.anchoredPosition, lerpTo, decelerate);

			if (Vector2.SqrMagnitude(newAnchoredPosition - lerpTo) > _data.MinLerpedMagnitude)
			{
				content.anchoredPosition = newAnchoredPosition;
			}
			else
			{
				content.anchoredPosition = lerpTo;
				_needLerp = false;
			}
		}
		
		private int GetClosestImageIndex()
		{
			int closestImageIndex = NowChild;
			int leftIndex = closestImageIndex - 1, rightIndex = closestImageIndex + 1;
			float minDeltaDistance =
				Vector2.SqrMagnitude(content.anchoredPosition - _childPositions[closestImageIndex]);
		
			if (leftIndex != -1)
			{
				float leftImageDelta =
					Vector2.SqrMagnitude(content.anchoredPosition - _childPositions[leftIndex]);
				if (leftImageDelta < minDeltaDistance)
				{
					closestImageIndex = leftIndex;
					minDeltaDistance = leftImageDelta;
				}
			}
		
			if (rightIndex != _childPositions.Count &&
				Vector2.SqrMagnitude(content.anchoredPosition - _childPositions[rightIndex]) <
				minDeltaDistance)
			{
				closestImageIndex = rightIndex;
			}
		
			return closestImageIndex;
		}

		public override void OnBeginDrag(PointerEventData eventData)
		{
			base.OnBeginDrag(eventData);
			_needLerp = false;
		}

		public override void OnDrag(PointerEventData eventData)
		{
			base.OnDrag(eventData);
			NowChild = GetClosestImageIndex();
		}

		public override void OnEndDrag(PointerEventData eventData)
		{
			base.OnEndDrag(eventData);
			_needLerp = true;
			int axis = horizontal ? 0 : 1;
			
			Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
			if (dragVectorDirection[axis] > 0)
			{
				PreviousImage();
			}
			else if (dragVectorDirection[axis] < 0)
			{
				NextImage();
			}
		}

		public void SetPage(int page)
		{
			if(_childPositions.Count <= page) return;
			
	        content.anchoredPosition = _childPositions[page];
	        NowChild = page;
		}
		
		public void SetPageWithLerp(int page)
		{
			if(_childPositions.Count <= page) return;
			
			NowChild = page;
			_needLerp = true;
		}
		
		public void PreviousImage()
		{
			if(NowChild - 1 < 0) return;
			
			--NowChild;
			_needLerp = true;
		}
		
		public void NextImage()
		{
			if(NowChild + 1 >= _childPositions.Count) return;
			
			++NowChild;
			_needLerp = true;
		}
	}
}