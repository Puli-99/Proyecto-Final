using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHUD : MonoBehaviour, IObserverEnemy
{
    //Logical Vars
    int health = 100;
    int defense = 100;

    //Reference Vars
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text defenseText;

    public void OnNotify(EnemyDataContainer amount)
    {
        if (amount.Type == EnemyDataContainer.NotificationType.TookDamage)
        {
            int damageToHealth = Mathf.Max(amount.Value - defense, 0);
            defense = Mathf.Max(defense - amount.Value, 0);
            health -= damageToHealth;
            if (health < 0)
            {
                health = 0;
            }

            healthText.text = $"Vida: {(health).ToString()}";
            defenseText.text = $"Defensa: {(defense).ToString()}";
        }
    }
}
