using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class JsonDataManager
{
    private static readonly string SavePath = Application.persistentDataPath + "/Saves/";

    // Salvar um objeto como JSON
    public static void Save<T>(T data, string fileName)
    {
        // Garantir que o diretório de salvamento exista
        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }

        string filePath = SavePath + fileName + ".json";
        string jsonData = JsonUtility.ToJson(data, true); // 'true' para formatação legível

        File.WriteAllText(filePath, jsonData);

        Debug.Log($"Dados salvos em {filePath}");
    }

    // Carregar um objeto de JSON
    public static T Load<T>(string fileName)
    {
        string filePath = SavePath + fileName + ".json";

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            T data = JsonUtility.FromJson<T>(jsonData);

            Debug.Log($"Dados carregados de {filePath}");
            return data;
        }

        Debug.LogWarning($"Arquivo não encontrado: {filePath}");
        return default;
    }

    // Excluir um arquivo de salvamento
    public static void DeleteSave(string fileName)
    {
        string filePath = SavePath + fileName + ".json";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log($"Arquivo deletado: {filePath}");
        }
        else
        {
            Debug.LogWarning($"Arquivo não encontrado: {filePath}");
        }
    }

    // Verificar se um arquivo de salvamento existe
    public static bool SaveExists(string fileName)
    {
        string filePath = SavePath + fileName + ".json";
        return File.Exists(filePath);
    }
}

