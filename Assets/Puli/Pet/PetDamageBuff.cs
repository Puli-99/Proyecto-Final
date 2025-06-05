using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PetDamageBuff : MonoBehaviour
{
    [Header("Insertar Player Panel")]
    [SerializeField] PlayerButtons playerbuttons;
    [Space(25)]
    [SerializeField] int damageBuff;

    private void OnEnable()
    {
        DamageBuff();
    }
    void DamageBuff()
    {
        playerbuttons.AddDamage(damageBuff);
    }
}
