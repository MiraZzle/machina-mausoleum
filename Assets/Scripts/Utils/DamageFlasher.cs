using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlasher : MonoBehaviour
{
    [SerializeField] private Material damageMaterial;
    [SerializeField] private Material normalMaterial;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float flashDuration;
    [SerializeField] private bool belongsToPlayer = false;

    private Coroutine flashCoroutine;


    void Start()
    {
        if (belongsToPlayer)
        {
            PlayerStateTracker.onDamageTaken += FlashOnDamage;
        }

        normalMaterial = spriteRenderer.material;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            FlashOnDamage();
        }
    }



    public void FlashOnDamage()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = damageMaterial;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.material = normalMaterial;

        flashCoroutine = null;
    }
}
