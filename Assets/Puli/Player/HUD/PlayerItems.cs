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
    
    public void Defense()
    {
        //Add Defense player.AddDefense(extradefense)
        Debug.Log("MoreDefense");
    }

    public void Weapons()
    {
        Debug.Log("MoreDamage");
        //Add Damage player.AddDamage(extradamage)
    }
}
