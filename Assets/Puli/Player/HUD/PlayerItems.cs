using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    //[SerializeField] CombatManager combatManager;
    [SerializeField] PlayerLife player;
    [SerializeField] int heal;
    [SerializeField] int extradefense;
    [SerializeField] int extradamage;

    public void Heal()
    {
        player.AddHealth(heal);
    }
    
    public void Armor()
    {
        //Add Defense player.AddDefense(extradefense)
        Debug.Log("MoreDamage");
    }

    public void Weapons()
    {
        Debug.Log("MoreArmor");
        //Add Damage player.AddDamage(extradamage)
    }
}
