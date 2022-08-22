using System;

namespace GenerationData.States.CubeStates
{
	[Serializable]
	public class CubeStateMachine
	{
		protected State _nowState = new IdleState();

		public event Action OnIdleStart;
		public event Action OnIdleStop;
		public event Action OnShakeStart;
		public event Action OnShakeStop;
		public event Action OnEscapeStart;
		public event Action OnEscapeStop;
		public event Action OnReturnStart;
		public event Action OnReturnStop;

		public CubeStateMachine()
		{
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

		public void StartIdle() => OnIdleStart?.Invoke();

		public void StopIdle() => OnIdleStop?.Invoke();

		public void StartShake() => OnShakeStart?.Invoke();

		public void StopShake() => OnShakeStop?.Invoke();

		public void StartEscape() => OnEscapeStart?.Invoke();

		public void StopEscape() => OnEscapeStop?.Invoke();

		public void StartReturn() => OnReturnStart?.Invoke();

		public void StopReturn() => OnReturnStop?.Invoke();
	}
}