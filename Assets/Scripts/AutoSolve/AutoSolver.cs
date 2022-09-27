#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FigureGameObjects;
using GenerationData;
using UnityEngine;

namespace AutoSolve
{
	[RequireComponent(typeof(FigureSpawner))]
	public class AutoSolver : MonoBehaviour
	{
		[SerializeField] private float cooldownBetweenEscape;

		private Task SolveTask;
		private FigureSpawner _figureSpawner;

		private void Awake()
		{
			_figureSpawner = GetComponent<FigureSpawner>();
		}

		public void Solve()
		{
			if (SolveTask != null && !SolveTask.IsCompleted) return;

			SolveTask = Solve(_figureSpawner.FiguresParent);
		}

		private async Task Solve(FiguresParent figuresParent)
		{
			foreach (Figure figure in figuresParent)
			{
				if (figure == null || figure.IsKnockedOut) continue;

				List<Figure> figuresToEscape = figuresParent.GetFiguresOnFiguresDirecion(figure).ToList();
				figuresToEscape.Add(figure);

				foreach (Figure figureToEscape in figuresToEscape)
				{
					figureToEscape.FigureGameObject.Escape();
					await Task.Delay((int)(cooldownBetweenEscape * 1000));
				}
			}
		}
	}
}

#endif