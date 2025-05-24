using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour, IDamageable, IKillable
{
    public List<IObserver> observers = new List<IObserver>();

    int health = 100;

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

    public void TakeDamage(int damage)
    { 
        health -= damage;
        NotifyObservers(new PlayerDataContainer(PlayerDataContainer.NotificationType.TookDamage, damage));
    }

    public void Die()
    {

    }
}
