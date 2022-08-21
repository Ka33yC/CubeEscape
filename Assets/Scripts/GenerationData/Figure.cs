using System;
using System.Collections.Generic;
using UnityEngine;

namespace GenerationData
{
	[Serializable]
	public abstract class Figure
	{
		public readonly FiguresParent Parent;
		public readonly Vector3Int Сoordinates;
		public readonly SpeedParameters SpeedParameters;

		private float _nowSpeed;

		public float NowSpeed
		{
			get => _nowSpeed;
			set
			{
				_nowSpeed = value;
				if (_nowSpeed < SpeedParameters.MaxSpeedPerPhysicsFrame) return;
				
				_nowSpeed = SpeedParameters.MaxSpeedPerPhysicsFrame;
			}
		}

		protected Figure(FiguresParent parent, Vector3Int coordinates, SpeedParameters speedParameters)
		{
			Parent = parent;
			Сoordinates = coordinates;
			SpeedParameters = speedParameters;
			NowSpeed = speedParameters.StartSpeedPerPhysicsFrame;
		}

		public Direction Direction { get; protected set; }

		public Vector3 WorldPosition()
		{
			float xCenterCoordinates = - ((float)Parent.GetFiguresCount(0) - 1) / 2;
			float yCenterCoordinates = - ((float)Parent.GetFiguresCount(1) - 1) / 2;
			float zCenterCoordinates = - ((float)Parent.GetFiguresCount(2) - 1) / 2;

			return new Vector3(xCenterCoordinates + Сoordinates.x, 
				yCenterCoordinates + Сoordinates.y,  zCenterCoordinates + Сoordinates.z);
		}

		public abstract void SetRandomDirection(params Direction[] notAvailableDirections);

		public abstract IEnumerable<Figure> GetFiguresOnDirection();

		public void UpSpeedOnAcceleration() => _nowSpeed += SpeedParameters.AccelerationPerPhysicsFrame;
		public void SetStartSpeed() => _nowSpeed = SpeedParameters.StartSpeedPerPhysicsFrame;
	}
}
