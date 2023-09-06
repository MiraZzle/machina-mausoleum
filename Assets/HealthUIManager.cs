using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField] GameObject heartUIRef;
    private int maxHealth;

    List<HeartUI> hearts = new List<HeartUI>();
    void Start()
    {
        maxHealth = PlayerStateTracker.maxHealth;
        RedrawHearts();
        UpdateHearts();
        PlayerStateTracker.onDamageTaken += UpdateHearts;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDamage();
    }

    private void CheckDamage()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerStateTracker.ChangeHealth(-1);
        }
    }

    public void RedrawHearts()
    {
        ClearHearts();
        int heartCount = (maxHealth / 2) + (maxHealth % 2);

        for (int i = 0; i < heartCount; i++)
        {
            AddHeart();
        }
    }

    public void UpdateHearts()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            int heartFilling = (int)Mathf.Clamp(PlayerStateTracker.currentHealth - (i * 2), 0, 2);
            Debug.Log(heartFilling);

            hearts[i].SetState((HeartUI.heartStates)heartFilling);
        }
    }

    public void ClearHearts()
    {
        foreach (Transform heart in transform)
        {
            Destroy(heart);
        }
        hearts = new List<HeartUI>();
    }

    public void AddHeart()
    {
        GameObject heartInst = Instantiate(heartUIRef);
        heartInst.transform.SetParent(transform);

        HeartUI heartUI = heartInst.GetComponent<HeartUI>();
        heartUI.SetState(HeartUI.heartStates.empty);
        hearts.Add(heartUI);

    }
}
