using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemys ID", menuName = "Enemies ID System/Create new Enemy ID")] 
public class EnemiesID : ScriptableObject
{
    [Tooltip("Unique enemy ID")]
    public string enemyID;

    [Tooltip("Item Name")]
    public string enemyName;

    [Tooltip("Enemy Prefab")]
    public GameObject enemyPrefab;

    [TextArea(17, 1000)]
    public string comment = "Type coomments here";
}