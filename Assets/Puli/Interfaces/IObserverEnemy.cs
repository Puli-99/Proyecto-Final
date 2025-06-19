using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserverEnemy
{
    void OnNotify(BaseEnemy sourceEnemy, EnemyDataContainer typeOfValue);
}
