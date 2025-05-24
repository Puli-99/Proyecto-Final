using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class HealthBoard : MonoBehaviour, IObserver
{
    //Logical Vars
    int health = 10000;

    //Reference Vars
    TMP_Text healthText;

    void Start()
    {
        healthText = GetComponent<TMP_Text>();
    }

    public void OnNotify(PlayerDataContainer damage) //Given a Notification Type, sustracts the variable value type of the Notification
    {
        if (damage.Type == PlayerDataContainer.NotificationType.TookDamage)
        {
            health -= damage.Value;
            healthText.text = $"Health: {(health / 100).ToString()}";
            if (health <= 0)
            {
                health = 10000;
            }
        }
    }
}
