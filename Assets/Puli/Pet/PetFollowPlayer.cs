using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PetFollowPlayer : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] Transform Player;
    [SerializeField] float distance;

    void Update()
    {
        // Calcula la dirección entre el jugador y la mascota
        Vector3 direccion = (Player.position - transform.position).normalized;

        // La nueva posición de la mascota, manteniendo la distancia
        Vector3 destino = Player.position - direccion * distance;

        // Mueve a la mascota hacia el destino
        transform.position = Vector3.Lerp(transform.position, destino, speed * Time.deltaTime);
    }
}
