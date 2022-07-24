using UnityEngine;

[RequireComponent(typeof(Transform))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private InputEvents inputEvents;
    [SerializeField] private Transform target;
    [SerializeField, Min(0)] private Vector3Int minSafetyPosition;
    [SerializeField, Range(0.01f, 1)] private float sensitivity;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Start()
    {
        inputEvents.OnMouseMove += OnMouseMove;
        inputEvents.RayOnTouch += OnTouch;
    }

    private void OnTouch(Ray ray)
    {
        //Debug.Log(Physics.Raycast(ray, out var raycastHit, 2000) ? raycastHit.collider.gameObject.name : "None");
    }

    private void OnMouseMove(Vector3 mouseDelta)
    {
        mouseDelta *= sensitivity;

        var targetPosition = target.position;
        _transform.RotateAround(targetPosition, _transform.up, mouseDelta.x);
        _transform.RotateAround(targetPosition, _transform.right, -mouseDelta.y);
    }

    public void SetSafetyPosition(Vector3 parentSize)
    {
        Vector3 middleOfParent = new Vector3((parentSize.x + 1) / 2, (parentSize.y + 1) / 2, (parentSize.z + 1) / 2);
        for (int dimension = 0; dimension < 3; dimension++)
        {
            middleOfParent[dimension] = middleOfParent[dimension] > minSafetyPosition[dimension]
                ? middleOfParent[dimension]
                : minSafetyPosition[dimension];
        }

        _transform.position = target.position + middleOfParent;
        _transform.LookAt(target);
    }
}
