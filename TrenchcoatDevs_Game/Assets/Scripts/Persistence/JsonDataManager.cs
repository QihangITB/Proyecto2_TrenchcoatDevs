using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonDataManager : MonoBehaviour
{
    // Guarda un objeto como JSON en un archivo
    public static void SaveToJson<T>(T data, string fileName)
    {
        string json = JsonUtility.ToJson(data, true); // true para formato "pretty"
        string path = GetFilePath(fileName);
        File.AppendAllText(path, json);
        Debug.Log($"Datos guardados en: {path}");
    }

    // Carga un objeto desde un archivo JSON
    public static T LoadFromJson<T>(string fileName)
    {
        string path = GetFilePath(fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }
        else
        {
            Debug.LogWarning($"Archivo no encontrado: {path}");
            return default;
        }
    }

    // Verifica si el archivo existe
    public static bool FileExists(string fileName)
    {
        return File.Exists(GetFilePath(fileName));
    }

    // Ruta en el sistema dependiendo del dispositivo/plataforma
    private static string GetFilePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName + ".json");
    }
}
