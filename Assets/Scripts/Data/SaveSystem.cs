using UnityEngine;
using System.IO;

/// <summary>
/// SaveData�� JSON���� �����ϰ� �ҷ����� ������ �ý���.
/// ���� ���: Application.persistentDataPath/save.json
/// </summary>
public static class SaveSystem
{
    private static string FilePath
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, "save.json");
        }
    }

    /// <summary>���� ��/����/üũ����Ʈ ���¸� �޾� ����.</summary>
    public static void SaveCurrent(string sceneName, int coinCount, bool hasCheckpoint, Vector2 checkpointPos)
    {
        SaveData data = new SaveData();
        data.stageName = sceneName;
        data.coinCount = coinCount;
        data.hasCheckpoint = hasCheckpoint;
        data.checkpointX = checkpointPos.x;
        data.checkpointY = checkpointPos.y;

        string json = JsonUtility.ToJson(data, true); // ���� ���� �鿩����
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
