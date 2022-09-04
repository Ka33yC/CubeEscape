#if UNITY_EDITOR

using FigureGameObjects;
using UnityEngine;

namespace AutoSolve
{
	[RequireComponent(typeof(FigureSpawner))]
	public class AutoSolver : MonoBehaviour
	{
		private FigureSpawner _figureSpawner;
		private void Awake()
		{
			_figureSpawner = GetComponent<FigureSpawner>();
		}
	}
}

#endif