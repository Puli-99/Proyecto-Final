using UnityEngine;

public class Checkpoint : MonoBehaviour {
    private Vector3 lastCheckpointPosition; // Guarda la última posición del checkpoint alcanzado
    private bool PlayerDentro = false; // Verifica si el jugador está dentro del área

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            PlayerDentro = true; // Activa la detección cuando el jugador entra
            Debug.Log("🔹 Presiona 'E' para guardar el checkpoint.");
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            PlayerDentro = false; // Desactiva la detección cuando el jugador sale
        }
    }

    private void Update() {
        if (PlayerDentro && Input.GetKeyDown(KeyCode.E)) {
            int items = GameManager.Instance != null ? GameManager.Instance.collectedItems : 0;
            SaveSystem.SaveGame(transform.position, items, "mesi");
            lastCheckpointPosition = transform.position; // Guarda la posición del checkpoint
            Debug.Log("✅ Partida guardada en el checkpoint.");
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            if (lastCheckpointPosition != Vector3.zero) {
                GameObject Player = GameObject.FindGameObjectWithTag("Player");
                if (Player != null) {
                    Player.transform.position = lastCheckpointPosition;
                    Debug.Log("🚀 Teletransportado al último checkpoint guardado.");
                }
            } else {
                Debug.Log("⚠️ No hay ningún checkpoint guardado aún.");
            }
        }
    }
}
