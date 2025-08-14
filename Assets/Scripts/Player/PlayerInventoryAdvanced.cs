// Scripts/Player/PlayerInventoryAdvanced.cs
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryAdvanced : MonoBehaviour
{
    // 아이템별 보유량(런타임 저장소)
    private readonly Dictionary<ItemDefinition, int> counts = new();

    /// <summary>특정 아이템 개수가 바뀔 때 알림: (itemDef, newCount)</summary>
    public event Action<ItemDefinition, int> OnItemCountChanged;

    /// <summary>현재 보유 개수 조회(없으면 0)</summary>
    public int GetCount(ItemDefinition item)
    {
        return item != null && counts.TryGetValue(item, out int c) ? c : 0;
    }

    /// <summary>아이템 추가. 실제 반영된 수량을 반환.</summary>
    public int AddItem(ItemDefinition item, int amount)
    {
        if (item == null || amount <= 0) return 0;

        int current = GetCount(item);
        int target = current + amount;

        // 스택 정책 적용
        if (item.stackable)
        {
            int cap = Mathf.Max(1, item.maxStack);
            int newCount = Mathf.Min(target, cap);
            int applied = newCount - current;
            if (applied <= 0) return 0;

            counts[item] = newCount;
            OnItemCountChanged?.Invoke(item, newCount);
            return applied;
        }
        else
        {
            // 비스택형은 개념상 0/1로만 (원한다면 여러 칸 처리 구조 필요)
            int applied = current == 0 ? 1 : 0;
            if (applied == 1)
            {
                counts[item] = 1;
                OnItemCountChanged?.Invoke(item, 1);
            }
            return applied;
        }
    }

    /// <summary>아이템 n개 제거(주로 문/소모 등). 실제 제거 수 반환.</summary>
    public int RemoveItem(ItemDefinition item, int amount)
    {
        if (item == null || amount <= 0) return 0;

        int current = GetCount(item);
        int removed = Mathf.Min(current, amount);
        int newCount = current - removed;

        if (removed > 0)
        {
            if (newCount > 0) counts[item] = newCount;
            else counts.Remove(item);

            OnItemCountChanged?.Invoke(item, newCount);
        }
        return removed;
    }

    /// <summary>아이템 사용 시도. 성공 시 1개 소모(정책상).</summary>
    public bool UseItem(ItemDefinition item)
    {
        if (item == null || !item.usable) return false;
        if (GetCount(item) <= 0) return false;

        // 실제 효과 적용 (효과가 성공해야 소모)
        bool applied = item.useEffect ? item.useEffect.Apply(gameObject) : false;
        if (!applied) return false;

        // 소모 + 피드백
        RemoveItem(item, 1);
        if (item.useVfxPrefab) Instantiate(item.useVfxPrefab, transform.position, Quaternion.identity);
        if (item.useSfx && Camera.main)
            AudioSource.PlayClipAtPoint(item.useSfx, Camera.main.transform.position);
        return true;
    }
}
