using System;
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
			JsonSerializer serializer = new JsonSerializer();

			using StreamWriter sw = new StreamWriter(Path.Combine(_pathToStreamingAssets, fileName));
			using JsonWriter writer = new JsonTextWriter(sw);
			
			serializer.Serialize(writer, levelParameters);
		}
	}
}