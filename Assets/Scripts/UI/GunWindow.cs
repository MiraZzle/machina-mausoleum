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


    void Start()
    {
        weaponManager = GameObject.FindGameObjectWithTag("WeaponManager");
    }

    void Update()
    {
        currentGun = weaponManager.GetComponent<PlayerWeaponManager>().GetCurrentGun();

        gunMaxAmo = currentGun.GetComponent<GunShooting>().maxAmo;
        currentAmo = currentGun.GetComponent<GunShooting>().currentAmo;

        amoDisplay.text = GetAmoText();
        gunHolder.GetComponent<SpriteRenderer>().sprite = currentGun.GetComponent<Gun>().gunUISprite;
    }

    private string GetAmoText()
    {
        string amoText = currentAmo.ToString() + "/" + gunMaxAmo;
        return amoText;
    }
}
