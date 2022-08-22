using GenerationData;

namespace ActionStateMachines.States
{
	public class ReturningState : State
	{
		public override State HandleInput(FigureAction figureAction)
		{
			switch (figureAction)
			{
				case FigureAction.Idle:
					return new IdleState();
				default:
					return this;
			}
		}
		
		public override void EnterAction(CubeStateMachine cube)
		{
			cube.StartReturn();
		}

		public override void ExitAction(CubeStateMachine cube)
		{
			cube.StopReturn();
		}
	}
}