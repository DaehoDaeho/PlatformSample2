// Assets/Scripts/UI/HpBarUIPoll.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpBarUIPoll : MonoBehaviour
{
    public PlayerHealthSimple health;
    public Slider hpSlider;            // �����̴��� ���� ���
    public TextMeshProUGUI hpText;     // �ؽ�Ʈ�� ���� ���(�� �� �ϳ��� �ᵵ ��)

    void Start()
    {
        if (health == null) health = FindObjectOfType<PlayerHealthSimple>();

        if (hpSlider != null)
        {
            hpSlider.minValue = 0;
            hpSlider.maxValue = health.maxHp;
        }
    }

    void Update()
    {
        if (health == null) return;

        if (hpSlider != null)
        {
            hpSlider.value = health.hp;
        }

        if (hpText != null)
        {
            hpText.text = health.hp + " / " + health.maxHp;
        }
    }
}
