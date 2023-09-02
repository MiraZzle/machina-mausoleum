using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMover : MonoBehaviour
{
    [SerializeField] private float freq = 0.6f;
    [SerializeField] private float amplitude = 0.2f;

    private float floatOffset;

    private Vector3 startPos;
    private Vector3 currentPos;

    void Start()
    {
        floatOffset = Random.Range(0, 180);
        startPos = transform.position;
    }

    void Update()
    {
        Hover();
    }

    private void Hover()
    {
        currentPos = startPos;
        currentPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * freq + floatOffset) * amplitude;
        transform.position = currentPos;
    }
}
