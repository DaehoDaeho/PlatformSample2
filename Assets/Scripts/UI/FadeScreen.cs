// Assets/Scripts/UI/FadeScreen.cs
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 화면을 서서히 어둡게/밝게 만드는 컨트롤러.
/// - 코루틴 없이 Update에서 타이머로 진행
/// - StartFadeOut(duration) / StartFadeIn(duration) 호출
/// - 상태 확인용 IsCompletelyBlack / IsCompletelyClear 제공
/// </summary>
[RequireComponent(typeof(Image))]
public class FadeScreen : MonoBehaviour
{
    public float defaultDuration = 0.6f; // 기본 페이드 시간(초)

    private Image img;
    private bool fadingIn = false;   // true면 까맣게→투명(밝아짐)
    private bool fadingOut = false;  // true면 투명→까맣게(어두워짐)
    private float timer = 0f;        // 경과 시간
    private float duration = 0.6f;   // 현재 페이드에 사용할 총 시간

    void Awake()
    {
        img = GetComponent<Image>();

        // 시작은 완전 투명(검정 알파 0)
        Color c = img.color;
        c.a = 0f;
        img.color = c;
    }

    void Start()
    {
        // 새 씬에 들어왔을 때 자동으로 밝아지게 하고 싶으면 주석 해제
         StartFadeIn(defaultDuration);
    }

    void Update()
    {
        if (fadingIn == true)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);

            // 페이드 인: 알파 1 → 0
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

                // 페이드 아웃: 알파 0 → 1
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

        // 먼저 완전히 까맣게 만들고 → 시간에 따라 투명으로 감소
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

        // 투명에서 시작 → 시간에 따라 검은색으로 증가
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
