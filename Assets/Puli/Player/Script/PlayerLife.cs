using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour, IDamageable, IKillable
{
    public List<IObserver> observers = new List<IObserver>();

    int health = 100;
    int defense = 100;

  
    public void RegisterObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }
    public void UnregisterObserver(IObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    void NotifyObservers(PlayerDataContainer typeOfValue)
    {
        foreach (IObserver observer in observers)
        {
            observer.OnNotify(typeOfValue);
        }
    }

    public void AddHealth(int amount)//Setter para agregar vida al usar el boton de curar
    {
        health += amount;
        NotifyObservers(new PlayerDataContainer(PlayerDataContainer.NotificationType.LifeHealed, amount));
        Debug.Log(health);
    }
    public void AddDefense(int amount) //Setter para agregar defensa al comprar en el Market
    {
        defense += amount;
        NotifyObservers(new PlayerDataContainer(PlayerDataContainer.NotificationType.Defense, amount));
        Debug.Log(defense);
    }

    public void TakeDamage(int damage)
    {
        int damageToHealth = Mathf.Max(damage - defense, 0);
        defense = Mathf.Max(defense - damage, 0);
        health -= damageToHealth;

        NotifyObservers(new PlayerDataContainer(PlayerDataContainer.NotificationType.TookDamage, damage));
        Die();
    }

    public void Die()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
