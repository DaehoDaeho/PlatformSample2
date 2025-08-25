// Assets/Scripts/World/PlatformCarryPlayer.cs
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlatformCarryPlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") == false)
        {
            return;
        }

        // �������� �� "������ ������"�� ��ȣ�� �ִ��� Ȯ�� (������ ���� ����Ŵ)
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 n = collision.GetContact(i).normal;
            if (n.y < -0.5f) // ���� ���忡�� ����, ������ ������ ������ �Ʒ�(-Y)�� ����
            {
                // �÷��̾ ������ �ڽ����� ����
                collision.collider.transform.SetParent(transform);
                break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") == false)
        {
            return;
        }

        // ���ǿ��� �������� �θ� ����
        if (collision.collider.transform.parent == transform)
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
