using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStrategy
{
    public void ExecuteAttack(Transform enemyTransform, GameObject player);
}
