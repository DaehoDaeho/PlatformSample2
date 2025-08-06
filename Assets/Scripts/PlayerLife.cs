using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int life = 3; // ���� ���
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ����/��ֹ��� �ε�����
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Obstacle"))
        {
            life--; // ��� ����
            Debug.Log("�ǰ�! ���� ���: " + life);
            if (life <= 0)
            {
                Debug.Log("Game Over!");
                // Destroy(gameObject); // �Ǵ� ���ӿ��� UI/�� ��ȯ
            }
            // (�ɼ�) �ǰ� ����Ʈ, ���� �߰� ����
        }
    }

}
