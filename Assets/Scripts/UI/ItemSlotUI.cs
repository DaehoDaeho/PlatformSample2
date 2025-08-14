// Scripts/UI/ItemSlotUI.cs
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [Header("바인딩")]
    public ItemDefinition item;                 // 이 슬롯이 표시할 아이템 SO
    [SerializeField] private PlayerInventoryAdvanced inventory;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI countText;

    [Header("표현")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color emptyColor = new Color(1f, 1f, 1f, 0.35f);

    void Awake()
    {
        if (!inventory) inventory = FindObjectOfType<PlayerInventoryAdvanced>();
        if (icon && item && item.icon) icon.sprite = item.icon; // 최초 아이콘 세팅
    }

    void OnEnable()
    {
        if (inventory != null)
        {
            inventory.OnItemCountChanged += HandleCountChanged;
            // 초기 갱신
            HandleCountChanged(item, inventory.GetCount(item));
        }
    }

    void OnDisable()
    {
        if (inventory != null)
            inventory.OnItemCountChanged -= HandleCountChanged;
    }

    private void HandleCountChanged(ItemDefinition changedItem, int newCount)
    {
        if (changedItem != item) return;

        if (countText) countText.text = newCount.ToString();
        if (icon) icon.color = (newCount > 0) ? normalColor : emptyColor;
    }
}
