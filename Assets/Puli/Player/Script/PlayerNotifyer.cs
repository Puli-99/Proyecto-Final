using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNotifyer : MonoBehaviour
{
    //Register observators to publishers

    public PlayerLife playerLife;
    public PlayerHealthBoard playerHealthBoard;

    void Start()
    {
        playerLife = FindObjectOfType<PlayerLife>();
        playerHealthBoard = FindObjectOfType<PlayerHealthBoard>();
        playerLife.RegisterObserver(playerHealthBoard);
    }
}