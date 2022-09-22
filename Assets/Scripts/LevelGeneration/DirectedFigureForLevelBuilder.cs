#if UNITY_EDITOR

using GenerationData;
using UnityEngine;

namespace LevelGeneration
{
	public class DirectedFigureForLevelBuilder : MonoBehaviour
	{
		public Direction Direction;
		public bool Exist;

		public void ReverseExistState()
		{
			Exist = !Exist;
			gameObject.SetActive(Exist);
		}

		public void SetParent(Transform parent) => transform.SetParent(parent, false);

		public Vector3 Position
		{
			get => transform.position;
			set => transform.position = value;
		}
	}
}
#endif