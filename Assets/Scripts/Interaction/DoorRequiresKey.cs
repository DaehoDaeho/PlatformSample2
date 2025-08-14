// Scripts/World/DoorRequiresKey.cs
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DoorRequiresKey : MonoBehaviour
{
    public ItemDefinition requiredKey; // 열기 위해 필요한 키 SO
    [Header("피드백")]
    public AudioClip doorOpenSfx;
    public Animator animator;          // 문 애니메이터(열림 트리거)
    public string openTriggerName = "Open";

    private bool opened;
    private Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        if (col) col.isTrigger = true; // 플레이어 통과 트리거로 사용
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (opened) return;
        if (!other.CompareTag("Player")) return;
        if (requiredKey == null) return;

        var inv = other.GetComponent<PlayerInventoryAdvanced>();
        if (inv == null) return;

        // 키 1개 제거 시도
        int removed = inv.RemoveItem(requiredKey, 1);
        if (removed > 0)
        {
            opened = true;

            // 문 열림 처리: 실제로 통행 허용(막는 충돌체가 따로 있다면 그것을 비활성화)
            if (animator) animator.SetTrigger(openTriggerName);

            if (doorOpenSfx && Camera.main)
                AudioSource.PlayClipAtPoint(doorOpenSfx, Camera.main.transform.position);

            // 문 역할 충돌체를 끄거나, 별도 Barrier 콜라이더를 꺼도 됨
            // 여기서는 자기 자신이 Barrier라고 가정하고, isTrigger=true 유지로 통과 허용
            // 필요하면 SpriteRenderer 교체/오브젝트 비활성화 등도 가능
        }
        // else: 키 없음 → UI에 안내 메시지 노출 로직을 별도 추가 가능
    }
}
