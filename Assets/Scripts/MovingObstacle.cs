using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    private int dir = 1;

    // Start is called before the first frame update
    private void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * dir * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") == true)
        {
            dir = dir * -1;
        }
    }
}
