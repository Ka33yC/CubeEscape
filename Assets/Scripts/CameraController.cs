using FigureGameObjects;
using UnityEngine;

[RequireComponent(typeof(Transform), typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Min(0)] private float minApproximate;
    [SerializeField, Range(0.01f, 1)] private float sensitivity;
    [SerializeField, Range(0.01f, 10)] private float approximateSensitivity = 1;

    private Transform _transform;
    private Camera _camera;
    private float _minApproximate;
    private float _maxApproximate;

    private Vector3 _targetSize;

    public void Initialize(Vector3 targetSize)
    {
        _transform = GetComponent<Transform>();
        _camera = GetComponent<Camera>();

        SetSafetyPosition(targetSize);
    }

    private void Start()
    {
        InputEvents.Instance.OnMouseMove += RotateCamera;
        InputEvents.Instance.OnTouch += OnTouch;
        InputEvents.Instance.OnScroll += ApproximateCamera;
    }

    private void OnTouch(Vector3 mousePosition)
    {
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            CubeGameObject figure = raycastHit.collider.GetComponent<CubeGameObject>();
            figure.Escape();
        }
    }

    private void ApproximateCamera(float scrollDelta)
    {
        Vector3 mousePosition = InputEvents.Instance.MousePosition;
        mousePosition.z = 1;

        Vector3 lookPoint = _transform.rotation * mousePosition;
        Vector3 newPosition = _transform.position + lookPoint * (scrollDelta * approximateSensitivity);

        float magnitudeToTarget = (newPosition - target.position).magnitude;
        if(magnitudeToTarget > _maxApproximate || magnitudeToTarget < _minApproximate) return;
        
        _transform.position = newPosition;
    }

    private void RotateCamera(Vector3 mouseDelta)
    {
        mouseDelta *= sensitivity;

        var targetPosition = target.position;
        _transform.RotateAround(targetPosition, _transform.up, mouseDelta.x);
        _transform.RotateAround(targetPosition, _transform.right, -mouseDelta.y);
    }

    private void SetSafetyPosition(Vector3 targetSize)
    {
        _targetSize = targetSize;
        float halfOfParentSizeMagnitude = _targetSize.magnitude * 0.5f;

        _minApproximate = halfOfParentSizeMagnitude > minApproximate ? halfOfParentSizeMagnitude : minApproximate;
        _maxApproximate = _minApproximate * 4;

        float mediumApproximate = _minApproximate * 2;
        _transform.position = target.position + new Vector3(mediumApproximate, mediumApproximate, mediumApproximate);
        _transform.LookAt(target);
    }
}
