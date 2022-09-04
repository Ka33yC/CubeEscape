using System;
using System.Collections.Generic;
using FigureGameObjects;
using UnityEngine;

namespace GenerationData
{
	[Serializable]
	public abstract class Figure
	{
		public readonly FiguresParent Parent;
		public readonly Vector3Int CoordinatesInFiguresParent;
		public readonly Vector3 StartPosition;

		public bool IsKnockedOut { get; private set; }

		protected Figure(FiguresParent parent, Vector3Int coordinatesInFiguresParent)
		{
			Parent = parent;
			CoordinatesInFiguresParent = coordinatesInFiguresParent;
			StartPosition = CoordinatesToWorldPosition(parent, coordinatesInFiguresParent);
		}
		
		/// <returns>Is random direction found</returns>
		public abstract bool SetRandomDirection(params Direction[] notAvailableDirections);
		
		/// <summary>
		/// Difficult means that first will check directions which not match with behind figures
		/// </summary>
		/// <param name="notAvailableDirections"></param>
		/// <returns>Is random direction found</returns>
		public abstract bool SetDifficultRandomDirection(params Direction[] notAvailableDirections);

		public abstract IEnumerable<Figure> GetFiguresOnDirection();

		public static Vector3 CoordinatesToWorldPosition(FiguresParent figuresParent, Vector3Int coordinatesInFiguresParent)
		{
			float xCenterCoordinates = - ((float)figuresParent.Length[0] - 1) / 2;
			float yCenterCoordinates = - ((float)figuresParent.Length[1] - 1) / 2;
			float zCenterCoordinates = - ((float)figuresParent.Length[2] - 1) / 2;

			return new Vector3(xCenterCoordinates + coordinatesInFiguresParent.x, 
				yCenterCoordinates + coordinatesInFiguresParent.y,  zCenterCoordinates + coordinatesInFiguresParent.z);
		}

		public virtual void KnockOut() => IsKnockedOut = true;
	}
}
