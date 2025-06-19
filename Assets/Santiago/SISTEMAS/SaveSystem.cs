using System.IO;
using UnityEngine;

public class SaveSystem {
    public static void SaveGame(Vector3 playerPosition, int collectedItems, string sceneName) {
        GameData data = new GameData {
            playerX = playerPosition.x,
            playerY = playerPosition.y,
            playerZ = playerPosition.z,
            collectedItems = collectedItems,
            sceneName = sceneName
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        Debug.Log("📂 Partida guardada en: " + Application.persistentDataPath + "/savefile.json");
    }

    public static GameData LoadGame() {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            Debug.Log("📂 Cargando datos: " + json);
            return JsonUtility.FromJson<GameData>(json);
        }

        Debug.Log("⚠️ No se encontró un archivo de guardado.");
        return null;
    }
}