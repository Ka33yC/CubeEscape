using System;
using GenerationData;
using UnityEngine;

namespace FigureGameObjects.InfinityLevel
{
	public class InfinityLevelPart
	{
		private readonly InfinityLevelFigureSpawner _infinityLevelSpawner;
		private readonly bool _isDifficult;
		private readonly GameObject _infinityLevelPartGameObject;
		
		private FiguresParent _figuresParent;

		public event Action<InfinityLevelPart> OnLevelComplete;

		public InfinityLevelPart(InfinityLevelFigureSpawner infinityLevelSpawner, bool isDifficult)
		{
			_infinityLevelSpawner = infinityLevelSpawner;
			_isDifficult = isDifficult;
			_infinityLevelPartGameObject = new GameObject("InfinityLevelPart");
		}

		public Transform Transform => _infinityLevelPartGameObject.transform;

		public void GenerateFigures(Vector3Int cubeSize)
		{
			Figure[,,] figures = new Figure[cubeSize.x, cubeSize.y, cubeSize.z];
			_figuresParent = new FiguresParent(figures, _isDifficult);
			
			Action<DirectedFigure> generationAction = _isDifficult
				? figure => figure.SetDifficultRandomDirection(GetNotAvailableDirection(figure))
				: figure => figure.SetRandomDirection(GetNotAvailableDirection(figure));
			
			for (int x = 0; x < cubeSize.x; x++)
			{
				for (int y = 0; y < cubeSize.y; y++)
				{
					for (int z = 0; z < cubeSize.z; z++)
					{
						DirectedFigure cube = new DirectedFigure(_figuresParent, new Vector3Int(x, y, z));
						if(!IsOnAnyFace(cube)) continue;
						
						figures[x, y, z] = cube;
						generationAction(cube);
					}
				}
			}

			foreach (Figure figure in _figuresParent)
			{
				if(figure == null) continue;
                
				CubeGameObject cubeGameObject = _infinityLevelSpawner.InstantiateCube();
				cubeGameObject.transform.SetParent(_infinityLevelPartGameObject.transform, false);
				cubeGameObject.Initialize(figure);
			}

			_figuresParent.OnAllFiguresKnocked += () => OnLevelComplete?.Invoke(this);
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
		private bool IsOnAnyFace(Figure figure)
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