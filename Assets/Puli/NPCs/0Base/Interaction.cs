using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [Header("Referencias del Panel")]
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] UnityEngine.UI.Image npcImage;
    [SerializeField] TMP_Text npcName;
    [SerializeField] TMP_Text dialogueText;

    [Header("Variables Lógicas del diálogo")]
    [SerializeField] float textSpeed = 0.06f;
    [SerializeField] float nextTextSpeed = 2f;
    bool isPlayerClose = false;
    int index;
    bool isDialogueActive = false;

    [Header("Variables únicas para cada NPC")]
    [SerializeField] Sprite chrImage;
    [SerializeField] protected string[] dialogue;


    [HideInInspector]
    public enum DialogueEventType //Diferentes estados evento de dialogo para que los npcs sepan cuál NotifyObserver estan recibiendo.
    {
        DialogueExited, DialogueEnded, Example
    }


    //Patrón Observer para notificar a los scripts de los npcs cuando termina el dialogo.
    public List<IObserverNpcDialogue> observers = new List<IObserverNpcDialogue>();

    public void RegisterObserver(IObserverNpcDialogue observer)
    {
        GameObject observerGO = ((MonoBehaviour)observer).gameObject;

        if (observerGO != this.gameObject) //Evita que un Npc se registre en otro con otro nombre. Gian se bugeó y por alguna razón se registraba en maría
        {                                 //por lo que esto me sirve para que este bug no suceda
            return;
        }

        if (!observers.Contains(observer))
        {
            observers.Add(observer);
            //Debug.Log(observer); //Para saber si algun observer se metió en otro que no debía
        }
    }

    public void UnregisterObserver(IObserverNpcDialogue observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    public void NotifyObservers(DialogueEventType eventType)
    {
        foreach (IObserverNpcDialogue observer in observers)
        {
            observer.OnNotify(eventType);
        }
    }//Aca termina el patrón Observer.



    void Clear() //Limpia variables y valores
    {
        npcImage.sprite = null;
        dialogueText.text = "";
        npcName.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        isDialogueActive = false;
    }

    IEnumerator Typing() //Escribe letra por letra del diálogo y cuando termina de escribirse todo, espera un tiempo para pasar al siguiente texto
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(nextTextSpeed);
        NextLine();
    }

    void NextLine() //index = que parte del array de dialogos se esta mostrando. Si quedan dialogos, suma 1 a index para mostrar el siguiente dialogo borrando el anterior.
    {              //Si se mostraron todos entonces limpia.
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            Clear();
            NotifyObservers(DialogueEventType.DialogueEnded);
        }
    }

    void Interact() //Si el player esta cerca, no hay ningún dialogo activo y si pulsamos E entonces limpiamos el texto previo por si quedo algo, y luego activamos
                   //isDialogueActive así si el player pulsa muchas veces E no se bugea el texto, activamos el panel para que se muestren los textos e imagenes
                  //y le damos valor a esas imagenes y textos.
    {
        if (isPlayerClose && !isDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            if (dialoguePanel.activeInHierarchy)
            {
                Clear();
            }
            else
            {
                isDialogueActive = true;
                dialoguePanel.SetActive(true);
                npcImage.sprite = chrImage;
                npcName.text = gameObject.name;
                StartCoroutine(Typing());
            }
        }
    }

    private void Update()
    {
        Interact();
    }

    private void OnTriggerEnter(Collider other) //Cuando entra en rango del collider activa el bool para poder interactuar
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerClose = true;
        }
    }

    private void OnTriggerExit(Collider other) //Cuando sale del rango del collider limpia el texto y frena la generación de texto.
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Clear();
            StopAllCoroutines();
            isPlayerClose = false;
            NotifyObservers(DialogueEventType.DialogueExited);
        }
    }
}