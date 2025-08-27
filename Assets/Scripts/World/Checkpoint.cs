// Assets/Scripts/World/Checkpoint.cs
using UnityEngine;

/// <summary>
/// �÷��̾ Ʈ���ſ� ������ �� ��ġ�� üũ����Ʈ�� ����.
/// - Ư�� Ŭ������ �������� ����(�޽��� ���)
/// - �÷��̾�/�ڽ� ������Ʈ �� "SetCheckpoint(Vector2)"�� ���� ���� ����
/// - �� �� ������ ���� ����(�ð� ǥ��)
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour
{
    public Color activatedColor = new Color(1f, 1f, 0.6f, 1f);
    private bool isActivated = false;

    void Awake()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col.isTrigger == false)
        {
            col.isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == true)
        {
            // ��Ʈ�ѷ� �̸��� �����ϰ� ��ǥ ����(������ ȣ��, ������ ����)
            other.SendMessage("SetCheckpoint", (Vector2)transform.position, SendMessageOptions.DontRequireReceiver);

            if (isActivated == false)
            {
                isActivated = true;

                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = activatedColor;
                }
            }
        }
    }
}
