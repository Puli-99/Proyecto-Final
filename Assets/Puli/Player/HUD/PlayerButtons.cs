using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerButtons : MonoBehaviour
{
    [SerializeField] CombatManager combatManager;
    [SerializeField] GameObject playerPanel;
    [SerializeField] GameObject itemPanel;
    [SerializeField] int damage;
    public int GetDamage() => damage; //Getter para poder usar en CombatManager y atacar al enemigo seleccionado
    [SerializeField] TMP_Text damageText;


    private void OnEnable()
    {
        damageText.text = $"Daño: {(damage).ToString()}";
    }
    public void AddDamage(int amount)
    {
        damage += amount;
        damageText.text = $"Daño: {(damage).ToString()}";
    }

    public void Atack()
    {
        combatManager.PlayerDealsDamage();
        Debug.Log("Daño: " + damage);
    }

    public void Items()
    {
        playerPanel.SetActive(false);
        itemPanel.SetActive(true);
    }

    public void Talk()
    {
        GameManager.Instance.chasingEnemies.Clear();
        SceneManager.LoadScene(2);
    }
}
