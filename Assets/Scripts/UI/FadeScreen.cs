// Assets/Scripts/UI/FadeScreen.cs
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ȭ���� ������ ��Ӱ�/��� ����� ��Ʈ�ѷ�.
/// - �ڷ�ƾ ���� Update���� Ÿ�̸ӷ� ����
/// - StartFadeOut(duration) / StartFadeIn(duration) ȣ��
/// - ���� Ȯ�ο� IsCompletelyBlack / IsCompletelyClear ����
/// </summary>
[RequireComponent(typeof(Image))]
public class FadeScreen : MonoBehaviour
{
    public float defaultDuration = 0.6f; // �⺻ ���̵� �ð�(��)

    private Image img;
    private bool fadingIn = false;   // true�� ��İԡ�����(�����)
    private bool fadingOut = false;  // true�� ������İ�(��ο���)
    private float timer = 0f;        // ��� �ð�
    private float duration = 0.6f;   // ���� ���̵忡 ����� �� �ð�

    void Awake()
    {
        img = GetComponent<Image>();

        // ������ ���� ����(���� ���� 0)
        Color c = img.color;
        c.a = 0f;
        img.color = c;
    }

    void Start()
    {
        // �� ���� ������ �� �ڵ����� ������� �ϰ� ������ �ּ� ����
         StartFadeIn(defaultDuration);
    }

    void Update()
    {
        if (fadingIn == true)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);

            // ���̵� ��: ���� 1 �� 0
            SetAlpha(1f - t);

            if (t >= 1f)
            {
                fadingIn = false;
            }
        }
        else
        {
            if (fadingOut == true)
            {
                timer += Time.deltaTime;
                float t = Mathf.Clamp01(timer / duration);

                // ���̵� �ƿ�: ���� 0 �� 1
                SetAlpha(t);

                if (t >= 1f)
                {
                    fadingOut = false;
                }
            }
        }
    }

    public void StartFadeIn(float customDuration)
    {
        if (customDuration > 0f)
        {
            duration = customDuration;
        }
        else
        {
            duration = defaultDuration;
        }

        fadingIn = true;
        fadingOut = false;
        timer = 0f;

        // ���� ������ ��İ� ����� �� �ð��� ���� �������� ����
        SetAlpha(1f);
    }

    public void StartFadeOut(float customDuration)
    {
        if (customDuration > 0f)
        {
            duration = customDuration;
        }
        else
        {
            duration = defaultDuration;
        }

        fadingOut = true;
        fadingIn = false;
        timer = 0f;

        // ������ ���� �� �ð��� ���� ���������� ����
        SetAlpha(0f);
    }

    public bool IsCompletelyBlack()
    {
        return img.color.a >= 0.999f;
    }

    public bool IsCompletelyClear()
    {
        return img.color.a <= 0.001f;
    }

    private void SetAlpha(float a)
    {
        Color c = img.color;
        c.a = Mathf.Clamp01(a);
        img.color = c;
    }
}
