using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNotifyer : MonoBehaviour
{
    //Register observators to publishers

    public Interaction interaction;
    public GianDialogue gianDialogue;

    void Start()
    {
        //interaction = FindObjectOfType<Interaction>();
        //gianDialogue = FindObjectOfType<GianDialogue>();
        //interaction.RegisterObserver(gianDialogue);
        //Esto causa un bug en el cual gian se registra dos veces. De momento no lo boror por si lo necesito en un futuro, pero de momento no debe usarse este script.
    }
}
