using System;
using GenerationData;
using UnityEngine;

namespace FigureGameObjects.InfinityLevel
{
	public class InfinityLevelPart
	{
		private readonly InfinityLevelFigureSpawner _infinityLevelSpawner;
		private readonly bool _isDifficult;
		private readonly GameObject _infinityLevelPartGameObject;

		private InfinityLevelPart _child;

		private InfinityFiguresParent _infinityFiguresParent;

		public event Action<InfinityLevelPart> OnLevelComplete;

		public InfinityLevelPart(InfinityLevelFigureSpawner infinityLevelSpawner, bool isDifficult)
		{
			_infinityLevelSpawner = infinityLevelSpawner;
			_isDifficult = isDifficult;
			_infinityLevelPartGameObject = new GameObject("InfinityLevelPart");
		}

		public Transform Transform => _infinityLevelPartGameObject.transform;

		public void SetParent(InfinityLevelPart parent)
		{
			parent._child = this;
			Transform.SetParent(parent.Transform, false);
		}

		public void GenerateFigures(Vector3Int cubeSize)
		{
			_infinityFiguresParent =
				new InfinityFiguresParent(new Figure[cubeSize.x, cubeSize.y, cubeSize.z], _isDifficult);
			_infinityFiguresParent.GenerateFigures();

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