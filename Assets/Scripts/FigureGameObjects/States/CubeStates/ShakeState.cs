namespace GenerationData.States.CubeStates
{
	public class ShakingState : State
	{
		public override State HandleInput(FigureAction figureAction)
		{
			switch (figureAction)
			{
				case FigureAction.Idle:
					return new IdleState();
				case FigureAction.Collision:
					return new ShakingState();
				case FigureAction.Escape:
					return new EscapingState();
				default:
					return this;
			}
		}
		
		public override void EnterAction(CubeStateMachine cube)
		{
			cube.StartShake();
		}
		
		public override void ExitAction(CubeStateMachine cube)
		{
			cube.StopShake();
		}
	}
}