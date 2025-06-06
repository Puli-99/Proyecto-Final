using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthBoard : MonoBehaviour, IObserver
{
    //Logical Vars
    int health = 100;
    int defense = 100;

    //Reference Vars
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text defenseText;



    public void OnNotify(PlayerDataContainer amount)
    {
        if (amount.Type == PlayerDataContainer.NotificationType.TookDamage)
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

        else if (amount.Type == PlayerDataContainer.NotificationType.LifeHealed)
        {
            health += amount.Value;
            healthText.text = $"Vida: {(health).ToString()}";
        }

        else if (amount.Type == PlayerDataContainer.NotificationType.Defense)
        {
            defense += amount.Value;
            defenseText.text = $"Defensa: {(defense).ToString()}";
        }
    }

}
