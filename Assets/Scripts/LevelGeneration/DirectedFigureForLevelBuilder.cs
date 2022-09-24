#if UNITY_EDITOR

using System;
using GenerationData;
using UnityEngine;

namespace LevelGeneration
{
	public class DirectedFigureForLevelBuilder : MonoBehaviour
	{
		public Direction Direction;
		[HideInInspector] public bool Exist;
		
		[SerializeField] private Material noneDirectionMaterial;
		[SerializeField] private Material anyDirectionMaterial;
		[SerializeField] private MeshRenderer meshRenderer;

		private Direction _directionInPreventFrame;

		private void Update()
		{
			if(_directionInPreventFrame == Direction) return;
			
			meshRenderer.transform.localRotation = Direction.ToQuaternion();
			_directionInPreventFrame = Direction;
			meshRenderer.material = Direction == Direction.None ? noneDirectionMaterial : anyDirectionMaterial;
		}

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