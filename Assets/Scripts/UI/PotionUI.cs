using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotionUI : MonoBehaviour
{
    public PlayerInventory player;
    public TextMeshProUGUI potionText;

    private void OnEnable()
    {
        if(player != null)
        {
            player.OnPotionChanged += UpdateText;
        }
    }

    private void OnDisable()
    {
        if(player != null)
        {
            player.OnPotionChanged -= UpdateText;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(player != null)
        {
            UpdateText(player.potionCount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateText(int count)
    {
        potionText.text = count.ToString();
    }
}
