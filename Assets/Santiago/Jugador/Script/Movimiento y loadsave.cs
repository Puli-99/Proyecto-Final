using System.IO;
using UnityEngine;

public class Movimiento : MonoBehaviour {
    public float velocidad = 5f;
    public float fuerzaSalto = 5f;
    private Rigidbody rb;

 void Start() {
{
    rb = GetComponent<Rigidbody>();
}

    string path = Application.persistentDataPath + "/savefile.json";

    if (File.Exists(path)) {
        GameData data = SaveSystem.LoadGame();
        if (data != null) {
            transform.position = new Vector3(data.playerX, data.playerY, data.playerZ);
            Debug.Log("📂 Partida cargada desde el último checkpoint.");
        }
    } else {
        Debug.Log("⚠️ No se encontró archivo de guardado, iniciando nueva partida.");
    }
}


    void Update() {
        // Movimiento horizontal y vertical
        float movX = Input.GetAxis("Horizontal");
        float movZ = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movX, 0f, movZ) * velocidad;
        rb.velocity = new Vector3(movimiento.x, rb.velocity.y, movimiento.z);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }
}
