using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 발밑에서 3개의 광선을 쏴서 그 중 하나라도 지면에 충돌하면 플레이어가 지면에 있는 상태인 것으로 처리.
/// </summary>
public class GroundCheck2D : MonoBehaviour
{
    public Vector2 feetOffset = new Vector2(0.0f, -0.8f);
    public float rayLength = 0.1f;
    public float raySpacing = 0.25f;
    public LayerMask groundMask;

    private bool drawGizmos = true;

    public bool IsGrounded { get; private set; }
    public Vector2 FeetOrigin => (Vector2)transform.position + feetOffset;
        
    // Update is called once per frame
    void Update()
    {
        Vector2 c = FeetOrigin;
        Vector2 l = c + Vector2.left * raySpacing;
        Vector2 r = c + Vector2.right * raySpacing;

        bool hc = Physics2D.Raycast(c, Vector2.down, rayLength, groundMask);
        bool hl = Physics2D.Raycast(l, Vector2.down, rayLength, groundMask);
        bool hr = Physics2D.Raycast(r, Vector2.down, rayLength, groundMask);
        if(hc == true || hl == true || hr == true)
        {
            IsGrounded = true;
        }

        if(drawGizmos == true)
        {
            var col = IsGrounded == true ? Color.green : Color.red;
            Debug.DrawLine(c, c + Vector2.down * rayLength, col);
            Debug.DrawLine(l, l + Vector2.down * rayLength, col);
            Debug.DrawLine(r, r + Vector2.down * rayLength, col);
        }
    }
}
