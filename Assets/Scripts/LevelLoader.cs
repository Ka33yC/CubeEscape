using System;
using System.IO;
using System.Threading.Tasks;
using FigureGameObjects;
using LevelGeneration;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	public async void StartLevel(int levelNum)
	{
		await LoadScene(1);

		FindObjectOfType<FigureSpawnerBase>().StartLevel(JsonConvert.DeserializeObject<LevelParameters>(
			File.ReadAllText(Path.Combine(Application.streamingAssetsPath, $"lvl{levelNum}.json"))));
		Destroy(gameObject);
	}

	public async void StartInfinityLevel(int levelNum)
	{
		await LoadScene(2);

		FindObjectOfType<FigureSpawnerBase>().StartLevel(JsonConvert.DeserializeObject<LevelParameters>(
			File.ReadAllText(Path.Combine(Application.streamingAssetsPath, $"infLvl{levelNum}.json"))));
		Destroy(gameObject);
	}

	private async Task LoadScene(int sceneIndex)
	{
		var sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneIndex);
		sceneLoadingOperation.allowSceneActivation = true;
		while (!sceneLoadingOperation.isDone)
		{
			await Task.Yield();
		}
	}
}
