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
            // <> �ȿ� �ش�Ǵ� ������Ʈ�� �߰��Ǿ� �ִ� ������Ʈ�� �������� �Լ�.
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
