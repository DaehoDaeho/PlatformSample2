// Assets/Scripts/Player/PlayerRespawnController.cs
using UnityEngine;

/// <summary>
/// �÷��̾� ������ ��� �ּ� ����.
/// - Checkpoint�� SetCheckpoint(Vector2)�� ȣ��
/// - ��� �� RespawnNow()�� ȣ��
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerRespawnController : MonoBehaviour
{
    public Vector2 lastCheckpointPos; // ����� ��ġ
    public bool hasCheckpoint = false;

    public void SetCheckpoint(Vector2 pos)
    {
        lastCheckpointPos = pos;
        hasCheckpoint = true;
    }

    public void RespawnNow()
    {
        // üũ����Ʈ�� �־�� �̵�
        if (hasCheckpoint == true)
        {
            transform.position = lastCheckpointPos;
        }

        // �ӵ� �ʱ�ȭ(ƨ��/���� ����)
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
