// Scripts/UI/InventoryHUD.cs
using UnityEngine;

public class InventoryHUD : MonoBehaviour
{
    [SerializeField] private PlayerInventoryAdvanced inventory;

    void Awake()
    {
        if (!inventory) inventory = FindObjectOfType<PlayerInventoryAdvanced>();

        // ���� ��ġ�� ��� ItemSlotUI�� ��ĵ�� �ʱ갪 ����
        var slots = GetComponentsInChildren<ItemSlotUI>(true);
        foreach (var slot in slots)
        {
            // Awake/OnEnable���� �̹� ���� ������ �����ϹǷ� ���⼱ ���� ����
            // �ʿ� �� �߰� �ʱ�ȭ ���� �ۼ�
        }
    }
}
