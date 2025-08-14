// Scripts/Items/ItemDefinition.cs
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item Definition")]
public class ItemDefinition : ScriptableObject
{
    [Header("기본 정보")]
    public string displayName;
    public ItemType type;
    public Sprite icon;

    [Header("스택 정책")]
    public bool stackable = true;
    public int maxStack = 99;

    [Header("사용 효과 (소비형일 경우)")]
    public bool usable = false;              // 사용 가능한 아이템인가?
    public ItemUseEffect useEffect;          // 사용 시 실행할 효과(SO)

    [Header("피드백")]
    public AudioClip useSfx;                 // 사용 SFX(선택)
    public GameObject useVfxPrefab;          // 사용 VFX(선택)
}
