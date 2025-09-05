using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // ����ڷκ��� Ű �Է��� �޴´�.
    // Ű �Է��� ���� ��, Ư�� �������� �̵��� ���Ѿ� �Ѵ�.
    // �̵� �ӵ�.
    Rigidbody2D rigidbody;
    Animator animator;
    float inputX;
    public float moveSpeed = 4.0f;
    public float jumpPower = 5.0f;
    bool isJump = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        //inputX = Input.GetAxisRaw("Horizontal");
        inputX = Input.GetAxis("Horizontal");
        
        float scaleValue = gameObject.transform.localScale.x;   // ������ ���� Ű�� �������ٸ� -1.
        if(inputX > 0.0f)
        {
            scaleValue = Mathf.Abs(scaleValue) * 1.0f;
        }
        else if(inputX < 0.0f)
        {
            scaleValue = Mathf.Abs(scaleValue) * -1.0f;
        }

        gameObject.transform.localScale = new Vector3(scaleValue, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

        animator.SetFloat("Speed", inputX);

        if(Input.GetKeyDown(KeyCode.Space) == true)
        {
            animator.SetTrigger("Jump");
            isJump = true;
        }
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(inputX * moveSpeed, rigidbody.velocity.y);

        if(isJump == true)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0.0f);
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJump = false;
        }
    }
}
