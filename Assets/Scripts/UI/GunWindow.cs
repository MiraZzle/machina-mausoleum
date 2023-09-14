using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunWindow : MonoBehaviour
{
    [SerializeField] private GameObject weaponManager;
    [SerializeField] private GameObject gunHolder;
    [SerializeField] private GameObject currentGun;
    [SerializeField] private TMP_Text amoDisplay;

    [SerializeField] private int gunMaxAmo;
    [SerializeField] private int currentAmo;

    private char slashChar = '/';

    void Start()
    {
        weaponManager = GameObject.FindGameObjectWithTag("WeaponManager");
    }

    void Update()
    {
        currentGun = weaponManager.GetComponent<PlayerWeaponManager>().GetCurrentGun();

        gunMaxAmo = currentGun.GetComponent<GunShooting>().maxAmo;
        currentAmo = currentGun.GetComponent<GunShooting>().currentAmo;

        // Update the ammo display text and the gun's UI sprite in the gun window
        amoDisplay.text = GetAmoText();
        gunHolder.GetComponent<SpriteRenderer>().sprite = currentGun.GetComponent<Gun>().gunUISprite;
    }

    // Generate the ammo text in the format "currentAmo / gunMaxAmo".
    private string GetAmoText()
    {
        string amoText = currentAmo.ToString() + slashChar + gunMaxAmo;
        return amoText;
    }
}
