using System;
using System.Collections.Generic;
using UnityEngine;

namespace GenerationData
{
	[Serializable]
	public abstract class Figure
	{
		public readonly FiguresParent Parent;
		public readonly Vector3Int Position;
	
		
		protected Figure(FiguresParent parent, Vector3Int localPosition)
		{
			Parent = parent;
			Position = localPosition;
		}

		public abstract void SetRandomDirection(params Direction[] notAvailableDirections);

		public abstract IEnumerable<Figure> GetFiguresOnDirection();

		public static Vector3 LocalToWorldPosition(FiguresParent figuresParent, Vector3Int coordinates)
		{
			float xCenterCoordinates = - ((float)figuresParent.GetFiguresCount(0) - 1) / 2;
			float yCenterCoordinates = - ((float)figuresParent.GetFiguresCount(1) - 1) / 2;
			float zCenterCoordinates = - ((float)figuresParent.GetFiguresCount(2) - 1) / 2;

			return new Vector3(xCenterCoordinates + coordinates.x, 
				yCenterCoordinates + coordinates.y,  zCenterCoordinates + coordinates.z);
		}
	}
}
