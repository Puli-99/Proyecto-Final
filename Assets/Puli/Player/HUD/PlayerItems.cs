using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [SerializeField] PlayerLife player;
    [SerializeField] int heal;
    [SerializeField] int extradefense;
    [SerializeField] int extradamage;
    [SerializeField] GameObject playerPanel;

    public void Heal()
    {
        player.AddHealth(heal);
    }
    
    public void Defense()
    {
        //Add Defense player.AddDefense(extradefense)
    }

    public void Weapons()
    {
        //Add Damage player.AddDamage(extradamage)
    }

    public void Back()
    {
        gameObject.SetActive(false);
        playerPanel.SetActive(true);
    }
}
