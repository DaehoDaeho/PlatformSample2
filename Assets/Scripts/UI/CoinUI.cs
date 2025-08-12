using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    PlayerInventory player;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        if(player == null)
        {
            // <> 안에 해당되는 컴포넌트가 추가되어 있는 오브젝트를 가져오는 함수.
            player = FindObjectOfType<PlayerInventory>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(player != null)
        {
            UpdateText(player.coin);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(int value)
    {
        coinText.text = value.ToString();
    }
}
