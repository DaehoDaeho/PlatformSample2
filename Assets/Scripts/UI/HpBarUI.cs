using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    public PlayerHealth health;
    public Slider hpBar;

    private void OnEnable()
    {
        if(health != null)
        {
            health.OnHpChanged += UpdateBar;
            SetupRangeAndInit(health);
        }
    }

    private void OnDisable()
    {
        if (health != null)
        {
            health.OnHpChanged -= UpdateBar;
        }
    }

    private void SetupRangeAndInit(PlayerHealth h)
    {
        hpBar.minValue = 0;
        hpBar.maxValue = h.MaxHp;
        hpBar.value = h.Hp;
    }

    void UpdateBar(int oldValue, int newValue)
    {
        if(hpBar != null)
        {
            hpBar.value = newValue;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
