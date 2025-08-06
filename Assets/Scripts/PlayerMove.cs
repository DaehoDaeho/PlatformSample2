using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum AnimState
{
    Idle,
    Move,
    Jump,
    Die
}

public class PlayerMove : MonoBehaviour
{
    //========== 애니메이션 관련 변수들 ==========
    public Sprite[] idleSprites;    // 대기 상태의 애니메이션에 사용할 스프라이트들의 배열.
    public Sprite[] moveSprites;    // 이동 상태의 애니메이션에 사용할 스프라이트들의 배열.
    public Sprite[] jumpSprites;    // 점프 상태의 애니메이션에 사용할 스프라이트들의 배열.
    public Sprite[] dieSprites; // 사망 상태의 애니메이션에 사용할 스프라이트들의 배열.

    private AnimState state = AnimState.Idle;   // 현재 동작 상태를 저장.

    private SpriteRenderer sr;  // 스프라이트 렌더러 컴포넌트.

    private int frame = 0;  // 현재 애니메이션 프레임.
    private float timer = 0.0f; // 다음 동작까지의 시간을 재기 위한 타이머 변수.
    public float frameRate = 0.15f; // 현재 동작에서 다음 동작까지 걸리는 시간.
    //===========================================

    // 이동 속도.
    public float moveSpeed = 5.0f;
    public float jumpPower = 6.0f;

    // Rigidbody2D를 저장할 변수.
    private Rigidbody2D rb;

    private float moveInput = 0.0f;

    public TextMeshProUGUI moveText;
    public Animator animator;
    
    public GameObject groundCheck;
    public LayerMask groundLayer;

    private bool isGrounded = true;

    public AudioSource jumpSound;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void PlayAnimation()
    {
        Sprite[] curArr = idleSprites;
        switch (state)
        {
            case AnimState.Move:
                {
                    curArr = moveSprites;
                }
                break;

            case AnimState.Jump:
                {
                    curArr = jumpSprites;
                }
                break;

            case AnimState.Die:
                {
                    curArr = dieSprites;
                }
                break;
        }

        frame = (frame + 1) % curArr.Length;
        sr.sprite = curArr[frame];
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = IsGrounded();

        if (isGrounded == true)
        {
            state = AnimState.Idle;
        }

        moveInput = Input.GetAxisRaw("Horizontal");

        float scaleX = Mathf.Abs(gameObject.transform.localScale.x);
        bool isMove = true;
        if (moveInput > 0.01f)
        {
            moveText.text = "Move to Right";
            gameObject.transform.localScale = new Vector3(scaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

            if(state != AnimState.Jump)
            {
                state = AnimState.Move;
            }
        }
        else if(moveInput < -0.01f)
        {
            moveText.text = "Move to Left";
            gameObject.transform.localScale = new Vector3(-scaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

            if (state != AnimState.Jump)
            {
                state = AnimState.Move;
            }
        }
        else
        {
            moveText.text = "Not Move";
            isMove = false;

            if(state != AnimState.Jump)
            {
                state = AnimState.Idle;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 3.0f);
        //if (hit.collider.gameObject.tag == "Ground")
        //{
        //    isGrounded = true;
        //    animator.SetBool("IsJump", false);
        //}


        //animator.SetBool("IsMove", isMove);
        //animator.SetBool("IsJump", !isGrounded);

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0.0f;
            PlayAnimation();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        //rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        isGrounded = false;
        //animator.SetBool("IsJump", true);
        jumpSound.Play();
        state = AnimState.Jump;
    }

    private void FixedUpdate()
    {
        // 물리엔진을 이용한 이동 처리
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);        
    }

    private bool IsGrounded()
    {
        float rayLength = 0.25f;
        Vector2 rayOrigin = new Vector2(groundCheck.transform.position.x, groundCheck.transform.position.y - 0.1f);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, groundLayer);
        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collision 안에 충돌한 오브젝트의 정보가 들어있음.
        //if(collision.gameObject.tag == "Ground")
        //{
        //    isGrounded = true;
        //    animator.SetBool("IsJump", false);
        //}
        //else if(collision.gameObject.tag == "Obstacle")
        //{
        //    animator.SetBool("IsDead", true);
        //}
    }
}
