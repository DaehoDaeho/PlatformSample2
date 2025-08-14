// Scripts/Items/PickupItem.cs
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupItem : MonoBehaviour
{
    [Header("�ݴ� ������ ����(SO)")]
    public ItemDefinition itemDefinition;
    public int amount = 1;

    [Header("�ɼ�")]
    public bool useOnPickup = false;  // ���ڸ��� ���(usable=true ����)
    public AudioClip pickupSfx;       // �ݴ� �Ҹ�
    public GameObject pickupVfx;      // �ݴ� ����Ʈ

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
            // ��� ���: �����ϸ� �Ҹ� (�κ��丮 �߰� ���� �ٷ� ȿ��)
            inv.UseItem(itemDefinition);
        }
        else
        {
            // ����: �κ��丮�� �߰�
            inv.AddItem(itemDefinition, amount);
        }

        if (pickupVfx) Instantiate(pickupVfx, transform.position, Quaternion.identity);
        if (pickupSfx && Camera.main)
            AudioSource.PlayClipAtPoint(pickupSfx, Camera.main.transform.position);

        Destroy(gameObject);
    }
}
