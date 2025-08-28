using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 화면 좌상단 HUD: HP 바 + HP 텍스트, 코인 텍스트, 스테이지 이름.
/// 다른 스크립트가 SetHP/SetCoins/SetStageName 을 호출해서 갱신한다.
/// </summary>
public class HUDController : MonoBehaviour
{
    // --- 정적 인스턴스(아주 단순) ---
    public static HUDController Instance;

    [Header("HP UI")]
    public Slider hpFill;   // 채우기 이미지(Image Type=Filled, Horizontal)
    public TMP_Text hpText;         // "HP 3 / 5"

    [Header("Coin UI")]
    public TMP_Text coinText;       // "x 0"

    [Header("Stage Name")]
    public TMP_Text stageText;      // "Stage 1"

    void Awake()
    {
        // 인스턴스 등록(하나만 있다고 가정)
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // 중복 방지
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 씬 이름을 기본 스테이지명으로 사용(필요 시 교체 가능)
        string sceneName = SceneManager.GetActiveScene().name;
        SetStageName(sceneName);

        // 시작 시 GameData 값을 반영
        SetCoins(GameData.coinCount);

        // HP는 플레이어 쪽에서 Start 때 한 번 호출해 주는 것을 권장
    }

    /// <summary>HP 게이지와 텍스트를 설정.</summary>
    public void SetHP(int current, int max)
    {
        // 0으로 나누기 방지
        float fill = 0f;
        if (max > 0)
        {
            fill = Mathf.Clamp01((float)current / (float)max);
        }

        if (hpFill != null)
        {
            hpFill.value = fill;
        }

        if (hpText != null)
        {
            hpText.text = "HP " + current.ToString() + " / " + max.ToString();
        }
    }

    /// <summary>코인 텍스트 설정.</summary>
    public void SetCoins(int coins)
    {
        if (coinText != null)
        {
            coinText.text = "x " + coins.ToString();
        }
    }

    /// <summary>스테이지 이름 설정.</summary>
    public void SetStageName(string name)
    {
        if (stageText != null)
        {
            stageText.text = name;
        }
    }
}
