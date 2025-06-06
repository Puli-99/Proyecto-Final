using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MariaDialogue : Interaction, IObserverNpcDialogue
{
    [SerializeField] GameObject mariaMarket;


    private void OnEnable() //Debido a los bugs tuve que implementar esto para que se registren correctamente
    {
        Interaction interaction = GetComponent<Interaction>();
        if (interaction != null)
        {
            interaction.RegisterObserver(this);
        }
    }

    private void OnDisable()
    {
        Interaction interaction = GetComponent<Interaction>();
        if (interaction != null)
        {
            interaction.UnregisterObserver(this);
        }
    }

    public void OnNotify(DialogueEventType eventType)
    {
        switch (eventType)
        {
            case DialogueEventType.DialogueEnded:
                mariaMarket.SetActive(true);
                break;

            case DialogueEventType.DialogueExited:
                mariaMarket.SetActive(false);
                break;

            default:
                Debug.Log("Default");
                break;
        }
    }
}