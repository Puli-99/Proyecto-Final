using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNotifyer : MonoBehaviour
{
    //Register observators to publishers

  //  public EnemyHUD enemyHUD;
    public CombatManager combatManager;

    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
        // enemyHUD = FindObjectOfType<EnemyHUD>();
        BaseEnemy[] allEnemies = FindObjectsOfType<BaseEnemy>();
        foreach (var enemy in allEnemies)
        {
            enemy.RegisterObserver(combatManager);
        }
    }
}