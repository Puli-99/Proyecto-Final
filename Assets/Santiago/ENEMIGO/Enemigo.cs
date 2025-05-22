using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float velocidad = 3f;
    public float rangoVision = 10f;
    public float rangoSonido = 5f;
    public Transform jugador;
    private bool persiguiendo = false;

    void Update()
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

        // Movimiento hacia el jugador si está en persecución
        if (persiguiendo)
        {
            transform.position = Vector3.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);
        }
    }
}
