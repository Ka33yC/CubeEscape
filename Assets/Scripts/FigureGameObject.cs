using GenerationData;
using UnityEngine;

public class FigureGameObject : MonoBehaviour
{
	private Figure _figure;
	
	public Figure Figure
	{
		get => _figure;
		set
		{
			_figure = value;
			transform.position += _figure.Сoordinates;
			transform.rotation = _figure.Direction.ToQuaternion();
		}
	}
}