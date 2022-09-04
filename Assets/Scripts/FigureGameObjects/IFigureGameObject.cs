using GenerationData;

namespace FigureGameObjects
{
	public interface IFigureGameObject
	{
		public Figure Figure { get; }

		public void Initialize(Figure figure);

		public void Collide(IFigureGameObject collideWith);

		public void Escape();
	}
}