using System;
using System.Collections.Generic;
using UnityEngine;

namespace GenerationData
{
	[Serializable]
	public abstract class Figure
	{
		protected readonly FiguresParent _parent;
		public readonly Vector3Int Сoordinates;

		protected Figure(FiguresParent parent, Vector3Int coordinates)
		{
			_parent = parent;
			Сoordinates = coordinates;
		}

		public Direction Direction { get; protected set; }

		public Vector3 WorldPosition()
		{
			float xCenterCoordinates = - ((float)_parent.GetFiguresCount(0) - 1) / 2;
			float yCenterCoordinates = - ((float)_parent.GetFiguresCount(1) - 1) / 2;
			float zCenterCoordinates = - ((float)_parent.GetFiguresCount(2) - 1) / 2;

			return new Vector3(xCenterCoordinates + Сoordinates.x, 
				yCenterCoordinates + Сoordinates.y,  zCenterCoordinates + Сoordinates.z);
		}

		public abstract void SetRandomDirection(params Direction[] notAvailableDirections);

		public abstract IEnumerable<Figure> GetFiguresOnDirection();
	}
}
