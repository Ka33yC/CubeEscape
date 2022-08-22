using FigureGameObjects;
using GenerationData;
using UnityEngine;
using UnityEngine.Serialization;

public class FigureSpawner : MonoBehaviour
{
    [SerializeField, Min(1)] private Vector3Int cubeSize;
    [SerializeField] private CubeGameObject cubePrefab;
    // TODO: Когда появится файл сохранения, убрать прямую передачу и вызов метода и поменять его на вызов в start у камеры
    [SerializeField] private CameraController cameraController;
    
    [Header("Figure Speed Settings")]
    [SerializeField, Min(0)] private float escapeDistance = 10;
    [SerializeField, Min(0)] private float startSpeed = 1;
    [SerializeField, Min(0)] private float acceleration = 0.5f; 
    [SerializeField, Min(0)] private float maxSpeed = 10;
    
    private FiguresParent _figuresParent;
    
    private void Awake()
    {
        int x = cubeSize.x, y = cubeSize.y, z = cubeSize.z;
        Figure[,,] figures = new Figure[x, y, z];
        _figuresParent = new FiguresParent(figures);
        SpeedParameters speedParameters = new SpeedParameters(escapeDistance, startSpeed, acceleration, maxSpeed);

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                for (int k = 0; k < z; k++)
                {
                    Cube cube = new Cube(_figuresParent, new Vector3Int(i, j, k), speedParameters);
                    figures[i, j, k] = cube;
                    cube.SetRandomDirection();
                }
            }
        }
    }

    private void Start()
    {
        foreach (Figure figure in _figuresParent)
        {
            Instantiate(cubePrefab, transform).Initialize(figure);
        }
        
        cameraController.SetSafetyPosition(cubeSize);
    }
}
