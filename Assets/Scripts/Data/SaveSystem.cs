using UnityEngine;
using System.IO;

/// <summary>
/// SaveData�� JSON���� �����ϰ� �ҷ����� ������ �ý���.
/// ���� ���: Application.persistentDataPath/save.json
/// </summary>
public static class SaveSystem
{
    public static SaveData gameData = new SaveData();

    private static string FilePath
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, "save.json");
            // Application.persistentDataPath : ���� ������� ���. ����Ƽ���� �����ϴ� ���.
        }
    }

    /// <summary>���� ��/����/üũ����Ʈ ���¸� �޾� ����.</summary>
    public static void SaveCurrent(string sceneName, int coinCount, bool hasCheckpoint, Vector2 checkpointPos)
    {
        //SaveData data = new SaveData(); // Ŭ������ �����ڸ� �̿��ؼ� �޸� �Ҵ�.
        //data.stageName = sceneName;
        //data.coinCount = coinCount;
        //data.hasCheckpoint = hasCheckpoint;
        //data.checkpointX = checkpointPos.x;
        //data.checkpointY = checkpointPos.y;

        //string json = JsonUtility.ToJson(data, true); // ���� ���� �鿩����
        //File.WriteAllText(FilePath, json);

        //Debug.Log("���� �Ϸ�: " + FilePath);

        //SaveData data = new SaveData(); // Ŭ������ �����ڸ� �̿��ؼ� �޸� �Ҵ�.
        gameData.stageName = sceneName;
        gameData.coinCount = coinCount;
        gameData.hasCheckpoint = hasCheckpoint;
        gameData.checkpointX = checkpointPos.x;
        gameData.checkpointY = checkpointPos.y;

        string json = JsonUtility.ToJson(gameData, true); // ���� ���� �鿩����
        File.WriteAllText(FilePath, json);

        Debug.Log("���� �Ϸ�: " + FilePath);
    }

    public static void SaveVolume(float masterVolume, float bgmVolume, float sfxVolume)
    {
        gameData.masterVolume = masterVolume;
        gameData.bgmVolume = bgmVolume;
        gameData.sfxVolume = sfxVolume;

        string json = JsonUtility.ToJson(gameData, true); // ���� ���� �鿩����
        File.WriteAllText(FilePath, json);

        Debug.Log("���� �Ϸ�: " + FilePath);
    }

    /// <summary>������ ������ SaveData�� ��ȯ, ������ null.</summary>
    public static SaveData Load()
    {
        if (File.Exists(FilePath) == false)
        {
            return null;
        }

        string json = File.ReadAllText(FilePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        return data;
    }

    /// <summary>���� ���� ����.</summary>
    public static void Delete()
    {
        if (File.Exists(FilePath) == true)
        {
            File.Delete(FilePath);
        }
    }
}
