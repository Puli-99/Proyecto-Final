using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int damage;



    void TakeDamage(int damage)
    {
        health -= damage;
    }
}
