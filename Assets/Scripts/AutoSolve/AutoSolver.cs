#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using FigureGameObjects;
using GenerationData;
using Unity.VisualScripting;
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

			StartCoroutine(Solve());
			isSolve = false;
		}

		public IEnumerator Solve()
		{
			FiguresParent figuresParent = _figureSpawner.FiguresParent;
			foreach (Figure figure in figuresParent)
			{
				if(figure.IsKnockedOut) continue;
			
				HashSet<Figure> figuresToEscape = figuresParent.GetFiguresOnFiguresDirecion(figure);
				figuresToEscape.Add(figure);
				
				foreach (Figure figureToEscape in figuresToEscape)
				{
					figureToEscape.FigureGameObject.Escape();
					yield return new WaitForSeconds(cooldownBetweenEscape);
				}
			}
		}
	}
}

#endif