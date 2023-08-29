using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;

    private Vector3 Offset = new Vector3(0, 0, -20);
    [SerializeField] private float SmoothSpeed = 0.1f;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = player.position;
    }

    void Update()
    {
        //transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + Offset;
        Vector3 smoothedPosition =
        Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, SmoothSpeed);

        transform.position = smoothedPosition;
    }
}
