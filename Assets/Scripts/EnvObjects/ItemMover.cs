using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMover : MonoBehaviour
{
    [SerializeField] private float freq = 0.6f;
    [SerializeField] private float amplitude = 0.2f;

    private Vector3 startPos;
    private Vector3 currentPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        currentPos = startPos;
        currentPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * freq) * amplitude;
        transform.position = currentPos;
    }
}
