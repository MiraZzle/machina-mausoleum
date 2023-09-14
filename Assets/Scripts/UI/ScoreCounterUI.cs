using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounterUI : MonoBehaviour
{
    [SerializeField] private GameObject scoreDisplay;
    private TMP_Text textScoreComp;
    private char multiplierChar = 'x';
    void Start()
    {
        textScoreComp = scoreDisplay.GetComponent<TMP_Text>();
    }

    void Update()
    {
        DisplayScore();
    }

    private void DisplayScore()
    {
        textScoreComp.text = multiplierChar + PlayerStateTracker.enemiesKilled.ToString();
    }
}
