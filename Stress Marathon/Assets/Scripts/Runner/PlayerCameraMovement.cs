using System;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject _camaraPoint;
    Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        _camera.transform.position = _camaraPoint.transform.position;
    }
}
