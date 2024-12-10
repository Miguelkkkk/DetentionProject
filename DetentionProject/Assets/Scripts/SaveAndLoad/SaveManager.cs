using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string saveFilePath = "playerData.json";

    // Salvar dados
    public static void SavePlayerData(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(GetSaveFilePath(), json);
        Debug.Log("Player data saved");
    }

    // Carregar dados
    public static PlayerData LoadPlayerData()
    {
        if (File.Exists(GetSaveFilePath()))
        {
            string json = File.ReadAllText(GetSaveFilePath());
            return JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            Debug.LogWarning("Save file not found!");
            return null;
        }
    }

    // Caminho para o arquivo de save
    private static string GetSaveFilePath()
    {
        return Path.Combine(Application.persistentDataPath, saveFilePath);
    }
}
