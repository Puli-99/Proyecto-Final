using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNotifyer : MonoBehaviour
{
    //Register observators to publishers

    public BaseEnemy baseEnemy;
    public EnemyHUD enemyHUD;

    void Start()
    {
        baseEnemy = FindObjectOfType<BaseEnemy>();
        enemyHUD = FindObjectOfType<EnemyHUD>();
        baseEnemy.RegisterObserver(enemyHUD);
    }
}
