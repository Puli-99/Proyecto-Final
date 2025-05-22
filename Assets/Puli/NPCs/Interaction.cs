using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Interacción");
                //Interaction Logic
            }
        }
    }
}