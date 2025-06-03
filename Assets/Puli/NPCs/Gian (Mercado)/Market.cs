using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    [SerializeField] Bank bank;
    [SerializeField] PlayerLife player;
    

    public void BuyArmor()
    {
        if (bank.CurrentBalance >= 10)
        {
            player.AddDefense(10);
            bank.Withdraw(15);
        }
        else
        {
            Debug.Log("You dont have enough money");
        }
    }
}