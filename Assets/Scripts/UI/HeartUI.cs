using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;

    [SerializeField] private Image imageRef;

    public enum heartStates
    {
        empty = 0,
        half = 1,
        full = 2
    }
    void Start()
    {
        imageRef = GetComponent<Image>();
    }

    // Match heart sprite according to value
    public void SetState(heartStates state)
    {
        switch (state)
        {
            case heartStates.full:
                imageRef.sprite = fullHeart;
                break;
            case heartStates.half:
                imageRef.sprite = halfHeart;
                break;
            case heartStates.empty:
                imageRef.sprite = emptyHeart;
                break;
        }
    }
}
