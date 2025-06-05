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


    public void OnNotify(PlayerDataContainer damage) //Given a Notification Type, sustracts the variable value type of the Notification
    {
        if (damage.Type == PlayerDataContainer.NotificationType.TookDamage)
        {
            int damageToHealth = Mathf.Max(damage.Value - defense, 0);
            defense = Mathf.Max(defense - damage.Value, 0);
            health -= damageToHealth;
            if (health < 0)
            {
                health = 0;
            }

            healthText.text = $"Vida: {(health).ToString()}";
            defenseText.text = $"Defensa: {(defense).ToString()}";
        }

        else if (damage.Type == PlayerDataContainer.NotificationType.LifeHealed)
        {
            health += damage.Value;
            healthText.text = $"Health: {(health).ToString()}";
        }

        else if (damage.Type == PlayerDataContainer.NotificationType.Defense)
        {
            defense += damage.Value;
            defenseText.text = $"Defensa: {(defense).ToString()}";
        }
    }

}
