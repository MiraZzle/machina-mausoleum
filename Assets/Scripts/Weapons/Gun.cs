using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Gun : MonoBehaviour
{
    [SerializeField] private SpriteRenderer gunSprite;
    public Sprite gunUISprite;

    private float shootPosVert;

    private Vector3 mousePosition;

    void Start()
    {

    }
    void Update()
    {
        RotateTowardsMouse();
    }

    private void FixedUpdate()
    {
        
    }

    private void RotateTowardsMouse()
    {

        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 rotationTarget = mousePosition - transform.position;

        float angle = Mathf.Atan2(rotationTarget.y, rotationTarget.x) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rot;


        if (mousePosition.x > transform.position.x)
        {
            gunSprite.transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            gunSprite.transform.localScale = new Vector3(1,-1,1);
        }
    }

}
