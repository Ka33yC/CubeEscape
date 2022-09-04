namespace FigureGameObjects.States.CubeStates
{
	public class IdleState : State
	{
		public override State HandleInput(FigureAction figureAction)
		{
			switch (figureAction)
			{
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
			cube.StartIdle();
		}

		public override void ExitAction(CubeStateMachine cube)
		{
			cube.StopIdle();
		}
	}
}