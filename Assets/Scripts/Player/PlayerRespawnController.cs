// Assets/Scripts/Player/PlayerRespawnController.cs
using UnityEngine;

public class PlayerRespawnController : MonoBehaviour
{
    public PlayerHealthSimple health;
    public float respawnDelay = 1.0f;     // ��� �� ��� �ð�
    private float respawnTimer = 0f;

    public MonoBehaviour[] scriptsToDisableOnDeath; // ����/�̵� ���� ��ũ��Ʈ ����

    public GameObject deathVfx;
    public AudioClip deathSfx;

    private Vector3 startPosition; // üũ����Ʈ�� ���� �� ���ư� ���� ��ġ

    void Awake()
    {
        if (health == null) health = GetComponent<PlayerHealthSimple>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (health == null) return;

        // ��� ���� ������ �� �� ����: ���� ����, ����Ʈ/����
        if (health.isDead && respawnTimer == 0f)
        {
            // ���� ��ũ��Ʈ ��Ȱ��
            for (int i = 0; i < scriptsToDisableOnDeath.Length; i++)
            {
                if (scriptsToDisableOnDeath[i] != null)
                    scriptsToDisableOnDeath[i].enabled = false;
            }

            if (deathVfx) Instantiate(deathVfx, transform.position, Quaternion.identity);
            if (deathSfx && Camera.main)
                AudioSource.PlayClipAtPoint(deathSfx, Camera.main.transform.position);

            respawnTimer = respawnDelay; // Ÿ�̸� ����
        }

        // ��� ���̸� Ÿ�̸� ���
        if (health.isDead && respawnTimer > 0f)
        {
            respawnTimer -= Time.deltaTime;
            if (respawnTimer <= 0f)
            {
                DoRespawn();
            }
        }
    }

    private void DoRespawn()
    {
        // ��ġ ����: üũ����Ʈ�� ������ �ű��, ������ ���� ��ġ
        Vector3 targetPos = Checkpoint.hasCheckpoint ? Checkpoint.lastCheckpointPosition : startPosition;

        // �̵�
        transform.position = targetPos;

        // ü�� ȸ�� + ���� �ʱ�ȭ
        health.FullRestore();

        // ������ ���� ��� ����(�ٷ� �� ���� �ʰ�)
        health.invincible = true;
        health.invincibleEndTime = Time.time + 0.5f; // ���� ����
        health.SetSpriteVisible(true); // ����

        // ���� �ٽ� �ѱ�
        for (int i = 0; i < scriptsToDisableOnDeath.Length; i++)
        {
            if (scriptsToDisableOnDeath[i] != null)
                scriptsToDisableOnDeath[i].enabled = true;
        }

        respawnTimer = 0f;
    }
}
