using System;
using GenerationData;
using UnityEngine;

namespace FigureGameObjects.InfinityLevel
{
	public class InfinityFiguresParent : FiguresParent
	{
		private readonly Action<DirectedFigure> _generationAction;
		
		public InfinityFiguresParent(Figure[,,] figures, bool isDifficult) : base(figures, isDifficult)
		{
			_generationAction = isDifficult
				? figure => figure.SetDifficultRandomDirection(GetNotAvailableDirection(figure))
				: figure => figure.SetRandomDirection(GetNotAvailableDirection(figure));
		}
		
		public override void GenerateFigures()
		{
			for (int x = 0; x < Length[0]; x++)
			{
				for (int y = 0; y < Length[1]; y++)
				{
					for (int z = 0; z < Length[2]; z++)
					{
						DirectedFigure cube = new DirectedFigure(this, new Vector3Int(x, y, z));
						if(!IsOnAnyFace(cube)) continue;
						
						_figures[x, y, z] = cube;
						_generationAction(cube);
					}
				}
			}

			SubscribeOnKnockOutChecker();
		}

		/// <summary>
		/// Возвращает "запрещённые" направления только для тех кубов, что стоят на грани, но не на ребре
		/// </summary>
		private Direction GetNotAvailableDirection(DirectedFigure figure)
		{
			Direction notAvailableDirection = Direction.None;
			
			for (int i = 0; i < 3; i++)
			{
				if (figure.CoordinatesInFiguresParent[i] == 0)
				{
					if (notAvailableDirection != Direction.None) return Direction.None;
					
					notAvailableDirection = (Direction)i;
				}
				else if (figure.CoordinatesInFiguresParent[i] == figure.Parent.Length[i] - 1)
				{
					if (notAvailableDirection != Direction.None) return Direction.None;
					
					notAvailableDirection = (Direction)(i + 3);
				}
			}
			
			return notAvailableDirection;
		}

		/// <summary>
		/// На грани куба?
		/// </summary>
		private bool IsOnAnyFace(DirectedFigure figure)
		{
			for (int i = 0; i < 3; i++)
			{
				if (figure.CoordinatesInFiguresParent[i] == 0 ||
				    figure.CoordinatesInFiguresParent[i] == figure.Parent.Length[i] - 1) return true;
			}

			return false;
		}
	}
}