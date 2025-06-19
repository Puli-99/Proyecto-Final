using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour
{
    [SerializeField] PlayerLife playerLife;
    public Vector3 lastCheckpointPosition; // Guarda la última posición del checkpoint alcanzado

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
        string sceneName = GameManager.Instance != null ? GameManager.Instance.sceneToReturn : null;
        SaveSystem.SaveGame(transform.position, items, SceneManager.GetActiveScene().name);
        lastCheckpointPosition = transform.position; // Guarda la posición del checkpoint
        playerLife.AddHealth(-35);
        Debug.Log(" Partida guardada en el checkpoint.");
    }

    public void Respawn()
    {
        GameData data = SaveSystem.LoadGame();
        if (data != null)
        {
            if (lastCheckpointPosition == Vector3.zero) return;
            // Si estamos en otra escena, cargamos la escena guardada
            if (SceneManager.GetActiveScene().name != data.sceneName)
            {
                StartCoroutine(LoadSceneAndTeleport(data.sceneName, lastCheckpointPosition));
            }
            else
            {
                TeleportPlayer(lastCheckpointPosition);
            }
        }
        else
        {
            Debug.Log("No hay datos de guardado.");
        }
    }

    IEnumerator LoadSceneAndTeleport(string sceneName, Vector3 position)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
            yield return null;

        yield return new WaitForSeconds(0.1f); // pequeño delay para asegurarse de que la escena está lista

        TeleportPlayer(position);
    }

    void TeleportPlayer(Vector3 position)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.ResetPath();
            }

            player.transform.position = position;
            Debug.Log(" Teletransportado al último checkpoint guardado.");
        }
        else
        {
            Debug.LogError(" No se encontró el jugador en la escena.");
        }
    }
}