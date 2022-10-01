using System;
using GenerationData;
using LevelGeneration;
using UnityEngine;

namespace FigureGameObjects.InfinityLevel
{
	public class InfinityLevelPartGameObject
	{
		private readonly InfinityLevelFigureSpawner _infinityLevelSpawner;
		private readonly GameObject _infinityLevelPartGameObject;

		private InfinityLevelPartGameObject _child;

		private InfinityFiguresParent _infinityFiguresParent;

		public event Action<InfinityLevelPartGameObject> OnLevelComplete;

		public InfinityLevelPartGameObject(InfinityLevelFigureSpawner infinityLevelSpawner)
		{
			_infinityLevelSpawner = infinityLevelSpawner;
			_infinityLevelPartGameObject = new GameObject("InfinityLevelPart");
		}

		private Transform Transform => _infinityLevelPartGameObject.transform;

		public void SetParent(InfinityLevelPartGameObject parent)
		{
			parent._child = this;
			Transform.SetParent(parent.Transform, false);
		}

		public void GenerateFigures(LevelParameters levelParameters)
		{
			_infinityFiguresParent = new InfinityFiguresParent(levelParameters);
			_infinityFiguresParent.GenerateDirectedFigure();

			foreach (Figure figure in _infinityFiguresParent)
			{
				if (figure == null) continue;

				CubeGameObject cubeGameObject = _infinityLevelSpawner.InstantiateCube();
				cubeGameObject.transform.SetParent(_infinityLevelPartGameObject.transform, false);
				cubeGameObject.Initialize(figure);
			}

			_infinityFiguresParent.OnAllFiguresKnocked += () => OnLevelComplete?.Invoke(this);
		}

		public void UpChildInHierarchy()
		{
			_child._infinityLevelPartGameObject.transform.SetParent(_infinityLevelPartGameObject.transform.parent);
			_child.SetScaleWithCubeSize();
			_child = null;
		}

		public void SetScaleWithCubeSize()
		{
			Vector3 newScale = Vector3.one;
			if (_infinityLevelPartGameObject.transform.parent != null)
			{
				for (int axis = 0; axis < 3; axis++)
				{
					newScale[axis] = (float)(_infinityFiguresParent.Length[axis] - 2) /
					                 _infinityFiguresParent.Length[axis];
				}
			}

			_infinityLevelPartGameObject.transform.localScale = newScale;
		}
	}
}