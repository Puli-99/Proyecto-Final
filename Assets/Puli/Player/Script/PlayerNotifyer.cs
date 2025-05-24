using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNotifyer : MonoBehaviour
{
    //Register observators to publishers

    public PlayerLife playerLife;
    public HealthBoard healthBoard;

    void Start()
    {
        playerLife = FindObjectOfType<PlayerLife>();
        healthBoard = FindObjectOfType<HealthBoard>();
        playerLife.RegisterObserver(healthBoard);
    }
}