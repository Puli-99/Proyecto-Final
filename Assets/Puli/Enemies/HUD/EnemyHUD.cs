using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHUD : MonoBehaviour, IObserverEnemy
{
    //Reference Vars
    [SerializeField] BaseEnemy currentEnemy;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text defenseText;

    void Update()
    {
        //var selected = CombatManager.Instance.selectedEnemy;
        //if (selected != null && selected != currentEnemy)
        //{
        //    currentEnemy = selected;
        //}
    }
    public void OnNotify(BaseEnemy sourceEnemy, EnemyDataContainer amount)
    {
        if (amount.Type == EnemyDataContainer.NotificationType.TookDamage)
        {
            //currentEnemy = sourceEnemy;
            healthText.text = $"Vida: {currentEnemy.GetHealth()}";
            defenseText.text = $"Defensa: {currentEnemy.GetDefense()}";
            Debug.Log(currentEnemy.name);
            Debug.Log(currentEnemy.GetHealth());
            Debug.Log(currentEnemy.GetDefense());
        }
    }
}
