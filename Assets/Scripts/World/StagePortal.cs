// Assets/Scripts/World/StagePortal.cs
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �÷��̾ Ʈ���ſ� ������ ���̵� �ƿ� ����.
/// ȭ���� ������ ������� ������ ������ �ε�.
/// FadeScreen�� ������ ��� ���� ��� �ε�.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class StagePortal : MonoBehaviour
{
    public string nextSceneName = "Stage_2"; // �̵��� �� �̸�(Scenes In Build�� ��� �ʼ�)
    public float fadeDuration = 0.6f;

    private FadeScreen fade;          // ���� �����ϴ� FadeScreen
    private bool isTransitioning = false;

    void Awake()
    {
        // ����: ������ Ʈ���ŷ� ����
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
            Debug.LogWarning("FadeScreen�� ã�� ���߽��ϴ�. Canvas�� FadeImage+FadeScreen�� �߰��ϼ���.");
        }
    }

    void Update()
    {
        // ��ȯ ���̸� ���̵� ���� ����
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
        // �÷��̾ ����
        if (other.CompareTag("Player") == true)
        {
            if (fade != null)
            {
                fade.StartFadeOut(fadeDuration);
                isTransitioning = true;
            }
            else
            {
                // ���̵尡 ������ ��� �ε�(���� ����)
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
