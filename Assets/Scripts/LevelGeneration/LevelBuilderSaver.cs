using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace LevelGeneration
{
	[RequireComponent(typeof(LevelBuilder))]
	public class LevelBuilderSaver : MonoBehaviour
	{
		[SerializeField] private string fileName;

		private LevelBuilder _levelBuilder;
		private string _pathToStreamingAssets;

		private void Awake()
		{
			_levelBuilder = GetComponent<LevelBuilder>();
			_pathToStreamingAssets = Path.Combine(Application.dataPath, "StreamingAssets");
		}

		public void Save()
		{
			LevelParameters levelParameters = _levelBuilder.ConstructLevelParameters();
			File.WriteAllText(Path.Combine(_pathToStreamingAssets, fileName), JsonConvert.SerializeObject(levelParameters));
		}
	}
}