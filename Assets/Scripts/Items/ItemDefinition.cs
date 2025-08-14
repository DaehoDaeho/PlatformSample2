// Scripts/Items/ItemDefinition.cs
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item Definition")]
public class ItemDefinition : ScriptableObject
{
    [Header("�⺻ ����")]
    public string displayName;
    public ItemType type;
    public Sprite icon;

    [Header("���� ��å")]
    public bool stackable = true;
    public int maxStack = 99;

    [Header("��� ȿ�� (�Һ����� ���)")]
    public bool usable = false;              // ��� ������ �������ΰ�?
    public ItemUseEffect useEffect;          // ��� �� ������ ȿ��(SO)

    [Header("�ǵ��")]
    public AudioClip useSfx;                 // ��� SFX(����)
    public GameObject useVfxPrefab;          // ��� VFX(����)
}
