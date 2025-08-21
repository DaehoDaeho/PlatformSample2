// Assets/Scripts/UI/HpBarUIPoll.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpBarUIPoll : MonoBehaviour
{
    public PlayerHealthSimple health;
    public Slider hpSlider;            // 슬라이더를 쓰는 경우
    public TextMeshProUGUI hpText;     // 텍스트를 쓰는 경우(둘 중 하나만 써도 됨)

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
