using UnityEngine;

/// <summary>
/// ���� ���Ͽ� ����� ������ ����.
/// �ʵ带 �߰�/�����ϸ�, ����/�ҷ����� �ڵ嵵 �Բ� ������Ʈ�ؾ� �Ѵ�.
/// </summary>
[System.Serializable]
public class SaveData
{
    public string stageName;      // ���� �� �̸�
    public int coinCount;         // GameData.coinCount
    public bool hasCheckpoint;    // üũ����Ʈ ���� ����
    public float checkpointX;     // üũ����Ʈ ��ǥ
    public float checkpointY;
}
