using System;
using GenerationData;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class FigureGameObject : MonoBehaviour
{
	private Figure _figure;
	private Transform _transform;

	private bool _isEscape;
	
	public Figure Figure
	{
		get => _figure;
		private set
		{
			_figure = value;
			transform.position = _figure.WorldPosition();
			transform.rotation = _figure.Direction.ToQuaternion();
		}
	}

	private void Awake()
	{
		_transform = GetComponent<Transform>();
	}

	private void FixedUpdate()
	{
		if(!_isEscape) return;

		_transform.position += _transform.forward * Figure.NowSpeed;
		_figure.UpSpeedOnAcceleration();
	}

	public void Initialize(Figure figure)
	{
		Figure = figure;
	}

	public void Escape()
	{
		_isEscape = true;
		Debug.Log("Escape");
	}
}