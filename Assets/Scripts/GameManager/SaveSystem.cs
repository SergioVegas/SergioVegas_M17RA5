using UnityEngine;

public static class SaveSystem
{
    private const string SaveKey = "SaveGame";

    public static void Save(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public static PlayerData Load()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);
            return JsonUtility.FromJson<PlayerData>(json);
        }
        return null;
    }

    public static void Clear()
    {
        PlayerPrefs.DeleteKey(SaveKey);
    }
}
