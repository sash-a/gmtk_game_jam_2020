
using Game;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    private float targetZoom;

    [SerializeField] private float zoomFactor;
    [SerializeField] private float zoomLerpSpeed;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;

    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }

    void LateUpdate()
    {
        var scroll = (int) GameManager.instance.controls.LevelDesign.Zoom.ReadValue<float>();
        targetZoom -= scroll * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
    }
}