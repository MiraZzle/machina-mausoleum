using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounterUI : MonoBehaviour
{
    [SerializeField] private GameObject scoreDisplay;
    private TMP_Text textScoreComp;
    void Start()
    {
        textScoreComp = scoreDisplay.GetComponent<TMP_Text>();
    }

    void Update()
    {
        textScoreComp.text = 'x' +PlayerStateTracker.enemiesKilled.ToString();
    }
}
