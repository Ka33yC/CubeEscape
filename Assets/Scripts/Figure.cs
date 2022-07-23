using System.Collections.Generic;
using UnityEngine;

public abstract class Figure
{
	protected readonly FiguresParent _parent;
	protected readonly Vector3Int Сoordinates;
	
	public Direction Direction { get; protected set; }

	protected Figure(FiguresParent parent, Vector3Int coordinates)
	{
		_parent = parent;
		Сoordinates = coordinates;
	}

	public abstract void SetRandomDirection(params Direction[] notAvailableDirections);
	
	public abstract IEnumerable<Figure> GetFiguresOnDirection();
}
