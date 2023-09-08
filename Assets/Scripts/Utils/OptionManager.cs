using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OptionManager
{
    [SerializeField] private static Texture2D cursorTexture;
    [SerializeField] private static CursorMode cursorMode = CursorMode.Auto;
    [SerializeField] private static Vector2 hotSpot = Vector2.zero;
    static void Start()
    {
    }
}
