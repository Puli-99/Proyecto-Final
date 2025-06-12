using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public static Inventario instancia; // Singleton para acceder al inventario fácilmente
    private Dictionary<string, int> items = new Dictionary<string, int>();

    void Awake()
    {
        instancia = this;
    }

    public void AgregarItem(string nombre, int cantidad)
    {
        if (items.ContainsKey(nombre))
        {
            items[nombre] += cantidad;
        }
        else
        {
            items.Add(nombre, cantidad);
        }

        Debug.Log($"Recolectaste {cantidad} {nombre}. Total: {items[nombre]}");
    }
}