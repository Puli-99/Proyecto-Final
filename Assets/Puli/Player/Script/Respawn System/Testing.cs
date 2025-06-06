using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] PlayerLife playerLife;
    private Vector3 lastCheckpointPosition; // Guarda la última posición del checkpoint alcanzado


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
        lastCheckpointPosition = transform.position; // Guarda la posición del checkpoint
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
                 Debug.Log(" Teletransportado al último checkpoint guardado.");
             }
         }
         else
         {
             Debug.Log(" No hay ningún checkpoint guardado aún.");
         }        
    }
}