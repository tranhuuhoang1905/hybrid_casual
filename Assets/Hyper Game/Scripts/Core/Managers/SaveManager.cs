using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string SavePath => Application.persistentDataPath + "/player_data.json";

    public static void SavePlayer(PlayerData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log("Saved to: " + SavePath);
    }

    public static PlayerData LoadPlayer()
    {
        // if (File.Exists(SavePath))
        // {
        //     string json = File.ReadAllText(SavePath);
        //     return JsonUtility.FromJson<PlayerData>(json);
        // }

        Debug.Log("No save found, creating new data");
        return new PlayerData();
    }
}