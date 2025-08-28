using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ȭ�� �»�� HUD: HP �� + HP �ؽ�Ʈ, ���� �ؽ�Ʈ, �������� �̸�.
/// �ٸ� ��ũ��Ʈ�� SetHP/SetCoins/SetStageName �� ȣ���ؼ� �����Ѵ�.
/// </summary>
public class HUDController : MonoBehaviour
{
    // --- ���� �ν��Ͻ�(���� �ܼ�) ---
    public static HUDController Instance;

    [Header("HP UI")]
    public Slider hpFill;   // ä��� �̹���(Image Type=Filled, Horizontal)
    public TMP_Text hpText;         // "HP 3 / 5"

    [Header("Coin UI")]
    public TMP_Text coinText;       // "x 0"

    [Header("Stage Name")]
    public TMP_Text stageText;      // "Stage 1"

    void Awake()
    {
        // �ν��Ͻ� ���(�ϳ��� �ִٰ� ����)
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // �ߺ� ����
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // �� �̸��� �⺻ �������������� ���(�ʿ� �� ��ü ����)
        string sceneName = SceneManager.GetActiveScene().name;
        SetStageName(sceneName);

        // ���� �� GameData ���� �ݿ�
        SetCoins(GameData.coinCount);

        // HP�� �÷��̾� �ʿ��� Start �� �� �� ȣ���� �ִ� ���� ����
    }

    /// <summary>HP �������� �ؽ�Ʈ�� ����.</summary>
    public void SetHP(int current, int max)
    {
        // 0���� ������ ����
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

    /// <summary>���� �ؽ�Ʈ ����.</summary>
    public void SetCoins(int coins)
    {
        if (coinText != null)
        {
            coinText.text = "x " + coins.ToString();
        }
    }

    /// <summary>�������� �̸� ����.</summary>
    public void SetStageName(string name)
    {
        if (stageText != null)
        {
            stageText.text = name;
        }
    }
}
