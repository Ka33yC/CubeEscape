#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using FigureGameObjects;
using GenerationData;
using UnityEngine;

namespace AutoSolve
{
	[RequireComponent(typeof(FigureSpawner))]
	public class AutoSolver : MonoBehaviour
	{
		[SerializeField] private float cooldownBetweenEscape;
		[SerializeField] private bool isSolve;
		
		private FigureSpawner _figureSpawner;
		
		private void Awake()
		{
			_figureSpawner = GetComponent<FigureSpawner>();
		}

		private void Update()
		{
			if(!isSolve) return;

			StartCoroutine(Solve(_figureSpawner.FiguresParent));
			isSolve = false;
		}

		public IEnumerator Solve(FiguresParent figuresParent)
		{
			foreach (Figure figure in figuresParent)
			{
				if(figure.IsKnockedOut) continue;
			
				IEnumerable<Figure> figuresToEscape = Solve(figure);
				foreach (Figure figureToEscape in figuresToEscape)
				{
					figureToEscape.FigureGameObject.Escape();
					yield return new WaitForSeconds(cooldownBetweenEscape);
				}
			}
		}

		private HashSet<Figure> Solve(Figure figureForSolve)
		{
			HashSet<Figure> escapeStack = new HashSet<Figure>();
			List<Figure> figuresOnDirection = new List<Figure>() { figureForSolve };
			
			for (int i = 0; i < figuresOnDirection.Count; i++)
			{
				if(figuresOnDirection[i].IsKnockedOut) continue;
				
				figuresOnDirection.AddRange(figuresOnDirection[i].GetFiguresOnDirection());
			}

			for (int i = figuresOnDirection.Count - 1; i >= 0; i--)
			{
				escapeStack.Add(figuresOnDirection[i]);
			}

			return escapeStack;
		}
	}
}

#endif