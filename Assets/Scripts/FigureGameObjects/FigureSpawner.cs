using System;
using GenerationData;
using UnityEngine;

namespace FigureGameObjects
{
    public class FigureSpawner : MonoBehaviour
    {
        [SerializeField, Min(1)] protected Vector3Int cubeSize;
        [SerializeField] protected bool isDifficult;

        [SerializeField] protected CubeGameObject cubePrefab;

        // TODO: Когда появится файл сохранения, убрать прямую передачу и вызов метода и поменять его на вызов в start у камеры
        [SerializeField] protected CameraController cameraController;

        public FiguresParent FiguresParent { get; protected set; }

        private void Awake()
        {
            GenerateFigures();
        }

        protected virtual void GenerateFigures()
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
                        DirectedFigure cube = new DirectedFigure(FiguresParent, new Vector3Int(i, j, k));
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
                if(figure == null) continue;
                
                Instantiate(cubePrefab, transform).Initialize(figure);
            }

            cameraController.SetSafetyPosition(cubeSize);
        }
    }
}
