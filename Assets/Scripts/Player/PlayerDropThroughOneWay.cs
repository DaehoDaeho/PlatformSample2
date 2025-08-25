// Assets/Scripts/Player/PlayerDropThroughOneWay.cs
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerDropThroughOneWay : MonoBehaviour
{
    public KeyCode downKey = KeyCode.S;          // �Ǵ� KeyCode.DownArrow
    public KeyCode jumpKey = KeyCode.LeftControl;
    public float ignoreTime = 0.25f;             // �� �ð� ���� �浹 ����
    public LayerMask oneWayMask;                 // OneWayPlatform ���̾�
    public GameObject groundCheck;

    private Collider2D playerCol;

    // (�÷��� �ݶ��̴�, ���� �ð�) ���
    private List<Collider2D> ignoredCols = new List<Collider2D>();
    private List<float> ignoredUntil = new List<float>();

    void Awake()
    {
        playerCol = GetComponent<Collider2D>();
        if (oneWayMask.value == 0)
        {
            oneWayMask = LayerMask.GetMask("OneWayPlatform");
        }
    }

    void Update()
    {
        // 1) ���� Ÿ�̸� ó��
        for (int i = ignoredCols.Count - 1; i >= 0; i--)
        {
            if (Time.time >= ignoredUntil[i])
            {
                var col = ignoredCols[i];
                if (col != null)
                {
                    Physics2D.IgnoreCollision(playerCol, col, false);
                }

                ignoredCols.RemoveAt(i);
                ignoredUntil.RemoveAt(i);
            }
        }

        // 2) �Է� üũ: �Ʒ�+����
        //if (Input.GetKey(downKey) == true && Input.GetKeyDown(jumpKey) == true)
        if (Input.GetKeyDown(downKey) == true)
        {
            TryDrop();
        }
    }

    private void TryDrop()
    {
        // �߹����� ���� ª�� Raycast �ؼ� "���� ���ִ�" OneWayPlatform ã��
        //Vector2 origin = (Vector2)transform.position + Vector2.down * 0.1f;
        Vector2 origin = (Vector2)groundCheck.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.2f, oneWayMask);

        if (hit.collider == null)
        {
            return; // �߹ؿ� �Ϲ����� ������ ����
        }

        // �浹 ��� ����
        Physics2D.IgnoreCollision(playerCol, hit.collider, true);
        ignoredCols.Add(hit.collider);
        ignoredUntil.Add(Time.time + ignoreTime);

        // ����Ű�� �Բ� ������, ������ ���� ���� ��ũ��Ʈ�� ó��
        // (���� ������ ������ ���߰� �ʹٸ� ���⼭ �÷��̾� �ӵ� y�� ��¦ -�� �ʱ�ȭ�� ���� ����)
    }
}
