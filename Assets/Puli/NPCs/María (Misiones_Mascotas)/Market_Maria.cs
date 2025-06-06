using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market_Maria : MonoBehaviour
{
    [SerializeField] Bank bank;
    [SerializeField] PlayerLife player;
    [SerializeField] GameObject pet;
    [SerializeField] int cost;

    public void BuyPet()
    {
        if (bank.CurrentBalance >= cost)
        {
            bank.Withdraw(cost);
            pet.SetActive(true);
        }
        else
        {
            Debug.Log("You dont have enough money");
        }
    }
}
