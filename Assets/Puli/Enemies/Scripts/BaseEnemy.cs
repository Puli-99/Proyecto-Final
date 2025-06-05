using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IDamageable, IKillable
{
    private IStrategy attackStrategy;
    [SerializeField] protected int health = 20;
    [SerializeField][Range(0, 20)] protected int damage;


    public void SetAttackStrategy(IStrategy newStrategy)
    {
        attackStrategy = newStrategy;
    }

    public void Attack()
    {
        attackStrategy.ExecuteAttack(/*Pasar parametros de IStrategy/SimpleAttack*/);
    }

    public void TakeDamage(int damage) //IDamageable Interface
    {
        health -= damage;
        Debug.Log(health);
        Die();
    }

    public void Die() //IKilleable Interface
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
 

    public void EnemyTurn(IDamageable target)
    {
        target.TakeDamage(damage);
        Debug.Log("Ataque Enemigo");
    }
}