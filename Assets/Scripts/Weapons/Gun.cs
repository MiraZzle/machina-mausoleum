using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Gun : MonoBehaviour
{
    [SerializeField] private SpriteRenderer gunSprite;
    private GameObject player;
    private Vector3 mousePosition;

    public GameObject selfReference;
    public string gunName;
    public bool ownedByPlayer = true;
    public Sprite gunUISprite;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (!GameStateManager.gamePaused)
        {
            PassTarget();
        }
    }

    private void PassTarget()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (ownedByPlayer)
        {
            RotateTowardsTarget(mousePosition);
        }
        else
        {
            RotateTowardsTarget(player.transform.position);
        }
    }

    private void RotateTowardsTarget(Vector3 target)
    {
        Vector2 rotationTarget = target - transform.position;

        float angle = Mathf.Atan2(rotationTarget.y, rotationTarget.x) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rot;

        // Flip the gun sprite if the target is on the left side of the gun.

        if (target.x > transform.position.x)
        {
            gunSprite.transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            gunSprite.transform.localScale = new Vector3(1,-1,1);
        }
    }

}
