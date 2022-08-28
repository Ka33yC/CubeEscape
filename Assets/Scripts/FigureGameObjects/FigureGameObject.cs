using GenerationData;
using UnityEngine;

namespace FigureGameObjects
{
	public abstract class FigureGameObject : MonoBehaviour
	{
		public abstract void Initialize(Figure figure, SpeedParameters speedParameters);

		public virtual void Escape()
		{
			Debug.Log("Escape");
		}
	}
}