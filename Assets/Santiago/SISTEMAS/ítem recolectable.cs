using UnityEngine;

public class ItemRecolectable : MonoBehaviour
{
    public string nombreItem;
    public int valor = 1;
    private bool dentroRango = false; // Variable para saber si el jugador está cerca

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Jugador"))

        {
            dentroRango = true;
        }
    }

    void OnTriggerExit(Collider otro)
    {
        if (otro.CompareTag("Jugador"))

        {
            dentroRango = false;
        }
    }

    void Update()
    {
        if (dentroRango && Input.GetKeyDown(KeyCode.E))
        {
            Inventario.instancia.AgregarItem(nombreItem, valor);
            Destroy(gameObject); // Eliminar el ítem de la escena al recogerlo
        }
    }
}
