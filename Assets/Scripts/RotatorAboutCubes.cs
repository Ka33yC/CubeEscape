using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Transform))]
public class RotatorAboutCubes : MonoBehaviour
{
    [SerializeField] private InputEvents inputEvents;
    [SerializeField] private Transform target;
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
        Debug.Log(Physics.Raycast(ray, out var raycastHit, 2000) ? raycastHit.collider.gameObject.name : "None");
    }

    private void OnMouseMove(Vector3 mouseDelta)
    {
        mouseDelta *= sensitivity;

        var targetPosition = target.position;
        _transform.RotateAround(targetPosition, _transform.up, mouseDelta.x);
        _transform.RotateAround(targetPosition, _transform.right, -mouseDelta.y);
    }
}
