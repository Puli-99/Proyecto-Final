using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IDamageable, IKillable
{
    private IStrategy attackStrategy;
    [SerializeField] protected int health;
    [SerializeField] protected int damage;


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
    }

    public void Die() //IKilleable Interface
    {
        if (health <= 0)
        {
            //Destruir/desactivar, etc
        }
    }

    private void OnCollisionStay(Collision collision) //Esta lógica es la que tengo del primer parcial, deberíamos definir cuándo queremos que haga el daño, si al hacer una animación
    {                                                // al colisionar, etc. Una vez definido, deberíamos implementar esa lógica y dejar/ajustar los damageable.

        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}