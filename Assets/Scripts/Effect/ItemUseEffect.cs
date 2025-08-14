// Scripts/Effect/ItemUseEffect.cs
using UnityEngine;

public abstract class ItemUseEffect : ScriptableObject
{
    /// <summary>
    /// 아이템 사용 효과 실행.
    /// 반환값: 실제로 효과가 적용되었는지 (true면 인벤토리에서 소모 가능)
    /// </summary>
    public abstract bool Apply(GameObject user);
}
