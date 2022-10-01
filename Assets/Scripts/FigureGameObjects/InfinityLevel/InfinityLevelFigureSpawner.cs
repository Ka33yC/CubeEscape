using GenerationData;
using LevelGeneration;

namespace FigureGameObjects.InfinityLevel
{
	public class InfinityLevelFigureSpawner : FigureSpawnerBase
	{
		private FiguresParent _figuresParent;

		private InfinityLevelPartGameObject LastInfinityLevelPart { get; set; }

		public CubeGameObject InstantiateCube() => Instantiate(cubePrefab);

		public override void StartLevel(LevelParameters levelParameters)
		{
			_levelParameters = levelParameters;
			
			GenerateFigures(levelParameters);
			cameraController.Initialize(levelParameters.Size());
		}

		private void GenerateFigures(LevelParameters levelParameters)
		{
			InfinityLevelPartGameObject[] infinityLevelParts = new InfinityLevelPartGameObject[3];
			for (int i = 0; i < infinityLevelParts.Length; i++)
			{
				infinityLevelParts[i] = new InfinityLevelPartGameObject(this);
			}
			
			for (int i = 1; i < infinityLevelParts.Length; i++)
			{
				infinityLevelParts[i].SetParent(infinityLevelParts[i - 1]);
			}
			
			foreach (InfinityLevelPartGameObject infinityLevelPart in infinityLevelParts)
			{
				infinityLevelPart.GenerateFigures(levelParameters);
				infinityLevelPart.SetScaleWithCubeSize();
				infinityLevelPart.OnLevelComplete += LevelUp;
			}

			LastInfinityLevelPart = infinityLevelParts[infinityLevelParts.Length - 1];
		}

		private void LevelUp(InfinityLevelPartGameObject infinityLevelPart)
		{
			infinityLevelPart.UpChildInHierarchy();
			infinityLevelPart.SetParent(LastInfinityLevelPart);
			infinityLevelPart.GenerateFigures(_levelParameters);
			infinityLevelPart.SetScaleWithCubeSize();

			LastInfinityLevelPart = infinityLevelPart;
		}
	}
}