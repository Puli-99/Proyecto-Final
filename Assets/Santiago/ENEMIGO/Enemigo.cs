using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemigo : MonoBehaviour
{
    public float velocidad = 3f;
    public float rangoVision = 10f;
    public float rangoSonido = 5f;
    public string nombreEscenaCombate = "EscenaCombate"; // Define la escena de combate
    public Transform jugador;
    private bool persiguiendo = false;

    void Update()
    {
        DetectarJugador();
        
        // Movimiento hacia el jugador si está en persecución
        if (persiguiendo)
        {
            transform.position = Vector3.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);
        }
    }

    void DetectarJugador()
    {
        // Detección de visión
        Vector3 direccionJugador = jugador.position - transform.position;
        if (Vector3.Distance(transform.position, jugador.position) <= rangoVision)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direccionJugador.normalized, out hit, rangoVision))
            {
                if (hit.transform == jugador)
                {
                    persiguiendo = true;
                }
            }
        }

        // Detección de ruido
        if (Vector3.Distance(transform.position, jugador.position) <= rangoSonido)
        {
            persiguiendo = true;
        }
    }

    void OnTriggerEnter(Collider otro)
    {
        // Si el jugador entra en contacto con el enemigo, cambia a la escena de combate
        if (otro.CompareTag("Jugador"))
        {
            SceneManager.LoadScene(nombreEscenaCombate);
        }
    }
}