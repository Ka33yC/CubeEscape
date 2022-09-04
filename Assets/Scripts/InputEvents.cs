using System;
using UnityEngine;

public class InputEvents : MonoBehaviour
{
	private Vector3 _startInputMousePosition;
	private Vector3 _preventFrameMousePosition;

	public Action<Vector3> OnMouseMove;
	public Action<Vector3> OnTouch;
	public Action<float> OnScroll;

	/// <summary>
	/// Возвращает позицию мышки на экране в диапазонах [-1; 1] по x и y
	/// </summary>
	public Vector3 MousePosition => new Vector3(Input.mousePosition.x / Screen.width * 2 - 1,
		Input.mousePosition.y / Screen.height * 2 - 1);

	public static InputEvents Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	private void Update()
	{
		Vector3 mousePosition = Input.mousePosition;

		if (Input.GetMouseButtonDown(0))
		{
			_startInputMousePosition = mousePosition;
			_preventFrameMousePosition = _startInputMousePosition;
		}

		if (Input.GetMouseButton(0))
		{
			OnMouseMove?.Invoke(mousePosition - _preventFrameMousePosition);
			_preventFrameMousePosition = mousePosition;
		}

		if (Input.GetMouseButtonUp(0) && _startInputMousePosition == mousePosition)
		{
			OnTouch?.Invoke(_startInputMousePosition);
		}

		if (Input.mouseScrollDelta != Vector2.zero)
		{
			OnScroll?.Invoke(Input.mouseScrollDelta.y);
		}
	}
}