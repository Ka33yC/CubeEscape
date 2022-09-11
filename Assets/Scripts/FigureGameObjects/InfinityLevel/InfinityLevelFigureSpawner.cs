using System;
using System.Collections.Generic;
using System.Linq;
using GenerationData;
using UnityEngine;

namespace FigureGameObjects.InfinityLevel
{
	public class InfinityLevelFigureSpawner : MonoBehaviour
	{
		[SerializeField, Min(1)] protected Vector3Int cubeSize;
		[SerializeField] protected bool isDifficult;
		[SerializeField] protected CubeGameObject cubePrefab;
		// TODO: Когда появится файл сохранения, убрать прямую передачу и вызов метода и поменять его на вызов в start у камеры
		[SerializeField] protected CameraController cameraController;

		private FiguresParent _figuresParent;
		
		private void Awake()
		{
			GenerateFigures();
		}
		
		private void Start()
		{
			foreach (Figure figure in _figuresParent)
			{
				if(figure == null) continue;
                
				Instantiate(cubePrefab, transform).Initialize(figure);
			}

			cameraController.SetSafetyPosition(cubeSize);
		}
		
		private void GenerateFigures()
		{
			if (cubeSize.x < 3 || cubeSize.y < 3 || cubeSize.z < 3)
			{
				throw new ArgumentException("For infinity level size must be more than 2");
			}
			
			Figure[,,] figures = new Figure[cubeSize.x, cubeSize.y, cubeSize.z];
			_figuresParent = new FiguresParent(figures, isDifficult);
			Action<DirectedFigure> generationAction = isDifficult
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