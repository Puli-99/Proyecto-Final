using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemigo : MonoBehaviour
{
    public float velocidad = 3f;
    public float rangoVision = 10f;
    public float rangoSonido = 5f;
    public string nombreEscenaCombate = "EscenaCombate"; // Define la escena de combate
    public Transform Player;
    private bool persiguiendo = false;

    void Update()
    {
        DetectarPlayer();
        
        // Movimiento hacia el jugador si está en persecución
        if (persiguiendo)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.position, velocidad * Time.deltaTime);
        }
    }

    void DetectarPlayer()
    {
        // Detección de visión
        Vector3 direccionPlayer = Player.position - transform.position;
        if (Vector3.Distance(transform.position, Player.position) <= rangoVision)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direccionPlayer.normalized, out hit, rangoVision))
            {
                if (hit.transform == Player)
                {
                    persiguiendo = true;
                }
            }
        }

        // Detección de ruido
        if (Vector3.Distance(transform.position, Player.position) <= rangoSonido)
        {
            persiguiendo = true;
        }
    }

    void OnTriggerEnter(Collider otro)
    {
        // Si el jugador entra en contacto con el enemigo, cambia a la escena de combate
        if (otro.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreEscenaCombate);
        }
    }
}