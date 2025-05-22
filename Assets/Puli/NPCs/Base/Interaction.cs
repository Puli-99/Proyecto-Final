using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] TMP_Text npcNameText;
    [SerializeField] protected string[] dialogue;
    int index;
    [SerializeField] float textSpeed;
    bool isPlayerClose = false;
    [SerializeField] float nextTextSpeed;
    [SerializeField] Sprite chrImage;
    [SerializeField] UnityEngine.UI.Image image;
    void ClearText()
    {
        image.sprite = null;
        dialogueText.text = "";
        npcNameText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(nextTextSpeed);
        NextLine();
    }

    void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            ClearText();
        }
    }

    void Interact()
    {
        if (isPlayerClose && Input.GetKeyDown(KeyCode.E))
        {
            if (dialoguePanel.activeInHierarchy)
            {
                ClearText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                image.sprite = chrImage;
                npcNameText.text = gameObject.name;
                StartCoroutine(Typing());
            }
        }
    }

    private void Update()
    {
        Interact();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ClearText();
            isPlayerClose = false;
        }
    }
}