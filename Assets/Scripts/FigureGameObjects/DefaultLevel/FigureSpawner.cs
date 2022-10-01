using GenerationData;
using LevelGeneration;

namespace FigureGameObjects.DefaultLevel
{
    public class FigureSpawner : FigureSpawnerBase
    {
        public FiguresParent FiguresParent { get; private set; }
        
        public override void StartLevel(LevelParameters levelParameters)
        {
            _levelParameters = levelParameters;
            
            FiguresParent = new FiguresParent(levelParameters);
            GenerateFigures();
            cameraController.Initialize(levelParameters.Size());
        }

        private void GenerateFigures()
        {
            FiguresParent.GenerateDirectedFigure();
            foreach (Figure figure in FiguresParent)
            {
                if(figure == null) continue;
                
                Instantiate(cubePrefab, transform).Initialize(figure);
            }
        }
    }
}
