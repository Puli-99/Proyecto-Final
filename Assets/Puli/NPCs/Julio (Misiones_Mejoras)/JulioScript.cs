using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JulioScript : Interaction, IObserverNpcDialogue
{
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
            default:
                Debug.Log("Default");
                break;
        }
    }
}