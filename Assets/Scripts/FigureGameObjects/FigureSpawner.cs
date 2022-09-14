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

        public FiguresParent FiguresParent { get; private set; }

        private void Awake()
        {
            GenerateFigures();
        }

        private void GenerateFigures()
        {
            int x = cubeSize.x, y = cubeSize.y, z = cubeSize.z;
            Figure[,,] figures = new Figure[x, y, z];
            FiguresParent = new FiguresParent(figures, isDifficult);
        }

        private void Start()
        {
            FiguresParent.GenerateFigures();
            foreach (Figure figure in FiguresParent)
            {
                if(figure == null) continue;
                
                Instantiate(cubePrefab, transform).Initialize(figure);
            }

            cameraController.SetSafetyPosition(cubeSize);
        }
    }
}
