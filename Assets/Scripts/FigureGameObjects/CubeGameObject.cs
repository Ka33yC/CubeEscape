using FigureGameObjects.Animations;
using FigureGameObjects.States;
using FigureGameObjects.States.CubeStates;
using GenerationData;
using UnityEngine;

namespace FigureGameObjects
{
	[RequireComponent(typeof(FigurePhysics), typeof(CubeAnimator))]
	public class CubeGameObject : MonoBehaviour, IFigureGameObject
	{
		[SerializeField] private CubeAnimatorParameters cubeAnimatorParameters;
		[SerializeField] private Transform cubeMesh;
		
		private FigurePhysics _figurePhysics;
		private CubeAnimator _cubeAnimator;
		
		private DirectedFigure _directedFigure;
		private CubeStateMachine _cubeStateMachine;

		private DirectedFigure DirectedFigure
		{
			get => _directedFigure;
			set
			{
				_directedFigure = value;
				_directedFigure.FigureGameObject = this;
				transform.position = _directedFigure.StartPosition;
				transform.rotation = _directedFigure.Direction.ToQuaternion();
			}
		}

		private void Awake()
		{
			_figurePhysics = GetComponent<FigurePhysics>();
			_cubeAnimator = new CubeAnimator(cubeMesh, cubeAnimatorParameters);
			_figurePhysics.OnCollision += Collide;
		}

		private void Start()
		{
			_cubeStateMachine = new CubeStateMachine(this);
		}

		public void Escape() => _cubeStateMachine.HandleInput(FigureAction.Escape);
		
		public void Collide(IFigureGameObject collideWith) => _cubeStateMachine.HandleInput(FigureAction.Collision);

		public void Initialize(Figure cube)
		{
			DirectedFigure = (DirectedFigure)cube;
		}

		public void StartIdle()
		{
			_figurePhysics.NowSpeed = 0;
			_figurePhysics.Position = DirectedFigure.StartPosition;
		}

		public void PlayShakeAnimation() => _cubeAnimator.PlayShakeAnimation();
		
		public void StopPlayShakeAnimation() => _cubeAnimator.StopShakeAnimation();
		
		public void StartMoveForward()
		{
			_figurePhysics.SetNowSpeedToStart();
			_figurePhysics.StartMoveTo(_directedFigure.StartPosition + _directedFigure.DirectionVector3 * 20);
			_figurePhysics.OnPositionReach += () => _directedFigure.KnockOut();
		}

		public void StartMoveBack()
		{
			_figurePhysics.SetNowSpeedToStart();
			_figurePhysics.StartMoveTo(_directedFigure.StartPosition);
			
			_figurePhysics.OnPositionReach += () => _cubeStateMachine.HandleInput(FigureAction.Idle);
		}
		
		public void StopMove()
		{
			_figurePhysics.StopMove();
		}
	}
}