using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LayerAnnouncer : MonoBehaviour
{
    [SerializeField] private TMP_Text textDisplay;
    [SerializeField] private string textPrefix = "Layer: ";
    void Start()
    {
        textDisplay = GetComponent<TMP_Text>();
        DisplayLayer();
    }

    private void DisplayLayer()
    {
        string layerText = '-' + textPrefix + LevelManager.currentLevel.ToString() + '-';
        textDisplay.text = layerText.ToUpper();
    }
}
