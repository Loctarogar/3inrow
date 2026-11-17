using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public static class SaveManager
{
    private static string SaveFolder => Path.Combine(Application.persistentDataPath, "Saves");
    private static string GetSavePath(int slot)
    {
        return Path.Combine(SaveFolder, $"slot_{slot}.json");
    }
    public static void SaveGame(int slot, HeroData data)
    {
        if (slot < 0 || slot > 7 || data == null)
        {
            return;
        }
        Directory.CreateDirectory(SaveFolder);

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetSavePath(slot), json);

        Debug.Log("Game saved in slot " + slot);
    }

    public static HeroData LoadGame(int slot)
    {
        if (slot < 0 || slot > 7)
            return null;

        string path = GetSavePath(slot);

        if (!File.Exists(path))
            return null;

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<HeroData>(json);
    }

    public static bool SaveExists(int slot)
    {
        if (slot < 0 || slot > 7)
            return false;

        return File.Exists(GetSavePath(slot));
    }

    public static void DeleteSave(int slot)
    {
        if (slot < 0 || slot > 7)
            return;

        string path = GetSavePath(slot);

        if (File.Exists(path))
            File.Delete(path);
    }

    public static bool IsHeroCreated(string heroName)
    {
        for (int i = 0; i < 8; i++)
        {
            HeroData hero = LoadGame(i);

            if (hero != null && hero.heroName == heroName)
                return true;
        }

        return false;
    }

    public static int GetFirstFreeSlot()
    {
        for (int i = 0; i < 8; i++)
        {
            if (!SaveExists(i))
                return i;
        }

        return -1;
    }
}
