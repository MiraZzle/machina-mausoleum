using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : KeyPickup
{
    [SerializeField] private int minAmmo = 10;
    [SerializeField] private int maxAmmo = 60;

    private int healAmount = 2;

    // Heal player on pickup
    protected override void ActOnPickup()
    {
        PlayerStateTracker.ChangeHealth(healAmount);
    }
}
