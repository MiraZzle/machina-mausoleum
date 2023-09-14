using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float SmoothSpeed = 0.1f;

    private Transform player;

    // Offset between the camera and the player
    private Vector3 Offset = new Vector3(0, 0, -20);
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = player.position;
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }

    // Calculate the desired camera position nad smoothly move it
    private void MoveCamera()
    {
        Vector3 desiredPosition = player.position + Offset;
        Vector3 smoothedPosition =
        Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, SmoothSpeed);

        transform.position = smoothedPosition;
    }
}
