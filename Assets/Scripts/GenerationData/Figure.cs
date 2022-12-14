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

		protected bool _isKnockedOut;
		
		public event Action<Figure> OnKnockOut;
		public IFigureGameObject FigureGameObject;


		public bool IsKnockedOut => _isKnockedOut;

		protected Figure(FiguresParent parent, Vector3Int coordinatesInFiguresParent)
		{
			Parent = parent;
			CoordinatesInFiguresParent = coordinatesInFiguresParent;
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

		public virtual void KnockOut()
		{
			if (_isKnockedOut)
			{
				throw new Exception("Cube can not be knocked out twice");
			}
			
			_isKnockedOut = true;
			Parent[CoordinatesInFiguresParent] = null;
			OnKnockOut?.Invoke(this);
		}
	}
}
