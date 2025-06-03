using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interaction;

public interface IObserverNpcDialogue
{
    void OnNotify(DialogueEventType eventType); //Avisa que tipo de evento del enum se activo para luego informarle al observer correspondiente
}
