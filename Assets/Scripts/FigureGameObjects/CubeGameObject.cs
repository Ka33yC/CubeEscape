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
		[SerializeField] private float escapeDistance = 10;

		private FigurePhysics _figurePhysics;
		private CubeAnimator _cubeAnimator;

		private DirectedFigure _directedFigure;
		private CubeStateMachine _cubeStateMachine;

		private Vector3 _startPosition;

		private DirectedFigure DirectedFigure
		{
			get => _directedFigure;
			set
			{
				_directedFigure = value;
				_directedFigure.FigureGameObject = this;
				_startPosition = FigureCoordinatesToWorldPosition(_directedFigure.Parent,
					_directedFigure.CoordinatesInFiguresParent);
				_figurePhysics.Position = _startPosition;
				_figurePhysics.Rotation = _directedFigure.Direction.ToQuaternion();
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
			_figurePhysics.Position = _startPosition;
		}

		public void PlayShakeAnimation() => _cubeAnimator.PlayShakeAnimation();

		public void StopPlayShakeAnimation() => _cubeAnimator.StopShakeAnimation();

		public void StartMoveForward()
		{
			int axis = _directedFigure.Direction.GetAxisIndex();
			Vector3 endPosition = _startPosition;
			endPosition[axis] = FigureCoordinatesToWorldPosition(_directedFigure.Parent, _directedFigure.GetBorderPosition())[axis];
			endPosition += (Vector3)_directedFigure.Direction.ToVector() * escapeDistance;
				
			_figurePhysics.SetNowSpeedToStart();
			_figurePhysics.StartMoveTo(endPosition);
			_figurePhysics.OnPositionReach += () =>
			{
				DirectedFigure.KnockOut();
				Destroy(gameObject);
			};
		}

		public void StartMoveBack()
		{
			_figurePhysics.SetNowSpeedToStart();
			_figurePhysics.StartMoveTo(_startPosition);

			_figurePhysics.OnPositionReach += () => _cubeStateMachine.HandleInput(FigureAction.Idle);
		}

		public void StopMove()
		{
			_figurePhysics.StopMove();
		}

		public static Vector3 FigureCoordinatesToWorldPosition(FiguresParent figuresParent, Vector3Int coordinates)
		{
			float xCenterCoordinates = -((float)figuresParent.Length[0] - 1) / 2;
			float yCenterCoordinates = -((float)figuresParent.Length[1] - 1) / 2;
			float zCenterCoordinates = -((float)figuresParent.Length[2] - 1) / 2;

			return new Vector3(xCenterCoordinates + coordinates.x,
				yCenterCoordinates + coordinates.y, zCenterCoordinates + coordinates.z);
		}
	}
}