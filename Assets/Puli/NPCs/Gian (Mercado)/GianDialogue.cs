using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GianDialogue : Interaction, IObserverNpcDialogue
{
    [SerializeField] GameObject gianMarket;

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
                gianMarket.SetActive(true);
                break;

            case DialogueEventType.DialogueExited:
                gianMarket.SetActive(false);
                break;

            default:
                Debug.Log("Default");
                break;
        }
    }
}