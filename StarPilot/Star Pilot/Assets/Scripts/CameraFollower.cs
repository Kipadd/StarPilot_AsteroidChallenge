using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollower : MonoBehaviour
{
    public GameObject playerShip;
    public Camera camera;
    public Vector3 cameraPosition;

    private void Start()
    {
        cameraPosition = camera.transform.position;
    }

    private void Update()
    {
        camera.transform.position = playerShip.transform.position + cameraPosition;
    }
}
