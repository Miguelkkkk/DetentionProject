using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string saveFilePath = "playerData.json";

    public static void SavePlayerData(PlayerData newPlayerData)
    {
        PlayerData existingPlayerData = LoadPlayerData();

        if (existingPlayerData != null)
        {
            foreach (var field in typeof(PlayerData).GetFields())
            {
                object newValue = field.GetValue(newPlayerData);
                if (newValue != null)
                {
                    field.SetValue(existingPlayerData, newValue);
                }
            }

            foreach (var property in typeof(PlayerData).GetProperties())
            {
                object newValue = property.GetValue(newPlayerData);
                if (newValue != null && property.CanWrite)
                {
                    property.SetValue(existingPlayerData, newValue);
                }
            }
        }
        else
        {
            existingPlayerData = newPlayerData;
        }

        string json = JsonUtility.ToJson(existingPlayerData, true);
        File.WriteAllText(GetSaveFilePath(), json);
        Debug.Log("Player data saved");
    }

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

    private static string GetSaveFilePath()
    {
        return Path.Combine(Application.persistentDataPath, saveFilePath);
    }
}
