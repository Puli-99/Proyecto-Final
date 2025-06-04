using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthBoard : MonoBehaviour, IObserver
{
    //Logical Vars
    int health = 100;

    //Reference Vars
    [SerializeField] TMP_Text healthText;

    void Start()
    {
    }

    public void OnNotify(PlayerDataContainer damage) //Given a Notification Type, sustracts the variable value type of the Notification
    {
        if (damage.Type == PlayerDataContainer.NotificationType.TookDamage)
        {
            health -= damage.Value;
            healthText.text = $"Health: {(health).ToString()}";
            if (health <= 0)
            {
                health = 100;
            }
        }

        else if (damage.Type == PlayerDataContainer.NotificationType.LifeHealed)
        {
            health += damage.Value;
            healthText.text = $"Health: {(health).ToString()}";
        }
    }

}
