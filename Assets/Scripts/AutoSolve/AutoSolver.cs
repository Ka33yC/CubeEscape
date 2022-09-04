#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
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
				if (figure.IsKnockedOut) continue;

				HashSet<Figure> figuresToEscape = figuresParent.GetFiguresOnFiguresDirecion(figure);
				figuresToEscape.Add(figure);

				foreach (Figure figureToEscape in figuresToEscape)
				{
					figureToEscape.FigureGameObject.Escape();
					await WaitFor((int)(cooldownBetweenEscape * 1000));
				}
			}
		}

		private async Task WaitFor(int millieseconds)
		{
			Task waitTask = Task.Run(() => Thread.Sleep(millieseconds));
			while (!waitTask.IsCompleted)
			{
				await Task.Yield();
			}
		}
	}
}

#endif