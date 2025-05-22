using UnityEngine;

public class CamaraTerceraPersona : MonoBehaviour
{
    public Transform jugador; // Referencia al jugador
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Ajuste de posición
    public float velocidadSuavizado = 5f; // Suavizado del movimiento

    void LateUpdate()
    {
        if (jugador != null)
        {
            // Calcula la posición deseada con offset
            Vector3 posicionDeseada = jugador.position + offset;
            // Suaviza la transición de la cámara
            transform.position = Vector3.Lerp(transform.position, posicionDeseada, velocidadSuavizado * Time.deltaTime);
            // La cámara mira al jugador
            transform.LookAt(jugador);
        }
    }
}
