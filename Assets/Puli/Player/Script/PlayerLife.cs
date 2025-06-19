using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour, IDamageable, IKillable
{    
    public List<IObserver> observers = new List<IObserver>();

    int health;
    int defense;
    public int GetHealth() => health;
    public int GetDefense() => defense;
    private void Awake()
    {
        LoadPlayerData(); // Cargar valores al iniciar la escena
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            Debug.Log(health);
        }
        Die();
    }

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

    public void AddHealth(int amount)
    {
        health += amount;
        NotifyObservers(new PlayerDataContainer(PlayerDataContainer.NotificationType.LifeHealed, amount));
        SavePlayerData();
        Debug.Log(health);
    }

    public void AddDefense(int amount)
    {
        defense += amount;
        NotifyObservers(new PlayerDataContainer(PlayerDataContainer.NotificationType.Defense, amount));
        SavePlayerData();
        Debug.Log(defense);
    }

    public void TakeDamage(int damage)
    {
        int damageToHealth = Mathf.Max(damage - defense, 0);
        defense = Mathf.Max(defense - damage, 0);
        health -= damageToHealth;

        NotifyObservers(new PlayerDataContainer(PlayerDataContainer.NotificationType.TookDamage, damage));
        SavePlayerData();
        Die();
    }

    public void Die()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    void SavePlayerData()
    {
        PlayerPrefs.SetInt("Health", health);
        PlayerPrefs.SetInt("Defense", defense);
        PlayerPrefs.Save();
    }

    void LoadPlayerData()
    {
        health = PlayerPrefs.GetInt("Health", 100);
        defense = PlayerPrefs.GetInt("Defense", 100);

        if (health <= 0) 
        {
            health = 100; // Resetear vida después de morir
            PlayerPrefs.SetInt("Health", health);
            PlayerPrefs.Save();
        }
    
        Debug.Log($"Vida cargada: {health}, Defensa cargada: {defense}");
    }

}