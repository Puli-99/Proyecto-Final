using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    Bank bank;

    void Awake()
    {
        bank = FindObjectOfType<Bank>();
    }


    public void BuyArmor()
    {
        if (bank.CurrentBalance >= 10)
        {
            bank.Withdraw(15);
            //Dar armadura
        }
        else
        {
            Debug.Log("You dont have enough money");
        }
    }
}