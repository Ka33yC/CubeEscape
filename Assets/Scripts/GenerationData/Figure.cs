using System;
using System.Collections.Generic;
using UnityEngine;

namespace GenerationData
{
	[Serializable]
	public abstract class Figure
	{
		public readonly FiguresParent Parent;
		public readonly Vector3Int CoordinatesInFiguresParent;
		public readonly Vector3 StartPosition;
		public event Action OnFixedUpdate;
		
		protected Figure(FiguresParent parent, Vector3Int coordinatesInFiguresParent)
		{
			Parent = parent;
			CoordinatesInFiguresParent = coordinatesInFiguresParent;
			StartPosition = CoordinatesToWorldPosition(parent, coordinatesInFiguresParent);
		}

		public abstract void SetRandomDirection(params Direction[] notAvailableDirections);

		public abstract IEnumerable<Figure> GetFiguresOnDirection();

		public virtual void FixedUpdate()
		{
			OnFixedUpdate?.Invoke();
		}

		public static Vector3 CoordinatesToWorldPosition(FiguresParent figuresParent, Vector3Int coordinatesInFiguresParent)
		{
			float xCenterCoordinates = - ((float)figuresParent.GetFiguresCount(0) - 1) / 2;
			float yCenterCoordinates = - ((float)figuresParent.GetFiguresCount(1) - 1) / 2;
			float zCenterCoordinates = - ((float)figuresParent.GetFiguresCount(2) - 1) / 2;

			return new Vector3(xCenterCoordinates + coordinatesInFiguresParent.x, 
				yCenterCoordinates + coordinatesInFiguresParent.y,  zCenterCoordinates + coordinatesInFiguresParent.z);
		}
	}
}
