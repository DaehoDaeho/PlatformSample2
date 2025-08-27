// Assets/Scripts/World/StagePortal.cs
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 플레이어가 트리거에 들어오면 페이드 아웃 시작.
/// 화면이 완전히 까매지면 지정한 씬으로 로드.
/// FadeScreen이 없으면 경고만 띄우고 즉시 로드.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class StagePortal : MonoBehaviour
{
    public string nextSceneName = "Stage_2"; // 이동할 씬 이름(Scenes In Build에 등록 필수)
    public float fadeDuration = 0.6f;

    private FadeScreen fade;          // 씬에 존재하는 FadeScreen
    private bool isTransitioning = false;

    void Awake()
    {
        // 안전: 무조건 트리거로 강제
        Collider2D col = GetComponent<Collider2D>();
        if (col.isTrigger == false)
        {
            col.isTrigger = true;
        }
    }

    void Start()
    {
        fade = GameObject.FindObjectOfType<FadeScreen>();
        if (fade == null)
        {
            Debug.LogWarning("FadeScreen을 찾지 못했습니다. Canvas에 FadeImage+FadeScreen을 추가하세요.");
        }
    }

    void Update()
    {
        // 전환 중이면 페이드 상태 감시
        if (isTransitioning == true)
        {
            if (fade != null)
            {
                if (fade.IsCompletelyBlack() == true)
                {
                    isTransitioning = false;
                    SceneManager.LoadScene(nextSceneName);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어만 반응
        if (other.CompareTag("Player") == true)
        {
            if (fade != null)
            {
                fade.StartFadeOut(fadeDuration);
                isTransitioning = true;
            }
            else
            {
                // 페이드가 없으면 즉시 로드(실패 방지)
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
