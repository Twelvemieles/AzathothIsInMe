using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public ScoreData scoreData;
    public List<CardData> cards;
    public Vector2 cardsMatrix;
}
public  class SaveManager:MonoBehaviour
{
    private  string FileName = "save.json";

    public  void Save(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        string path = GetPath();

        try
        {
            File.WriteAllText(path, json);
            Debug.Log($"[SaveSystem] Saved in: {path}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[SaveSystem] Error saving: {ex.Message}");
        }
    }

    public  GameData Load()
    {
        string path = GetPath();

        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                GameData data = JsonUtility.FromJson<GameData>(json);
                return data;
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[SaveSystem] Error Loading: {ex.Message}");
            }
        }
        else
        {
            Debug.LogWarning("[SaveSystem] Can't find any saved files.");
        }

        return null;
    }

    private  string GetPath()
    {
        return string.Concat( Application.persistentDataPath,"/", FileName);
    }
}
