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
        //Sospecho que esto me causó el error del extra gian
    }
}
