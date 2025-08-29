using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �Ͻ����� ��۰� �����/Ÿ��Ʋ �̵��� ���.
/// - Esc Ű�� PausePanel Ȱ��/��Ȱ��
/// - Time.timeScale 0/1 ��ȯ
/// </summary>
public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;     // ��Ȱ��ȭ�� Panel�� ����

    private bool isPaused = false;

    void Start()
    {
        // ������ �׻� ���� ����
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        Time.timeScale = 1f;
    }

    void Update()
    {
        // Esc�� ���
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

        // ���� ����
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isPaused = false;

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        // ���� �簳
        Time.timeScale = 1f;
    }

    public void RestartStage()
    {
        // ���� �� �ٽ� �ε�
        string sceneName = SceneManager.GetActiveScene().name;
        // TimeScale�� �����ϰ� �ε�
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void GoToTitle()
    {
        // ���� Ÿ��Ʋ �� �̸��� ��ü
        string titleScene = "Title";
        Time.timeScale = 1f;
        SceneManager.LoadScene(titleScene);
    }
}
