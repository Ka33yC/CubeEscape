using System.IO;
using GenerationData;
using LevelGeneration;
using Newtonsoft.Json;
using UnityEngine;

namespace FigureGameObjects
{
    public class FigureSpawner : MonoBehaviour
    {
        [SerializeField, Min(1)] private Vector3Int cubeSize;
        [SerializeField] private bool isDifficult;
        [SerializeField] private CubeGameObject cubePrefab;
        [SerializeField] private string levelFileName;

        // TODO: Когда появится файл сохранения, убрать прямую передачу и вызов метода и поменять его на вызов в start у камеры
        [SerializeField] protected CameraController cameraController;

        public FiguresParent FiguresParent { get; private set; }

        private void Awake()
        {
            if (string.IsNullOrEmpty(levelFileName))
            {
                GenerateFiguresByCubeSize();
            }
            else
            {
                FiguresParent = new FiguresParent(JsonConvert.DeserializeObject<LevelParameters>(
                    File.ReadAllText(Path.Combine(Application.streamingAssetsPath, levelFileName))));
            }
        }

        private void GenerateFiguresByCubeSize()
        {
            LevelParameters levelParameters = new LevelParameters();
            levelParameters.DirectedFigures = GetDirectedFiguresByCubeSize();
            levelParameters.IsDifficultGeneration = isDifficult;
            FiguresParent = new FiguresParent(levelParameters);
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

        private void Start()
        {
            FiguresParent.GenerateDirectedFigure();
            foreach (Figure figure in FiguresParent)
            {
                if(figure == null) continue;
                
                Instantiate(cubePrefab, transform).Initialize(figure);
            }

            cameraController.SetSafetyPosition(cubeSize);
        }
    }
}
