// Scripts/World/DoorRequiresKey.cs
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DoorRequiresKey : MonoBehaviour
{
    public ItemDefinition requiredKey; // ���� ���� �ʿ��� Ű SO
    [Header("�ǵ��")]
    public AudioClip doorOpenSfx;
    public Animator animator;          // �� �ִϸ�����(���� Ʈ����)
    public string openTriggerName = "Open";

    private bool opened;
    private Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        if (col) col.isTrigger = true; // �÷��̾� ��� Ʈ���ŷ� ���
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (opened) return;
        if (!other.CompareTag("Player")) return;
        if (requiredKey == null) return;

        var inv = other.GetComponent<PlayerInventoryAdvanced>();
        if (inv == null) return;

        // Ű 1�� ���� �õ�
        int removed = inv.RemoveItem(requiredKey, 1);
        if (removed > 0)
        {
            opened = true;

            // �� ���� ó��: ������ ���� ���(���� �浹ü�� ���� �ִٸ� �װ��� ��Ȱ��ȭ)
            if (animator) animator.SetTrigger(openTriggerName);

            if (doorOpenSfx && Camera.main)
                AudioSource.PlayClipAtPoint(doorOpenSfx, Camera.main.transform.position);

            // �� ���� �浹ü�� ���ų�, ���� Barrier �ݶ��̴��� ���� ��
            // ���⼭�� �ڱ� �ڽ��� Barrier��� �����ϰ�, isTrigger=true ������ ��� ���
            // �ʿ��ϸ� SpriteRenderer ��ü/������Ʈ ��Ȱ��ȭ � ����
        }
        // else: Ű ���� �� UI�� �ȳ� �޽��� ���� ������ ���� �߰� ����
    }
}
