using System;
using UnityEngine;

namespace GenerationData.States.CubeStates
{
	[Serializable]
	public class CubeStateMachine
	{
		protected State _nowState;
		protected Cube _cube;

		public CubeStateMachine(Cube cube)
		{
			_cube = cube;
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

		public void StartIdle() => _cube.FigurePhysics.NowSpeed = 0;

		public void StopIdle() => Debug.Log("StopIdle");

		public void StartShake() => Debug.Log("StartShake");

		public void StopShake() => Debug.Log("StopShake");

		public void StartEscape() => _cube.StartMoveForward();

		public void StopEscape() => _cube.StopMoveForward();

		public void StartReturn() => _cube.StartMoveBack();

		public void StopReturn() => _cube.StopMoveBack();
	}
}