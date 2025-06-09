using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BaseEnemy : MonoBehaviour, IDamageable, IKillable
{
    private IStrategy attackStrategy;
    [SerializeField] protected int health = 20;
    [SerializeField] protected int damage;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text damageText;
    [SerializeField] TMP_Text defenseText;


    public string enemyName;
    public int defense;


    public void Setup(EnemyData data)
    {
        this.enemyName = data.enemyName;
        this.health = data.health;
        this.damage = data.damage;
        this.defense = data.defense;
    }



    private void OnEnable()
    {
        DisplayDamage();
        DisplayHealth();
    }

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
        DisplayHealth();
        Die();
    }

    public void Die() //IKilleable Interface
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void DisplayHealth()
    {
        healthText.text = ("Vida : " + health);
        if (health < 0)
        {
            healthText.text = ("Vida : 0");
        }
    }

    void DisplayDamage()
    {
        damageText.text = ("Daño : " + damage);
    }

    public void EnemyAttack(IDamageable target)
    {
        target.TakeDamage(damage);
    }

    
}