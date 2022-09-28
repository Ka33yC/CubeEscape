using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.FixedScroll
{
	[RequireComponent(typeof(ScrollRect))]
	public class FixedScrollRect : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		[Tooltip("How fast will page lerp to target position"), SerializeField]
		private float decelerationRate = 10f;

		[SerializeField] private float minLerpedMagnitude = 0.25f;

		private ScrollRect _scrollView;
		private List<Vector2> _imagePositions;
		private bool _needLerp = false;
		private int _nowImage;

		public RectTransform ContentTransform { get; private set; }

		public bool NeedRecalculateImagePositions = true;

		public void Initialize()
		{
			_scrollView = GetComponent<ScrollRect>();
			ContentTransform = _scrollView.content;
			_imagePositions = new List<Vector2>();
		}
		
		private List<Vector2> GetImagePositions()
		{
			int childCount = ContentTransform.childCount;
			List<Vector2> result = new List<Vector2>(childCount);

			for (int i = 0; i < childCount; i++)
			{
				RectTransform child = (RectTransform)ContentTransform.GetChild(i);
				if (!child.gameObject.activeSelf) continue;

				result.Add(-child.anchoredPosition);
			}

			return result;
		}

		private void Update()
		{
			if(!NeedRecalculateImagePositions) return;
			
			_imagePositions = GetImagePositions();
		}

		private void LateUpdate()
		{
			if (!_needLerp) return;

			float decelerate = Mathf.Min(decelerationRate * Time.deltaTime, 1f);
			Vector2 lerpTo = _imagePositions[_nowImage];
			Vector2 newAnchoredPosition = Vector2.Lerp(ContentTransform.anchoredPosition, lerpTo, decelerate);

			if (Vector2.SqrMagnitude(newAnchoredPosition - lerpTo) > minLerpedMagnitude)
			{
				ContentTransform.anchoredPosition = newAnchoredPosition;
			}
			else
			{
				ContentTransform.anchoredPosition = lerpTo;
				_needLerp = false;
			}
		}
		
		private int GetClosestImageIndex()
		{
			int closestImageIndex = _nowImage;
			int leftIndex = closestImageIndex - 1, rightIndex = closestImageIndex + 1;
			float minDeltaDistance =
				Vector2.SqrMagnitude(ContentTransform.anchoredPosition - _imagePositions[closestImageIndex]);
		
			if (leftIndex != -1)
			{
				float leftImageDelta =
					Vector2.SqrMagnitude(ContentTransform.anchoredPosition - _imagePositions[leftIndex]);
				if (leftImageDelta < minDeltaDistance)
				{
					closestImageIndex = leftIndex;
					minDeltaDistance = leftImageDelta;
				}
			}
		
			if (rightIndex != _imagePositions.Count &&
				Vector2.SqrMagnitude(ContentTransform.anchoredPosition - _imagePositions[rightIndex]) <
				minDeltaDistance)
			{
				closestImageIndex = rightIndex;
			}
		
			return closestImageIndex;
		}

		public void OnBeginDrag(PointerEventData eventData) => _needLerp = false;
		
		public void OnDrag(PointerEventData eventData) => _nowImage = GetClosestImageIndex();

		public void OnEndDrag(PointerEventData eventData)
		{
			_needLerp = true;
			int axis = _scrollView.horizontal ? 0 : 1;
			
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
			if(_imagePositions.Count <= page) return;
			
	        ContentTransform.anchoredPosition = _imagePositions[page];
	        _nowImage = page;
		}

		public void PreviousImage()
		{
			_nowImage = --_nowImage <= -1 ? 0 : _nowImage;
			_needLerp = true;
		}
		
		public void NextImage()
		{
			_nowImage = ++_nowImage >= _imagePositions.Count ? _imagePositions.Count - 1 : _nowImage;
			_needLerp = true;
		}
	}
}