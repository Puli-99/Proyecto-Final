using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [Tooltip("Nivel a cargar, Fijarse en build settings")][SerializeField] int level;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            Debug.Log("Cambiar de nivel");
            SceneManager.LoadScene(level);
        }
    }
}
