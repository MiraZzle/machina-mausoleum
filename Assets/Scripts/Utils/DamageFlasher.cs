using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlasher : MonoBehaviour
{
    // The material to apply when flashing on damage
    [SerializeField] private Material damageMaterial;
    [SerializeField] private Material normalMaterial;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float flashDuration = 0.15f;
    [SerializeField] private bool belongsToPlayer = false;

    private Coroutine flashCoroutine;

    void Start()
    {
        if (belongsToPlayer)
        {
            // Subscribe to damage and player death events for the player
            PlayerStateTracker.onDamageTaken += FlashOnDamage;
            PlayerStateTracker.playerDied += ResetMaterial;
        }

        normalMaterial = spriteRenderer.material;
    }

    public void FlashOnDamage()
    {
        // Stop the previous flash coroutine if it's still running
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

    private void ResetMaterial()
    {
        spriteRenderer.material = normalMaterial;
    }
}
