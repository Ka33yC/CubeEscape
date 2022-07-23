using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class InputEvents : MonoBehaviour
{
	private Camera _camera;
	private Vector3 _startInputMousePosition;
	private Vector3 _preventFrameMousePosition;

	public Action<Vector3> OnMouseMove;
	public Action<Vector3> OnTouch;
	public Action<Ray> RayOnTouch;

	private void Awake()
	{
		_camera = GetComponent<Camera>();
		OnTouch += ConvertToRayOnTouch;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_startInputMousePosition = Input.mousePosition;
			_preventFrameMousePosition = _startInputMousePosition;
		}

		if (Input.GetMouseButton(0))
		{
			Vector3 mousePosition = Input.mousePosition;
			OnMouseMove?.Invoke(mousePosition - _preventFrameMousePosition);
			_preventFrameMousePosition = mousePosition;
		}

		if (Input.GetMouseButtonUp(0) && _startInputMousePosition == _preventFrameMousePosition)
		{
			OnTouch?.Invoke(_startInputMousePosition);
		}
	}

	private void ConvertToRayOnTouch(Vector3 obj)
	{
		RayOnTouch?.Invoke(_camera.ScreenPointToRay(obj));
	}
}