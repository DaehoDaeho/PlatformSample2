using UnityEngine;

/// <summary>
/// 저장 파일에 기록할 값들의 묶음.
/// 필드를 추가/삭제하면, 저장/불러오기 코드도 함께 업데이트해야 한다.
/// </summary>
[System.Serializable]
public class SaveData
{
    public string stageName;      // 현재 씬 이름
    public int coinCount;         // GameData.coinCount
    public bool hasCheckpoint;    // 체크포인트 보유 여부
    public float checkpointX;     // 체크포인트 좌표
    public float checkpointY;
}
