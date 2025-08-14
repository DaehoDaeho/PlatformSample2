// Scripts/Player/HotbarInput.cs
using UnityEngine;

public class HotbarInput : MonoBehaviour
{
    [Header("핫바 슬롯에 연결할 아이템(SO)")]
    public ItemDefinition slot1;
    public ItemDefinition slot2;
    public ItemDefinition slot3;

    private PlayerInventoryAdvanced inventory;

    void Awake()
    {
        inventory = GetComponent<PlayerInventoryAdvanced>();
    }

    void Update()
    {
        if (inventory == null) return;

        if (Input.GetKeyDown(KeyCode.Alpha1) && slot1)
            inventory.UseItem(slot1);

        if (Input.GetKeyDown(KeyCode.Alpha2) && slot2)
            inventory.UseItem(slot2);

        if (Input.GetKeyDown(KeyCode.Alpha3) && slot3)
            inventory.UseItem(slot3);
    }
}
