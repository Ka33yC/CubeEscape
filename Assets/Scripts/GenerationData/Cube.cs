using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GenerationData
{
	public class Cube : Figure
	{
		private float _escapeDistance;
		private float _startSpeed;
		private float _acceleration;
		private float _maxSpeed;

		public Cube(FiguresParent parent, Vector3Int coordinates, SpeedParameters speedParameters) : 
			base(parent, coordinates, speedParameters)
		{
		
		}

		public override void SetRandomDirection(params Direction[] notAvailableDirections)
		{
			List<Direction> availableDirections = new List<Direction>()
			{
				Direction.Up, Direction.Right, Direction.Forward, Direction.Down, Direction.Left, Direction.Back
			};
		
			foreach (Direction notAvailableDirection in notAvailableDirections)
			{
				availableDirections.Remove(notAvailableDirection);
			}

			for (int i = availableDirections.Count; i > 0; i--)
			{
				Direction = availableDirections[Random.Range(0, availableDirections.Count)];
				HashSet<Figure> checkedFigures = new HashSet<Figure>();
				List<Figure> notCheckedFigures = GetFiguresOnDirection().ToList();
				bool isFindDirection = true;
			
				for (int j = 0; j < notCheckedFigures.Count; j++)
				{
					Figure figureToCheck = notCheckedFigures[j];
					if (figureToCheck == this)
					{
						availableDirections.Remove(Direction);
						isFindDirection = false;
						break;
					}

					if (checkedFigures.Add(figureToCheck))
					{
						notCheckedFigures.AddRange(figureToCheck.GetFiguresOnDirection());
					}
				}
			
				if(isFindDirection) return;
			}
		}

		public override IEnumerable<Figure> GetFiguresOnDirection() =>
			Parent.GetFiguresByDirection(Сoordinates + Direction.ToVector(), Direction);
	}
}
