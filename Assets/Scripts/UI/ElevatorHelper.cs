using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorHelper : MonoBehaviour
{
    [SerializeField] private GameObject elevatorHandler;
    private bool entered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ElevatorManager elevatorManager = elevatorHandler.GetComponent<ElevatorManager>();
        if (collision.CompareTag("Player") && PlayerStateTracker.keyObtained)
        {
            if (!elevatorManager.elevatorEntry && !entered)
            {
                entered = true;
                elevatorManager.OpenElevator();
            }
        }
    }
}
