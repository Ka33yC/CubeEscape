using GenerationData;
using UnityEngine;

namespace FigureGameObjects
{
	public abstract class FigureGameObject : MonoBehaviour
	{
		public abstract void Initialize(Figure figure);

		public abstract void Collide(FigureGameObject collideWith);
		
		public virtual void Escape()
		{
			Debug.Log("Escape");
		}
	}
}