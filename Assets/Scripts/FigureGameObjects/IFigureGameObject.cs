using GenerationData;
using UnityEngine;

namespace FigureGameObjects
{
	public interface IFigureGameObject
	{
		public void Initialize(Figure figure);

		public void Collide(IFigureGameObject collideWith);

		public void Escape();
	}
}