// Scripts/Player/PlayerInventoryAdvanced.cs
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryAdvanced : MonoBehaviour
{
    // �����ۺ� ������(��Ÿ�� �����)
    private readonly Dictionary<ItemDefinition, int> counts = new();

    /// <summary>Ư�� ������ ������ �ٲ� �� �˸�: (itemDef, newCount)</summary>
    public event Action<ItemDefinition, int> OnItemCountChanged;

    /// <summary>���� ���� ���� ��ȸ(������ 0)</summary>
    public int GetCount(ItemDefinition item)
    {
        return item != null && counts.TryGetValue(item, out int c) ? c : 0;
    }

    /// <summary>������ �߰�. ���� �ݿ��� ������ ��ȯ.</summary>
    public int AddItem(ItemDefinition item, int amount)
    {
        if (item == null || amount <= 0) return 0;

        int current = GetCount(item);
        int target = current + amount;

        // ���� ��å ����
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
            // �������� ����� 0/1�θ� (���Ѵٸ� ���� ĭ ó�� ���� �ʿ�)
            int applied = current == 0 ? 1 : 0;
            if (applied == 1)
            {
                counts[item] = 1;
                OnItemCountChanged?.Invoke(item, 1);
            }
            return applied;
        }
    }

    /// <summary>������ n�� ����(�ַ� ��/�Ҹ� ��). ���� ���� �� ��ȯ.</summary>
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

    /// <summary>������ ��� �õ�. ���� �� 1�� �Ҹ�(��å��).</summary>
    public bool UseItem(ItemDefinition item)
    {
        if (item == null || !item.usable) return false;
        if (GetCount(item) <= 0) return false;

        // ���� ȿ�� ���� (ȿ���� �����ؾ� �Ҹ�)
        bool applied = item.useEffect ? item.useEffect.Apply(gameObject) : false;
        if (!applied) return false;

        // �Ҹ� + �ǵ��
        RemoveItem(item, 1);
        if (item.useVfxPrefab) Instantiate(item.useVfxPrefab, transform.position, Quaternion.identity);
        if (item.useSfx && Camera.main)
            AudioSource.PlayClipAtPoint(item.useSfx, Camera.main.transform.position);
        return true;
    }
}
