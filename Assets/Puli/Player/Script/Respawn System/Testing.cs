using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] PlayerLife playerLife;
    private Vector3 lastCheckpointPosition; // Guarda la �ltima posici�n del checkpoint alcanzado


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateCheckpoint();   
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Respawn();
        }
        
    }

    void CreateCheckpoint()
    {
        int items = GameManager.Instance != null ? GameManager.Instance.collectedItems : 0;
        SaveSystem.SaveGame(transform.position, items);
        lastCheckpointPosition = transform.position; // Guarda la posici�n del checkpoint
        playerLife.AddHealth(-35);
        Debug.Log(" Partida guardada en el checkpoint.");
    }

    public void Respawn()
    {
         if (lastCheckpointPosition != Vector3.zero)
         {
             GameObject Player = GameObject.FindGameObjectWithTag("Player");
             if (Player != null)
             {
                 Player.transform.position = lastCheckpointPosition;
                 Debug.Log(" Teletransportado al �ltimo checkpoint guardado.");
             }
         }
         else
         {
             Debug.Log(" No hay ning�n checkpoint guardado a�n.");
         }        
    }
}