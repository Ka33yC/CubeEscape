using GenerationData;
using UnityEngine;

namespace FigureGameObjects
{
	public abstract class FigureGameObject : MonoBehaviour
	{
		public abstract void Initialize(Figure cube);

		public virtual void Escape()
		{
			Debug.Log("Escape");
		}
	}
}