using Game;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    private float targetZoom;
    private Vector3 targetPosition;

    // Zoom
    [SerializeField] private float zoomFactor;
    [SerializeField] private float zoomLerpSpeed;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    // Move
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private float maxX;
    [SerializeField] private float maxY;
    [SerializeField] private float minX;
    [SerializeField] private float minY;


    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
        targetPosition = cam.transform.position;
    }

    void LateUpdate()
    {
        Zoom();
        Move();
    }

    void Zoom()
    {
        var scroll = (int) GameManager.instance.controls.LevelDesign.Zoom.ReadValue<float>();
        targetZoom -= scroll * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
    }

    void Move()
    {
        var move = GameManager.instance.controls.LevelDesign.Move.ReadValue<Vector2>() * moveSpeed;
        targetPosition += new Vector3(move.x, move.y, 0);
        targetPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minX, maxX),
            Mathf.Clamp(targetPosition.y, minY, maxY),
            targetPosition.z);
        cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, Time.deltaTime * lerpSpeed);
    }
}