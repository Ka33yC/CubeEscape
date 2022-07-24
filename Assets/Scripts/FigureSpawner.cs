using System;
using GenerationData;
using UnityEngine;

public class FigureSpawner : MonoBehaviour
{
    [SerializeField, Min(1)] private Vector3Int cubeSize;
    [SerializeField] private FigureGameObject figurePrefab;
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
                    cube.SetRandomDirection();
                }
            }
        }
    }

    private void Start()
    {
        foreach (Figure figure in _figuresParent.Figures)
        {
            FigureGameObject instantiatedFigure = Instantiate(figurePrefab);
            instantiatedFigure.Figure = figure;
        }
    }
}
