using System;
using System.Collections.Generic;
using System.Linq;
using GenerationData;
using LevelGeneration;
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
		private InfinityLevelPart[] _infinityLevelParts;

		public InfinityLevelPart LastInfinityLevelPart { get; private set; }

		public CubeGameObject InstantiateCube() => Instantiate(cubePrefab);

		private void Awake()
		{
		    if (cubeSize.x < 3 || cubeSize.y < 3 || cubeSize.z < 3)
            {
                throw new ArgumentException("For infinity level size must be more than 2");
            }

		    _infinityLevelParts = new InfinityLevelPart[3];
		    for (int i = 0; i < _infinityLevelParts.Length; i++)
		    {
			    _infinityLevelParts[i] = new InfinityLevelPart(this);
		    }
		}
		
		private void Start()
		{
			for (int i = 1; i < _infinityLevelParts.Length; i++)
			{
				_infinityLevelParts[i].SetParent(_infinityLevelParts[i - 1]);
			}
			
			foreach (InfinityLevelPart infinityLevelPart in _infinityLevelParts)
			{
				LevelParameters levelParameters = new LevelParameters();
				levelParameters.DirectedFigures = GetDirectedFiguresByCubeSize();
				levelParameters.IsDifficultGeneration = isDifficult;
				
				infinityLevelPart.GenerateFigures(levelParameters);
				infinityLevelPart.SetScaleWithCubeSize();
				infinityLevelPart.OnLevelComplete += LevelUp;
			}

			LastInfinityLevelPart = _infinityLevelParts[_infinityLevelParts.Length - 1];
			cameraController.SetSafetyPosition(cubeSize);
		}
		
		private bool[,,] GetDirectedFiguresByCubeSize()
		{
			bool[,,] directedFigures = new bool[cubeSize.x, cubeSize.x, cubeSize.x];
			for (int x = 0; x < cubeSize.x; x++)
			{
				for (int y = 0; y < cubeSize.y; y++)
				{
					for (int z = 0; z < cubeSize.z; z++)
					{
						directedFigures[x, y, z] = true;
					}
				}
			}

			return directedFigures;
		}

		private void LevelUp(InfinityLevelPart infinityLevelPart)
		{
			infinityLevelPart.UpChildInHierarchy();
			infinityLevelPart.SetParent(LastInfinityLevelPart);
			
			LevelParameters levelParameters = new LevelParameters();
			levelParameters.DirectedFigures = GetDirectedFiguresByCubeSize();
			levelParameters.IsDifficultGeneration = isDifficult;
			
			infinityLevelPart.GenerateFigures(levelParameters);
			infinityLevelPart.SetScaleWithCubeSize();

			LastInfinityLevelPart = infinityLevelPart;
		}
	}
}