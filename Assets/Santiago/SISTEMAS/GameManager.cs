using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public int collectedItems = 0;
    public Vector3 returnPosition;

    public List<Enemigo> chasingEnemies = new List<Enemigo>();

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
