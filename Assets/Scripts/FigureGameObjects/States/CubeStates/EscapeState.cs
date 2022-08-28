namespace GenerationData.States.CubeStates
{
	public class EscapingState : State
	{
		public override State HandleInput(FigureAction figureAction)
		{
			switch (figureAction)
			{
				case FigureAction.Collision:
					return new ReturningState();
				default:
					return this;
			}
		}
		
		public override void EnterAction(CubeStateMachine cube)
		{
			cube.StartEscape();
		}
		
		public override void ExitAction(CubeStateMachine cube)
		{
			cube.StopEscape();
		}
	}
}