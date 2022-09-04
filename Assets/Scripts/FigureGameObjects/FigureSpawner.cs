using System;
using GenerationData;
using UnityEngine;

namespace FigureGameObjects
{
    public class FigureSpawner : MonoBehaviour
    {
        [SerializeField, Min(1)] private Vector3Int cubeSize;
        [SerializeField] private bool isDifficult;
        [SerializeField] private CubeGameObject cubePrefab;
        // TODO: Когда появится файл сохранения, убрать прямую передачу и вызов метода и поменять его на вызов в start у камеры
        [SerializeField] private CameraController cameraController;

        public FiguresParent FiguresParent { get; private set; }

        private void Awake()
        {
            int x = cubeSize.x, y = cubeSize.y, z = cubeSize.z;
            Figure[,,] figures = new Figure[x, y, z];
            FiguresParent = new FiguresParent(figures, isDifficult);

            Action<Figure> generationAction = isDifficult
                ? figure => figure.SetDifficultRandomDirection()
                : figure => figure.SetRandomDirection();

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    for (int k = 0; k < z; k++)
                    {
                        Cube cube = new Cube(FiguresParent, new Vector3Int(i, j, k));
                        figures[i, j, k] = cube;
                        generationAction(cube);
                    }
                }
            }
        }

        private void Start()
        {
            foreach (Figure figure in FiguresParent)
            {
                Instantiate(cubePrefab, transform).Initialize(figure);
            }
        
            cameraController.SetSafetyPosition(cubeSize);
        }
    }
}
