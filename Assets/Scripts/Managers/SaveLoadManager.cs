using Newtonsoft.Json;
using System;
using UnityEngine;

public class SaveLoadManager
{
    private static readonly string SaveLoadKey = "Player";

    public static void Save(Player player)
    {
        var jsonStr = JsonConvert.SerializeObject(player);
        var encryptedJsonStr = EncryptManager.EncryptString(jsonStr);

        PlayerPrefs.SetString(SaveLoadKey, encryptedJsonStr);
    }

    public static void Load(Action<Player> loadAction, Action failedAction)
    {
        var encryptedJsonStr = PlayerPrefs.GetString(SaveLoadKey, string.Empty);

        if (string.IsNullOrEmpty(encryptedJsonStr))
        {
            failedAction?.Invoke();

            return;
        }

        var jsonStr = EncryptManager.DecryptString(encryptedJsonStr);

        Player player = null;

        try
        {
            player = JsonConvert.DeserializeObject<Player>(jsonStr);
        }
        catch
        {
            failedAction?.Invoke();

            return;
        }

        if (player == null)
        {
            failedAction?.Invoke();

            return;
        }

        loadAction?.Invoke(player);
    }
}
