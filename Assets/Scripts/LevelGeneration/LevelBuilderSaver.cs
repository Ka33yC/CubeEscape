using System;
using UnityEngine;

namespace LevelGeneration
{
	[RequireComponent(typeof(LevelBuilder))]
	public class LevelBuilderSaver : MonoBehaviour
	{
		[SerializeField] private string fileName;

		private LevelBuilder _levelBuilder;

		private void Awake()
		{
			_levelBuilder = GetComponent<LevelBuilder>();
		}

		public void Save()
		{
			_levelBuilder.ConstructLevelParameters();
		}
	}
}