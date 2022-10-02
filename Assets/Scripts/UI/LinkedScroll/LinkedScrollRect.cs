using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.LinkedScroll
{
	[RequireComponent(typeof(LinkedScrollRectData))]
	public class LinkedScrollRect : ScrollRect
	{
		private LinkedScrollRectData _data;
		
		private Vector2 _onBeginDragContentAnchoredPosition;
		private NowScrollRectDragging _nowDragging;
		private bool _isLinkedOnBeginDragCalled;
		
		protected override void Awake()
		{
			base.Awake();
			_data = GetComponent<LinkedScrollRectData>();
		}

		public override void OnBeginDrag(PointerEventData eventData)
		{
			base.OnBeginDrag(eventData);
			
			_onBeginDragContentAnchoredPosition = content.anchoredPosition;
			_nowDragging = NowScrollRectDragging.Undetected;
		}
		
		public override void OnDrag(PointerEventData eventData)
		{
			base.OnDrag(eventData);
			if (_nowDragging == NowScrollRectDragging.This) return;

			content.anchoredPosition = _onBeginDragContentAnchoredPosition;
			if (_nowDragging == NowScrollRectDragging.Linked)
			{
				if (_isLinkedOnBeginDragCalled)
				{
					_data.LinkedScrollRect.OnDrag(eventData);
				}
				else
				{
					_data.LinkedScrollRect.OnBeginDrag(eventData);
					_isLinkedOnBeginDragCalled = true;
				}
				return;
			}
			
			Vector2 delta = eventData.position - eventData.pressPosition;
			if (Math.Abs(delta.y) >= _data.MinMagnitudeToDrag)
			{
				_nowDragging = NowScrollRectDragging.This;
			}
			else if (Math.Abs(delta.x) >= _data.MinMagnitudeToDrag)
			{
				_nowDragging = NowScrollRectDragging.Linked;
			}
		}

		public override void OnEndDrag(PointerEventData eventData)
		{
			base.OnEndDrag(eventData);
			if (_nowDragging == NowScrollRectDragging.Linked)
			{
				_data.LinkedScrollRect.OnEndDrag(eventData);
			}
			
			_isLinkedOnBeginDragCalled = false;
			_nowDragging = NowScrollRectDragging.Undetected;
		}
	}

	enum NowScrollRectDragging
	{
		Undetected,
		This,
		Linked
	}
}
