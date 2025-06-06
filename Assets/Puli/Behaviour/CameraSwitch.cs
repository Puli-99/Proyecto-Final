using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera camActual;
    [SerializeField] CinemachineVirtualCamera camNueva;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Aseg�rate de que el jugador tenga este tag
        {
            camActual.Priority = 10;
            camNueva.Priority = 20; // Prioridad m�s alta para activarse
        }
    }
}
