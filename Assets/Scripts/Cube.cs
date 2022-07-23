using System.Collections.Generic;
using UnityEngine;

public class Cube : Figure
{
	private Vector3Int _coordinates;

	public Cube(FiguresParent parent, Vector3Int coordinates) : base(parent)
	{
		_coordinates = coordinates;
	}

	public override void SetRandomDirection()
	{
		
	}

	public override List<Figure> GetFiguresOnDirection()
	{
		
	}
}
