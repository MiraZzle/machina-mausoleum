using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform target;
    [SerializeField] private float damping;

    [SerializeField] Vector3 offset;


    private Vector3 velocity = Vector3.zero;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, damping);
    }
}
