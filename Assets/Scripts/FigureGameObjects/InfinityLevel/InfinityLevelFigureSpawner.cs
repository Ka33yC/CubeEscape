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
		private InfinityLevelPart[] _infinityLevelParts;
		
		private void Awake()
		{
		    if (cubeSize.x < 3 || cubeSize.y < 3 || cubeSize.z < 3)
            {
                throw new ArgumentException("For infinity level size must be more than 2");
            }

		    _infinityLevelParts = new InfinityLevelPart[3];
		    for (int i = 0; i < _infinityLevelParts.Length; i++)
		    {
			    _infinityLevelParts[i] = new InfinityLevelPart(this, isDifficult);
		    }
		}
		
		private void Start()
		{
			Vector3 scaleDelta = new((float)(cubeSize.x - 2) / cubeSize.x, (float)(cubeSize.y - 2) / cubeSize.y,
				(float)(cubeSize.z - 2) / cubeSize.z);
			
			_infinityLevelParts[0].Transform.SetParent(transform);
			for (int i = 1; i < _infinityLevelParts.Length; i++)
			{
				InfinityLevelPart infinityLevelPart = _infinityLevelParts[i];
				
				infinityLevelPart.Transform.SetParent(_infinityLevelParts[i - 1].Transform);
				infinityLevelPart.Transform.localScale = scaleDelta;
			}

			foreach (InfinityLevelPart infinityLevelPart in _infinityLevelParts)
			{
				infinityLevelPart.GenerateFigures(cubeSize);
			}

			cameraController.SetSafetyPosition(cubeSize);
		}

		public CubeGameObject InstantiateCube() => Instantiate(cubePrefab);
	}
}