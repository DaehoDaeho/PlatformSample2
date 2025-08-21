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
    public Animator animator;

    //========== �ִϸ��̼� ���� ������ ==========
    //public Sprite[] idleSprites;    // ��� ������ �ִϸ��̼ǿ� ����� ��������Ʈ���� �迭.
    //public Sprite[] moveSprites;    // �̵� ������ �ִϸ��̼ǿ� ����� ��������Ʈ���� �迭.
    //public Sprite[] jumpSprites;    // ���� ������ �ִϸ��̼ǿ� ����� ��������Ʈ���� �迭.
    //public Sprite[] dieSprites; // ��� ������ �ִϸ��̼ǿ� ����� ��������Ʈ���� �迭.

    private AnimState state = AnimState.Idle;   // ���� ���� ���¸� ����.

    private SpriteRenderer sr;  // ��������Ʈ ������ ������Ʈ.

    //private int frame = 0;  // ���� �ִϸ��̼� ������.
        
    //private float timer = 0.0f; // ���� ���۱����� �ð��� ��� ���� Ÿ�̸� ����.
    //public float frameRate = 0.15f; // ���� ���ۿ��� ���� ���۱��� �ɸ��� �ð�.

    private bool isKnockback = false;   // ���� �÷��̾� ĳ���Ͱ� �˹��� ���ϰ� �ִ� ������.
    private float knockbackTimer = 0f;  // �˹��� ������ �ð��� ��� ���� Ÿ�̸�.
    //===========================================

    // �̵� �ӵ�.
    public float moveSpeed = 5.0f;
    public float jumpPower = 6.0f;

    // Rigidbody2D�� ������ ����.
    private Rigidbody2D rb;

    private float moveInput = 0.0f;

    public TextMeshProUGUI moveText;
    
    public GameObject groundCheck;
    public LayerMask groundLayer;

    private bool isGrounded = true;

    public AudioSource jumpSound;

    private int hp = 100;

    PlayerHealthSimple playerHealth;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealthSimple>();
    }

    void PlayAnimation()
    {
        //Sprite[] curArr = idleSprites;
        //switch (state)
        //{
        //    case AnimState.Move:
        //        {
        //            curArr = moveSprites;
        //        }
        //        break;

        //    case AnimState.Jump:
        //        {
        //            curArr = jumpSprites;
        //        }
        //        break;

        //    case AnimState.Die:
        //        {
        //            curArr = dieSprites;
        //        }
        //        break;
        //}

        //frame = (frame + 1) % curArr.Length;
        //sr.sprite = curArr[frame];
    }

    // ���� ���� �� ���Ͱ� ȣ��
    public void TakeDamage(int damage, Vector2 knockbackDir, float knockbackForce, float knockbackTime)
    {
        // ü�� ���� ���� ���⿡!
        // ����: hp -= damage;
        hp -= damage;

        // �˹� ó��
        isKnockback = true;
        knockbackTimer = knockbackTime;
        rb.velocity = knockbackDir.normalized * knockbackForce;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth != null && playerHealth.IsInHitStun() == true)
        {
            return;
        }

        //if(isKnockback == false)
        {
            isGrounded = IsGrounded();

            moveInput = Input.GetAxisRaw("Horizontal");

            float scaleX = Mathf.Abs(gameObject.transform.localScale.x);
            bool isMove = true;
            if (moveInput > 0.01f)
            {
                moveText.text = "Move to Right";
                gameObject.transform.localScale = new Vector3(scaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
            else if (moveInput < -0.01f)
            {
                moveText.text = "Move to Left";
                gameObject.transform.localScale = new Vector3(-scaleX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
            else
            {
                moveText.text = "Not Move";
                isMove = false;
            }

            animator.SetBool("Move", isMove);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        isGrounded = false;
        jumpSound.Play();
        //state = AnimState.Jump;
    }

    private void FixedUpdate()
    {
        if(playerHealth != null && playerHealth.IsInHitStun() == true)
        //if (isKnockback)
        {
            //knockbackTimer -= Time.deltaTime;
            //if (knockbackTimer <= 0)
            //{
            //    isKnockback = false;
            //    rb.velocity = Vector2.zero;
            //    //state = AnimState.Idle;
            //}
            //rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
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
        //collision �ȿ� �浹�� ������Ʈ�� ������ �������.
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
