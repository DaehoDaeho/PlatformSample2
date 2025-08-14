// Scripts/UI/InventoryHUD.cs
using UnityEngine;

public class InventoryHUD : MonoBehaviour
{
    [SerializeField] private PlayerInventoryAdvanced inventory;

    void Awake()
    {
        if (!inventory) inventory = FindObjectOfType<PlayerInventoryAdvanced>();

        // 씬에 배치된 모든 ItemSlotUI를 스캔해 초깃값 갱신
        var slots = GetComponentsInChildren<ItemSlotUI>(true);
        foreach (var slot in slots)
        {
            // Awake/OnEnable에서 이미 최초 갱신을 수행하므로 여기선 생략 가능
            // 필요 시 추가 초기화 로직 작성
        }
    }
}
