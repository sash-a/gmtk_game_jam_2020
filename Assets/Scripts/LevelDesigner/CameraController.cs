using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.InputSystem.Controls;

public class CameraZoom : MonoBehaviour
{
    private Camera cam;
    private float targetZoom;
    private LevelDesignControls controls;

    [SerializeField] private float zoomFactor;
    [SerializeField] private float zoomLerpSpeed;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;

    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
        controls = GetComponent<LevelDesigner>().levelDesignControls;
    }

    void LateUpdate()
    {
        var scroll = (int) controls.LevelDesign.Zoom.ReadValue<float>();
        targetZoom -= scroll * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
    }
}