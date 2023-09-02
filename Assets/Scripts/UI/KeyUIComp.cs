using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUIComp : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

    }

    void Update()
    {
        if (PlayerStateTracker.keyObtained)
        {
            spriteRenderer.enabled = true;
        }
    }
}
