using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseEnemy : MonoBehaviour, IDamageable, IKillable
{
    public List<IObserverEnemy> observers = new List<IObserverEnemy>();


    private IStrategy attackStrategy;
    [SerializeField] protected int health = 20;
    [SerializeField] protected int damage;
    [SerializeField] protected int defense;
    [SerializeField] protected string enemyName;
    [SerializeField] protected string uniqueID;

    [SerializeField] GameObject player;
    [SerializeField] GameObject EnemyStatsHUD;


    //Getters para el CombatManager
    public int GetHealth() => health;
    public int GetDamage() => damage;
    public int GetDefense() => defense;


    public void RegisterObserver(IObserverEnemy observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }
    public void UnregisterObserver(IObserverEnemy observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    void NotifyObservers(EnemyDataContainer typeOfValue)
    {
        foreach (IObserverEnemy observer in observers)
        {
            observer.OnNotify(this, typeOfValue);
        }
    }

    public void Setup(EnemyData data)
    {
        this.enemyName = data.enemyName;
        this.health = data.health;
        this.damage = data.damage;
        this.defense = data.defense;
        this.uniqueID = data.uniqueID;
    }



    private void OnEnable() //Mostrar esto cuando se seleeccione un enemigo
    {
        transform.LookAt(player.transform);
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
        int damageToHealth = Mathf.Max(damage - defense, 0);
        defense = Mathf.Max(defense - damage, 0);
        health -= damageToHealth;
        NotifyObservers(new EnemyDataContainer(EnemyDataContainer.NotificationType.TookDamage, damage));
        Die();
    }

    public void Die() //IKilleable Interface
    {
        if (health <= 0)
        {
            //Falta desactivar también el botón del enemigo que muere
            EnemyStatsHUD.SetActive(false);
            GameManager.Instance.defeatedEnemies.Add(uniqueID);
            gameObject.SetActive(false);
        }
    }
    public void EnemyAttack(IDamageable target)
    {
        target.TakeDamage(damage);
    }    
}