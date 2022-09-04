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
    
        private FiguresParent _figuresParent;
    
        private void Awake()
        {
            int x = cubeSize.x, y = cubeSize.y, z = cubeSize.z;
            Figure[,,] figures = new Figure[x, y, z];
            _figuresParent = new FiguresParent(figures);

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    for (int k = 0; k < z; k++)
                    {
                        Cube cube = new Cube(_figuresParent, new Vector3Int(i, j, k));
                        figures[i, j, k] = cube;
                        if (isDifficult)
                        {
                            cube.SetDifficultRandomDirection();
                        }
                        else
                        {
                            cube.SetRandomDirection();
                        }
                    }
                }
            }
        }

        private void Start()
        {
            foreach (Figure figure in _figuresParent)
            {
                IFigureGameObject figureGameObject = Instantiate(cubePrefab, transform);
                figureGameObject.Initialize(figure);
                figure.FigureGameObject = figureGameObject;
            }
        
            cameraController.SetSafetyPosition(cubeSize);
        }
    }
}
