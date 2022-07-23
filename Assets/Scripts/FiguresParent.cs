using UnityEngine;

public class FiguresParent
{
	private Figure[,,] _figures;

	public FiguresParent(Figure[,,] figures)
	{
		_figures = figures;
	}

	public Figure[] GetFiguresByDirection(Vector3Int startPosition, Vector3Int direction)
	{
		return null;
	}
}