using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtons : MonoBehaviour
{
    [SerializeField] GameObject playerPanel;
    [SerializeField] GameObject itemPanel;
    [SerializeField] BaseEnemy enemy;


    public void Atack()
    {
        enemy.TakeDamage(10);
        Debug.Log("Atacar");
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
