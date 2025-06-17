using System.IO;
using UnityEngine;

public class Hability : MonoBehaviour
{
    void Start()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            GameData data = SaveSystem.LoadGame();
            if (data != null)
            {
                transform.position = new Vector3(data.playerX, data.playerY, data.playerZ);
                Debug.Log(" Partida cargada desde el �ltimo checkpoint.");
            }
        }
        else
        {
            Debug.Log(" No se encontr� archivo de guardado, iniciando nueva partida.");
        }
    }
}