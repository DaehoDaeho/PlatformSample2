// Assets/Scripts/Player/PlayerRespawnController.cs
using UnityEngine;

public class PlayerRespawnController : MonoBehaviour
{
    public PlayerHealthSimple health;
    public float respawnDelay = 1.0f;     // 사망 후 대기 시간
    private float respawnTimer = 0f;

    public MonoBehaviour[] scriptsToDisableOnDeath; // 점프/이동 같은 스크립트 연결

    public GameObject deathVfx;
    public AudioClip deathSfx;

    private Vector3 startPosition; // 체크포인트가 없을 때 돌아갈 최초 위치

    void Awake()
    {
        if (health == null) health = GetComponent<PlayerHealthSimple>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (health == null) return;

        // 사망 시작 시점에 한 번 실행: 조작 끄고, 이펙트/사운드
        if (health.isDead && respawnTimer == 0f)
        {
            // 조작 스크립트 비활성
            for (int i = 0; i < scriptsToDisableOnDeath.Length; i++)
            {
                if (scriptsToDisableOnDeath[i] != null)
                    scriptsToDisableOnDeath[i].enabled = false;
            }

            if (deathVfx) Instantiate(deathVfx, transform.position, Quaternion.identity);
            if (deathSfx && Camera.main)
                AudioSource.PlayClipAtPoint(deathSfx, Camera.main.transform.position);

            respawnTimer = respawnDelay; // 타이머 시작
        }

        // 사망 중이면 타이머 깎기
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
        // 위치 결정: 체크포인트가 있으면 거기로, 없으면 시작 위치
        Vector3 targetPos = Checkpoint.hasCheckpoint ? Checkpoint.lastCheckpointPosition : startPosition;

        // 이동
        transform.position = targetPos;

        // 체력 회복 + 상태 초기화
        health.FullRestore();

        // 리스폰 직후 잠깐 무적(바로 또 죽지 않게)
        health.invincible = true;
        health.invincibleEndTime = Time.time + 0.5f; // 반초 정도
        health.SetSpriteVisible(true); // 리셋

        // 조작 다시 켜기
        for (int i = 0; i < scriptsToDisableOnDeath.Length; i++)
        {
            if (scriptsToDisableOnDeath[i] != null)
                scriptsToDisableOnDeath[i].enabled = true;
        }

        respawnTimer = 0f;
    }
}
