using UnityEngine;
using System.IO;

/// <summary>
/// SaveData를 JSON으로 저장하고 불러오는 간단한 시스템.
/// 파일 경로: Application.persistentDataPath/save.json
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

    /// <summary>현재 씬/코인/체크포인트 상태를 받아 저장.</summary>
    public static void SaveCurrent(string sceneName, int coinCount, bool hasCheckpoint, Vector2 checkpointPos)
    {
        SaveData data = new SaveData();
        data.stageName = sceneName;
        data.coinCount = coinCount;
        data.hasCheckpoint = hasCheckpoint;
        data.checkpointX = checkpointPos.x;
        data.checkpointY = checkpointPos.y;

        string json = JsonUtility.ToJson(data, true); // 보기 좋게 들여쓰기
        File.WriteAllText(FilePath, json);

        Debug.Log("저장 완료: " + FilePath);
    }

    /// <summary>파일이 있으면 SaveData를 반환, 없으면 null.</summary>
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

    /// <summary>저장 파일 삭제.</summary>
    public static void Delete()
    {
        if (File.Exists(FilePath) == true)
        {
            File.Delete(FilePath);
        }
    }
}
