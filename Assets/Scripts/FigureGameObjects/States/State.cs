using GenerationData.States.CubeStates;

namespace GenerationData.States
{
	public abstract class State
	{
		public abstract State HandleInput(FigureAction figureAction);

		public virtual void EnterAction(CubeStateMachine cubeStateMachine)
		{
		}
		
		public virtual void ExitAction(CubeStateMachine cubeStateMachine)
		{
		}
	}
}