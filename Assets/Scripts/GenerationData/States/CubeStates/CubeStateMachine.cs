﻿using System;
using FigureGameObjects;
using UnityEngine;

namespace GenerationData.States.CubeStates
{
	[Serializable]
	public class CubeStateMachine
	{
		protected State _nowState;
		protected CubeGameObject _cubeGameObject;

		public CubeStateMachine(CubeGameObject cubeGameObject)
		{
			_cubeGameObject = cubeGameObject;
			_nowState = new IdleState();
			_nowState.EnterAction(this);
		}
		
		public void HandleInput(FigureAction figureAction)
		{
			State newState = _nowState.HandleInput(figureAction);
			if(_nowState == newState) return;
				
			_nowState.ExitAction(this);
			_nowState = newState;
			_nowState.EnterAction(this);
		}

		public void StartIdle() => _cubeGameObject.StartIdleInBaseState();

		public void StopIdle() => Debug.Log("StopIdle");

		public void StartShake() => Debug.Log("StartShake");

		public void StopShake() => Debug.Log("StopShake");

		public void StartEscape() => _cubeGameObject.StartMoveForward();

		public void StopEscape() => _cubeGameObject.StopMoveForward();

		public void StartReturn() => _cubeGameObject.StartMoveBack();

		public void StopReturn() => _cubeGameObject.StopMoveBack();
	}
}