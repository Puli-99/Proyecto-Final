using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtons : MonoBehaviour
{
    [SerializeField] GameObject playerPanel;
    [SerializeField] GameObject itemPanel;
    [SerializeField] BaseEnemy enemy;
    [SerializeField] int damage;

    public void AddDamage(int amount)
    {
        damage += amount;
        Debug.Log("Aumento de daño, nuevo daño: " + damage);
    }

    public void Atack()
    {
        enemy.TakeDamage(damage);
        Debug.Log("Daño: " + damage);
    }

    public void Items()
    {
        playerPanel.SetActive(false);
        itemPanel.SetActive(true);
    }

    public void Talk()
    {
        Debug.Log("Hablar");
    }
}
