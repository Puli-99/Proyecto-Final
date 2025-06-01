using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArielDialogue : Interaction, IObserverNpcDialogue
{
    [SerializeField] GameObject missionPanel;
    [SerializeField] TMP_Text missionText;


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

    public void OnNotify()
    {
        missionPanel.SetActive(true);        
    }
}