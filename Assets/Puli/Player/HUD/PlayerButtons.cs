using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerButtons : MonoBehaviour
{
    [SerializeField] GameObject playerPanel;
    [SerializeField] GameObject itemPanel;
    [SerializeField] BaseEnemy enemy;
    [SerializeField] int damage;
    [SerializeField] TMP_Text damageText;


    private void OnEnable()
    {
        damageText.text = $"Da�o: {(damage).ToString()}";
    }
    public void AddDamage(int amount)
    {
        damage += amount;
        damageText.text = $"Da�o: {(damage).ToString()}";
    }

    public void Atack()
    {
        enemy.TakeDamage(damage);
        Debug.Log("Da�o: " + damage);
    }

    public void Items()
    {
        playerPanel.SetActive(false);
        itemPanel.SetActive(true);
    }

    public void Talk()
    {
        SceneManager.LoadScene(2);
    }
}
