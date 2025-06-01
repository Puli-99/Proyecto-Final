using UnityEngine;

public class ItemRecolectable : MonoBehaviour
{
    public string nombreItem;
    public int valor = 1;
    private bool dentroRango = false; // Variable para saber si el jugador está cerca

    //References
    Bank bank;


    private void Start()
    {
        bank = FindObjectOfType<Bank>(); //Referencia del banco para acceder a sus metodos.
    }
    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Player"))
        {
            dentroRango = true;
        }
    }

    void OnTriggerExit(Collider otro)
    {
        if (otro.CompareTag("Player"))
        {
            dentroRango = false;
        }
    }

    public void GainMoney() //Si el item que agarramos es dinero, nos suma su valor al bank. Repetir esta función por cada item recolectable que haya.
    {
        if (bank == null) { return; }
        if (nombreItem == "Money")
        {
            bank.Deposit(valor);
        }
    }


    void Update()
    {
        if (dentroRango && Input.GetKeyDown(KeyCode.E))
        {
            Inventario.instancia.AgregarItem(nombreItem, valor);
            GainMoney();
            Destroy(gameObject); // Eliminar el ítem de la escena al recogerlo
        }
    }
}
