using UnityEngine;

[RequireComponent(typeof(Transform), typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Min(0)] private float minApproximate;
    [SerializeField, Range(0.01f, 1)] private float sensitivity;
    
    private Transform _transform;
    private Camera _camera;
    private float _maxApproximate;
    
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _camera = GetComponent<Camera>();
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
            Debug.Log(raycastHit.collider.name);
        }
    }

    private void ApproximateCamera(float scrollDelta)
    {
        Vector3 mousePosition = InputEvents.Instance.MousePosition;
        mousePosition.z = 1;

        Vector3 lookPoint = _transform.rotation * mousePosition;
        Vector3 newPosition = _transform.position + lookPoint * (scrollDelta * sensitivity);
        
        _transform.position = newPosition;
    }

    private void RotateCamera(Vector3 mouseDelta)
    {
        mouseDelta *= sensitivity;

        var targetPosition = target.position;
        _transform.RotateAround(targetPosition, _transform.up, mouseDelta.x);
        _transform.RotateAround(targetPosition, _transform.right, -mouseDelta.y);
    }

    public void SetSafetyPosition(Vector3 parentSize)
    {
        float halfOfParentSizeMagnitude = parentSize.magnitude * 0.5f;
        
        minApproximate = halfOfParentSizeMagnitude > minApproximate ? halfOfParentSizeMagnitude : minApproximate;
        _maxApproximate = minApproximate * 3;

        float mediumApproximate = Mathf.Clamp(0.5f, minApproximate, _maxApproximate);
        _transform.position = target.position + new Vector3(mediumApproximate, mediumApproximate, mediumApproximate);
        _transform.LookAt(target);
    }
}
