using UnityEngine;

public class Checkpoint : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            int items = GameManager.Instance != null ? GameManager.Instance.collectedItems : 0;
            SaveSystem.SaveGame(other.transform.position, items);
            Debug.Log("Checkpoint alcanzado. Partida guardada.");
        }
    }
}