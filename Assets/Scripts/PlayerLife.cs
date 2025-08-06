using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int life = 3; // 남은 목숨
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 몬스터/장애물에 부딪히면
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Obstacle"))
        {
            life--; // 목숨 감소
            Debug.Log("피격! 남은 목숨: " + life);
            if (life <= 0)
            {
                Debug.Log("Game Over!");
                // Destroy(gameObject); // 또는 게임오버 UI/씬 전환
            }
            // (옵션) 피격 이펙트, 사운드 추가 가능
        }
    }

}
