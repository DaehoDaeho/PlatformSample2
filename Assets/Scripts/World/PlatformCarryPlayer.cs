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

        // 접촉점들 중 "위에서 눌렀다"는 신호가 있는지 확인 (법선이 위를 가리킴)
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 n = collision.GetContact(i).normal;
            if (n.y < -0.5f) // 발판 입장에서 보면, 위에서 눌리면 법선이 아래(-Y)로 나옴
            {
                // 플레이어를 발판의 자식으로 붙임
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

        // 발판에서 내려오면 부모 해제
        if (collision.collider.transform.parent == transform)
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
