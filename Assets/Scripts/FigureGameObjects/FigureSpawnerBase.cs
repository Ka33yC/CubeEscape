using System.IO;
using LevelGeneration;
using Newtonsoft.Json;
using UnityEngine;

namespace FigureGameObjects
{
	public abstract class FigureSpawnerBase : MonoBehaviour
	{
		[SerializeField] protected string levelFileName;
		[SerializeField] protected CubeGameObject cubePrefab;

		// TODO: Когда появится файл сохранения, убрать прямую передачу и вызов метода и поменять его на вызов в start у камеры
		[SerializeField] protected CameraController cameraController;
		
		protected LevelParameters _levelParameters;

		public abstract void StartLevel(LevelParameters levelParameters);
		
		private void Awake()
		{
			if(string.IsNullOrEmpty(levelFileName)) return;
			
			StartLevel(JsonConvert.DeserializeObject<LevelParameters>(
				File.ReadAllText(Path.Combine(Application.streamingAssetsPath, levelFileName))));
		}
	}
}