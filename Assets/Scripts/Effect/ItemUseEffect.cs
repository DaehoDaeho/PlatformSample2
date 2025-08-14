// Scripts/Effect/ItemUseEffect.cs
using UnityEngine;

public abstract class ItemUseEffect : ScriptableObject
{
    /// <summary>
    /// ������ ��� ȿ�� ����.
    /// ��ȯ��: ������ ȿ���� ����Ǿ����� (true�� �κ��丮���� �Ҹ� ����)
    /// </summary>
    public abstract bool Apply(GameObject user);
}
