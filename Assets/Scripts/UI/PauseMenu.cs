using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool paused = false;
    [SerializeField] GameObject panel;
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if (paused)
            {
                Time.timeScale = 0;
                panel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                panel.SetActive(false);
            }
        }
    }
}
