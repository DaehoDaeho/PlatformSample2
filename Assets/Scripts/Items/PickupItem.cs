// Scripts/Items/PickupItem.cs
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupItem : MonoBehaviour
{
    [Header("줍는 아이템 정의(SO)")]
    public ItemDefinition itemDefinition;
    public int amount = 1;

    [Header("옵션")]
    public bool useOnPickup = false;  // 닿자마자 사용(usable=true 가정)
    public AudioClip pickupSfx;       // 줍는 소리
    public GameObject pickupVfx;      // 줍는 이펙트

    private bool consumed;

    void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (consumed) return;
        if (!other.CompareTag("Player")) return;

        var inv = other.GetComponent<PlayerInventoryAdvanced>();
        if (inv == null || itemDefinition == null) return;

        consumed = true;
        var col = GetComponent<Collider2D>();
        if (col) col.enabled = false;

        if (useOnPickup && itemDefinition.usable)
        {
            // 즉시 사용: 성공하면 소모 (인벤토리 추가 없이 바로 효과)
            inv.UseItem(itemDefinition);
        }
        else
        {
            // 보관: 인벤토리에 추가
            inv.AddItem(itemDefinition, amount);
        }

        if (pickupVfx) Instantiate(pickupVfx, transform.position, Quaternion.identity);
        if (pickupSfx && Camera.main)
            AudioSource.PlayClipAtPoint(pickupSfx, Camera.main.transform.position);

        Destroy(gameObject);
    }
}
