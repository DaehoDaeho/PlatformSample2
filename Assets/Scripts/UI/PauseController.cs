using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 일시정지 토글과 재시작/타이틀 이동을 담당.
/// - Esc 키로 PausePanel 활성/비활성
/// - Time.timeScale 0/1 전환
/// </summary>
public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;     // 비활성화된 Panel을 연결

    private bool isPaused = false;

    void Start()
    {
        // 시작은 항상 해제 상태
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        Time.timeScale = 1f;
    }

    void Update()
    {
        // Esc로 토글
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            if (isPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;

        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }

        // 게임 정지
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isPaused = false;

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        // 게임 재개
        Time.timeScale = 1f;
    }

    public void RestartStage()
    {
        // 현재 씬 다시 로드
        string sceneName = SceneManager.GetActiveScene().name;
        // TimeScale을 원복하고 로드
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void GoToTitle()
    {
        // 실제 타이틀 씬 이름로 교체
        string titleScene = "Title";
        Time.timeScale = 1f;
        SceneManager.LoadScene(titleScene);
    }
}
