using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNotifyer : MonoBehaviour
{
    //Register observators to publishers

    public PlayerHealth playerHealth;
    public HealthBoard healthBoard;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        healthBoard = FindObjectOfType<HealthBoard>();
        playerHealth.RegisterObserver(healthBoard);
    }
}